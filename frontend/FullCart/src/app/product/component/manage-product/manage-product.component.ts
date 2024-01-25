import { Component } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ProductService } from '../../service/product.service';
import { Category, InventoryProduct, Status } from '../../../core/auth/types';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { map } from 'rxjs';

@Component({
  selector: 'app-manage-product',
  templateUrl: './manage-product.component.html',
  styleUrls: ['./manage-product.component.css']
})
export class ManageProductComponent {
  productDialog: boolean = false;

  products!: InventoryProduct[];

  product!: InventoryProduct;

  selectedProducts!: any[] | null;
  submitted: boolean = false;
   categories: Category[] | undefined;
   inventoryStatuses: Status[] |undefined;

  constructor(private productService: ProductService, private messageService: MessageService, private confirmationService: ConfirmationService) { }

  ngOnInit() {
    this.getInventoryProduct()
    this.getProductCategory()
    this.getInventoryStatus()
  }

  getInventoryProduct(categoryId: string = '', sortOrder: string = '', searchTerm = '') {
    this.productService.getInventoryProduct(categoryId, sortOrder, searchTerm).subscribe({
      next: (inventoryProduct) => {
        this.products = inventoryProduct
      }
    })
  }

  getProductCategory() {
    this.productService.getProductCategory().pipe(map(cat => cat.filter(c => c.name !== 'All'))).subscribe({
      next: (categories) => {
        this.categories = categories
      }
    })
  }

  getInventoryStatus() {
    this.productService.getInventoryStatus().subscribe({
      next: (statuses) => {
        this.inventoryStatuses = statuses
      }
    })
  }


  openNew() {
    this.product = {};
    this.submitted = false;
    this.productDialog = true;
  }

  deleteSelectedProducts() {
    this.confirmationService.confirm({
      message: 'Are you sure you want to delete the selected products?',
      header: 'Confirm',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.products = this.products.filter((val) => !this.selectedProducts?.includes(val));
        this.selectedProducts = null;
        this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Products Deleted', life: 3000 });
      }
    });
  }

  editProduct(product: InventoryProduct) {
    this.product = { ...product };
    this.product.category = this.categories?.find(c => c.id === product.category?.id)
    this.productDialog = true;
  }

  deleteProduct(product:InventoryProduct) {
    this.confirmationService.confirm({
      message: 'Are you sure you want to delete ' + product.name + '?',
      header: 'Confirm',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.productService.deleteProductInInventory(product.id!).subscribe(() => {
          this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Product Deleted', life: 3000 });
          this.getInventoryProduct()
        })
        this.product = {};
      }
    });
  }

  hideDialog() {
    this.productDialog = false;
    this.submitted = false;
  }

  saveProduct() {
    this.submitted = true;
    this.products = [...this.products];
    this.productDialog = false;
    if (this.product.name?.trim()) {
      if (this.product.id) {
        this.productService.updateProductInInventory(this.product)
          .subscribe({
            next: () => {
              this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Product Updated', life: 3000 });
              this.getInventoryProduct();
            }
          })
      } else {
        this.productService.saveProductInInventory(this.product)
          .subscribe({
            next: () => {
              this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Product Created', life: 3000 });
              this.getInventoryProduct();
            }
          })
        
      }
      this.product = {};
    }
  }

  findIndexById(id: string): number {
    let index = -1;
    for (let i = 0; i < this.products.length; i++) {
      if (this.products[i].id === id) {
        index = i;
        break;
      }
    }

    return index;
  }


  getSeverity(status: string | undefined) {
    switch (status) {
      case 'INSTOCK':
        return 'success';
      case 'LOWSTOCK':
        return 'warning';
      case 'OUTOFSTOCK':
        return 'danger';
      default:
        return ''
    }
  }
}
