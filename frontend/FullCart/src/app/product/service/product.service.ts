import { Inject, Injectable } from '@angular/core';
import { BASE_API_URL } from '../../app.module';
import { AppHttpService } from '../../core/services/app-http.service';
import { Category, InventoryProduct, Status } from '../../core/auth/types';
import { map } from 'rxjs';

@Injectable()
export class ProductService {

  constructor(private http:AppHttpService) { }

  getInventoryProduct(categoryId?:string,sortOrder?:string,searchTerm?:string) {
    return this.http.get<InventoryProduct[]>(`api/inventory/product?categoryId=${categoryId}&sortOrder=${sortOrder}&searchTerm=${searchTerm}`)
  }

  getProductCategory() {
    return this.http.get<Category[]>('api/category')
      .pipe(map(c => [{ id: '', name: 'All' }, ...c]))
  }

  getInventoryStatus() {
    return this.http.get<Status[]>('api/inventory/status')
  }

  saveProductInInventory(product:InventoryProduct) {
    return this.http.post<void>('api/inventory', product)
  }

  updateProductInInventory(product: InventoryProduct) {
    return this.http.put<void>('api/inventory', product)
  }

  deleteProductInInventory(inventoryId:string) {
    return this.http.delete<void>(`api/inventory/${inventoryId}`)
  }
}
