import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { UserService } from './core/auth/services/user.service';
import { SharedObservableService } from './shared/service/shared-observable.service';
import { SharedService } from './shared/service/shared.service';

@Component({
  selector: 'top-bar',
  templateUrl: './app.topbar.component.html',
})

export class TopBarComponent {

  items: MenuItem[] | undefined;
  router = inject(Router)
  userService = inject(UserService)
  sharedObsService = inject(SharedObservableService)
  sharedService = inject(SharedService)
  user = this.userService.getUser()?.userInfo
  cartCount!: string;
  constructor() {}

  ngOnInit() {
    this.items = [
      {
        label: 'Products',
        command: () => this.router.navigate(['product']),
        state: {
          showOnLogin: true
        }
      },
      {
        label: 'My Order',
        command: () => this.router.navigate(['order']),
        state: {
          showOnLogin: true
        }
      },
      {
        label: 'Manage Product',
        command: () => this.router.navigate(['product/manage-product']),
        state: {
          onRole:'ADMIN',
          showOnLogin: true
        },
        visible:false
      },
      {
        label: 'Logout',
        command: () => this.logOut(),
        state: {
          showOnLogin: true
        }
      },
    ];
    this.updateCartCount()
  }


  updateCartCount() {
    this.sharedService.getCartItemCount(this.user?.id)
      .subscribe(value => {
        this.cartCount = value?.toString()
      })
  }

  logOut() {
    this.userService.removeUser()
    this.router.navigate(['login'])
  }


  getMenuItem(items: MenuItem[] | undefined) {
    // items!.forEach(item => {
    //   if (this.userService.getUser() && item?.state?.['showOnLogin']) {
    //      item.visible = true
    //   } else {
    //      item.visible = false
    //   }
    // });
    //return items

    //items?.forEach(item => {
    //  if (item.state && item.state?.['showOnLogin']) {
    //    if (item.state?.['onRole'] === 'ADMIN') {
    //      const userRoles = this.userService.getUser().userInfo?.roles;
    //      if (userRoles && userRoles.includes('ADMIN')) {
    //        item = { ...item, visible: true };
    //      }
    //    } else {
    //      item =  { ...item, visible: true };
    //    }
    //  }
    //  item =  { ...item, visible: false };
    //})
    //return items

    //return items?.map(item => {
    //  if (item.state && item.state?.['showOnLogin']) {
    //    if (item.state?.['onRole'] === 'ADMIN') {
    //      const roles = this.userService.getUser().userInfo?.roles;
    //      if (roles && roles.includes(item.state?.['onRole'])) {
    //        return { ...item, visible: true };
    //      }
    //    }
    //  }
    //  return { ...item, visible: false };
    //});


  //  return items?.map(item => {
  //    if (item.state && item.state?.['showOnLogin']) {
  //      if (item.state?.['onRole'] === 'ADMIN') {
  //        const userRoles = this.userService.getUser().userInfo?.roles;
  //        if (userRoles && userRoles.includes('ADMIN')) {
  //          return { ...item, visible: true };
  //        }
  //      } else {
  //        return { ...item, visible: true };
  //      }
  //    }
  //    return { ...item, visible: false };
    //  });


    items?.forEach(item => {
      if (item.state && item.state?.['showOnLogin']) {
        if (item.state?.['onRole'] === 'ADMIN') {
          const userRoles = this.userService.getUser().userInfo?.roles;
          item.visible = userRoles && userRoles.includes('ADMIN');
        } else {
          item.visible = true;
        }
      } else {
        item.visible = false;
      }
    });

    return items;

  }


}
