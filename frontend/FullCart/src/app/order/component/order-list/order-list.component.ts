import { Component, OnInit, inject } from '@angular/core';
import { OrderService } from '../../service/order.service';
import { CreateOrder } from '../../../shared/types';
import { UserService } from '../../../core/auth/services/user.service';
import { Order } from '../../types';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-order-list',
  templateUrl: './order-list.component.html',
  styleUrls: ['./order-list.component.css']
})
export class OrderListComponent implements OnInit {
  orderService = inject(OrderService)
  userService = inject(UserService)
  orders!:Order[]

  constructor(private messageService: MessageService) { }

  ngOnInit() {
    this.getOrder()
  }


  getOrder() {
    this.orderService.getOrder(this.userService.getUser().userInfo.id)
      .subscribe(orders => {
        this.orders = orders
      })
  }

  onCancelOrder(order: Order) {
    const orderRequest: CreateOrder = {
      userId: this.userService.getUser().userInfo.id,
    }
    this.orderService.cancelOrder(order.orderId, orderRequest)
      .subscribe({
        next: () => {
          this.messageService.add({
            severity: 'success',
            summary: 'Success',
            detail: `Order Cancelled!`
          });
          this.getOrder()
        },
        error: () => {
          this.messageService.add({
            severity: 'error',
            summary: 'Error',
            detail: `Something went wrong, can't cancel order!`
          });
        }
      })
  }
  
}
