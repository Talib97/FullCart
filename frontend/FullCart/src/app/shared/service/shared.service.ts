import { Injectable, inject } from '@angular/core';
import { AppHttpService } from '../../core/services/app-http.service';
import { CartItemCreate, CreateOrder } from '../types';

@Injectable()
export class SharedService {

  http = inject(AppHttpService)



  addItemToCart(cartItemRequest: CartItemCreate) {
    return this.http.post<void>('api/cart', cartItemRequest)
  }
  getCartItemCount(userId: string) {
    return this.http.get<number>(`api/cart/${userId}/count`)
  }

  createOrder(order: CreateOrder) {
    return this.http.post<void>(`api/order`, order)
  }
}
