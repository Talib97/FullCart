import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './core/auth/components/login/login.component';
import { SignUpComponent } from './core/auth/components/sign-up/sign-up.component';


const routes: Routes = [
  {
    path: '',
    redirectTo: '/login',
    pathMatch:'full'
  },
  {
  path: 'product',
  loadChildren: () => import("./product/product.module").then(p => p.ProductModule)
  },
  {
    path: 'cart',
    loadChildren: () => import("./cart/cart.module").then(c => c.CartModule)
  },
  {
    path: 'order',
    loadChildren: () => import("./order/order.module").then(o => o.OrderModule)
  },
  {
    path: 'login',
    component:LoginComponent
  },
  {
    path: 'register',
    component: SignUpComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
