import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { CoreModule } from '../core/core.module';
import { SharedModule } from '../shared/shared.module';
import { CartListComponent } from './component/cart-list/cart-list.component';
import { CartService } from './service/cart.service';
import { ConfirmationService } from 'primeng/api';
const routes: Routes = [
  {
    path:'', component:CartListComponent
  }
]


@NgModule({
  declarations: [
    CartListComponent
  ],
  imports: [
    CommonModule,
    CoreModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    RouterModule.forChild(routes)

  ],
  providers: [CartService, ConfirmationService]
})
export class CartModule { }
