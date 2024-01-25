import { Injectable } from '@angular/core';
import { AppHttpService } from '../../core/services/app-http.service';
import { Cart, UpdateQuantityResponse } from '../types';
import { CartItemCreate } from '../../shared/types';

@Injectable()
export class CartService {


  constructor(private http: AppHttpService) { }


  getCartItems(userId:string) {
   return this.http.get<Cart[]>(`api/cart/${userId}`)
  }

  updateCartItemQty(cartItem: CartItemCreate, cartId: string) {
    return this.http.patch<UpdateQuantityResponse>(`api/cart/${cartId}/update-quantity`, cartItem)
  }

  removeCartItem(cartId: string, userId: string) {
    return this.http.delete<Cart[]>(`api/cart/${userId}/remove/${cartId}`)
  }
}
