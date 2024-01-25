import { Component, inject } from '@angular/core';
import { CartService } from '../../service/cart.service';
import { UserService } from '../../../core/auth/services/user.service';
import { Cart } from '../../types';
import { SharedService } from '../../../shared/service/shared.service';
import { CartItemCreate, CreateOrder } from '../../../shared/types';
import { DropdownChangeEvent } from 'primeng/dropdown';
import { ConfirmationService, MessageService } from 'primeng/api';
import { map } from 'rxjs';
import { SharedObservableService } from '../../../shared/service/shared-observable.service';
import { TopBarComponent } from '../../../app.topbar.component';
import { FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cart-list',
  templateUrl: './cart-list.component.html',
  styleUrls: ['./cart-list.component.css']
})
export class CartListComponent {

  cartService = inject(CartService)
  userService = inject(UserService)
  messageService = inject(MessageService)
  confirmationService = inject(ConfirmationService)
  cart: Cart[] | undefined
  maxOrderQuantity: number[] = [1, 2, 3, 4]
  sharedObsService = inject(SharedObservableService)
  topBarComponent = inject(TopBarComponent)
  sharedService = inject(SharedService)
  router = inject(Router)
  showAddressForm = false
  shippingAddressControl = new FormControl('', Validators.required)
  placeOrderBtnLoader: boolean = false;


  ngOnInit() {
    this.getCartItem()
  }


  getCartItem() {
    const user = this.userService.getUser()
    this.cartService.getCartItems(user?.userInfo?.id)
      .pipe(
        map(this.calculateTotalItemPrice)
    )
      .subscribe({
        next: (cart) => {
          this.cart = cart
        }
      })
  }

  onChangeItemQty(event: DropdownChangeEvent, cartItem: Cart) {

    const cartItemRequest: CartItemCreate = {
      userId: this.userService.getUser().userInfo.id,
      addedQuantity: event?.value,
      inventoryId: cartItem.inventoryId
    }
    this.updateItemQuantity(cartItemRequest, cartItem, event);
  }

  updateItemQuantity(cartItemRequest: CartItemCreate, cartItem: Cart, event: DropdownChangeEvent) {
    this.cartService.updateCartItemQty(cartItemRequest, cartItem.cartId)
      .subscribe({
        next: ({ canUpdate, availableStockQuantity }) => {

          if (!canUpdate) {
            this.messageService.add({
              severity: 'error',
              summary: 'Error',
              detail: `Can't update quantity, available stock quantity ${availableStockQuantity}`
            });
            return;
          }
          cartItem.addedQty = event?.value;
          cartItem.totalItemPrice = cartItem.unitPrice * event?.value;
          this.calculateTotalCartAmpunt();
        },
        error: () => {
          this.messageService.add({
            severity: 'error',
            summary: 'Error',
            detail: 'Something went wrong!, unable to update item to cart'
          });
        }
      }); 
  }

  calculateTotalCartAmpunt() {
  return this.cart?.reduce((accumulator, currentValue) => {
      return accumulator + currentValue.totalItemPrice
    },0)
  }

  calculateTotalItemPrice(cart:Cart[]) {
    return cart.map(c => ({
      ...c,
      totalItemPrice: (c.addedQty! * c.unitPrice)
    }))
  }

  onRemoveCartItem(cartItem: Cart, userId: string) {
    cartItem.loadingStatus = true
    this.cartService.removeCartItem(cartItem.cartId, userId).pipe(
      map(this.calculateTotalItemPrice)
    )
      .subscribe({
        next: (cart) => {
          this.cart = cart
          cartItem.loadingStatus = false
          this.topBarComponent.updateCartCount()
        },
        error: () => {
          this.messageService.add({
            severity: 'error',
            summary: 'Error',
            detail: 'Something went wrong!, unable to remove item from cart'
          })
        }
      })
  }


  onRemoveConfirmation(cartItem: Cart) {
    this.confirmationService.confirm({
      message: 'Are you sure, you want to remove?',
      header: 'Confirmation',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.onRemoveCartItem(cartItem, this.userService.getUser().userInfo.id)
      }
    })
  }

  createOrder() {
    this.placeOrderBtnLoader = true
    const orderRequest: CreateOrder = {
      userId: this.userService.getUser().userInfo.id,
      shippingAddress: this.shippingAddressControl.value!,
      orderPrice: this.calculateTotalCartAmpunt()
    }

    this.sharedService.createOrder(orderRequest)
      .subscribe({
        next: () => {
          this.showAddressForm = false
          this.placeOrderBtnLoader = false
          this.messageService.add({
            severity: 'success',
            summary: 'Success',
            detail: `Order Created Successfully !`
          });
          this.router.navigate(['order'])
        },
        error: () => {
          this.messageService.add({
            severity: 'error',
            summary: 'Error',
            detail: 'Something went wrong!, unable to create order !'
          })
          this.placeOrderBtnLoader = false
        }
      })
  }
  
}
