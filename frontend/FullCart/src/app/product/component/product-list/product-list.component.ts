import { Component, OnInit, inject } from '@angular/core';
import { ProductService } from '../../service/product.service';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable, Subject, debounceTime, distinctUntilChanged, filter, of, startWith, tap } from 'rxjs';
import { Category, InventoryProduct } from '../../../core/auth/types';
import { ListboxChangeEvent } from 'primeng/listbox';
import { DropdownChangeEvent } from 'primeng/dropdown';
import { FormControl } from '@angular/forms';
import { UserService } from '../../../core/auth/services/user.service';
import { SharedService } from '../../../shared/service/shared.service';
import { CartItemCreate } from '../../../shared/types';
import { MessageService } from 'primeng/api';
import { SharedObservableService } from '../../../shared/service/shared-observable.service';
import { TopBarComponent } from '../../../app.topbar.component';

@Component({
  selector: 'product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {
  productService = inject(ProductService);
  router = inject(Router)
  categories$: Observable<Category[]> | undefined;
  inventoryProduct: InventoryProduct[] | undefined;
  loadingInventoryProduct: boolean | undefined;
  categoryIdSubject = new BehaviorSubject('')
  sortOrderSubject = new BehaviorSubject('')
  queryParamSubject = {
    categoryId: new BehaviorSubject(''),
    sortOrder: new BehaviorSubject('')
  }
  userService = inject(UserService)
  sharedService = inject(SharedService)
  messageService = inject(MessageService)
  sharedObsService = inject(SharedObservableService)
  topBarComponent = inject(TopBarComponent)
  searchControl = new FormControl('')
  sortOptions = [
    { label: 'High to Low', value: 'descending' },
    { label: 'Low to High', value: 'ascending' }
  ]
  constructor() {
    
  }

  ngOnInit() {
    this.getProductCategory()
    this.getInventoryProduct()
    this.searchControl.valueChanges
      .pipe(
        debounceTime(1000),
        distinctUntilChanged(),
    ).subscribe(searchQuery => this.getInventoryProduct(
      this.queryParamSubject.categoryId.getValue(),
      this.queryParamSubject.sortOrder.getValue(),
      searchQuery!
    ))
  }

  getSeverity(product: InventoryProduct): string | undefined { 
    switch (product.status?.description) {
      case 'INSTOCK':
        return 'success';

      case 'LOWSTOCK':
        return 'warning';

      case 'OUTOFSTOCK':
        return 'danger';

      default:
        return undefined
    }
  };

  getProductCategory() {
    this.categories$ = this.productService.getProductCategory()
  }

  getInventoryProduct(categoryId: string = '', sortOrder:string = '',searchTerm = '') {
    this.loadingInventoryProduct = true
    this.productService.getInventoryProduct(categoryId, sortOrder, searchTerm).subscribe({
      next: (inventoryProduct) => {
        this.inventoryProduct = inventoryProduct
        this.loadingInventoryProduct = false
      }
    })
  }

  onCategoryChange(event: ListboxChangeEvent) {
    this.queryParamSubject.categoryId.next(event?.value?.id)
    this.getInventoryProduct(
      this.queryParamSubject.categoryId.getValue(),
      this.queryParamSubject.sortOrder.getValue());
  }
  onSortChange(event: DropdownChangeEvent) {
    this.queryParamSubject.sortOrder.next(event?.value)
    this.getInventoryProduct(
      this.queryParamSubject.categoryId.getValue(),
      this.queryParamSubject.sortOrder.getValue())
  }

  addItemToCart(product: InventoryProduct) {
    product.loadingStatus = true
    const user = this.userService.getUser()
    const addItemInCartRequest: CartItemCreate = {
      inventoryId: product.id!,
      userId: user?.userInfo?.id,
      addedQuantity:1
    }
    this.sharedService.addItemToCart(addItemInCartRequest)
      .subscribe({
        next: () => {
          this.messageService.add({
            severity: 'success',
            summary: 'Success',
            detail: 'Item added to cart!'
          })
          this.topBarComponent.updateCartCount()
          product.loadingStatus = false
        },
        error: () => {
          this.messageService.add({
            severity: 'error',
            summary: 'Error',
            detail: 'Something went wrong!, unable to add item to cart'
          })
          product.loadingStatus = false
        }
      })
  }

}
