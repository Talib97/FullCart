import { Injectable } from '@angular/core';
import { AppHttpService } from '../../core/services/app-http.service';
import { Order } from '../types';
import { CreateOrder } from '../../shared/types';

@Injectable()
export class OrderService {

  constructor(private http:AppHttpService) { }

  getOrder(userId:string) {
    return this.http.get<Order[]>(`api/order/${userId}`)
  }


  cancelOrder(orderId: string, order: CreateOrder) {
    return this.http.patch(`api/order/${orderId}/cancel`, order)
  }

}
