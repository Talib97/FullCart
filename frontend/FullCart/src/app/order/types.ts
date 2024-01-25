export type Order =  {
  orderId: string;
  orderStatus: string;
  orderDate: Date;
  orderDetail: OrderDetail[];
}

export type OrderDetail = {
  name: string;
  image: string;
  orderedQty:number
}


