import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CoreModule } from '../core/core.module';
import { SharedModule } from '../shared/shared.module';
import { OrderListComponent } from './component/order-list/order-list.component';
import { OrderService } from './service/order.service';

const routes: Routes = [{
  path:'',component:OrderListComponent
}]

@NgModule({
  declarations: [
    OrderListComponent
  ],
  imports: [
    CommonModule,
    CoreModule,
    SharedModule,
    RouterModule.forChild(routes),
  ],
  providers: [OrderService]
})
export class OrderModule { }
