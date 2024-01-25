export type CartItemCreate = {
  userId: string;
  addedQuantity: number;
  inventoryId: string;
}

export type CreateOrder = {
  userId: string;
  shippingAddress?: string;
  orderPrice?: number;
}
