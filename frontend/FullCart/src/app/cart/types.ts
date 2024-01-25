

export type Cart = {
  cartId: string;
  inventoryId: string;
  name: string;
  description: string;
  image: string;
  unitPrice: number;
  addedQty: number | undefined;
  totalItemPrice: number;
  loadingStatus:boolean
}


export type UpdateQuantityResponse = { canUpdate: boolean, availableStockQuantity: number }
