import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { CoreModule } from '../core/core.module';
import { SharedModule } from '../shared/shared.module';
import { ProductListComponent } from './component/product-list/product-list.component';
import { ProductService } from './service/product.service';
import { ManageProductComponent } from './component/manage-product/manage-product.component';
import { ManageCategoryComponent } from './component/manage-category/manage-category.component';
import { ConfirmationService } from 'primeng/api';

const routes: Routes = [
  {
  path: '', component: ProductListComponent
} ,
 {
  path: 'manage-product', component: ManageProductComponent,
  },
  {
    path: 'manage-category', component: ManageCategoryComponent
  }

]


@NgModule({
  declarations: [
    ProductListComponent,
    ManageProductComponent,
    ManageCategoryComponent
  ],
  imports: [
    CommonModule,
    CoreModule,
    FormsModule,
    RouterModule.forChild(routes),
    ReactiveFormsModule,
    SharedModule

  ],
  providers: [ProductService, ConfirmationService]
})
export class ProductModule { }
