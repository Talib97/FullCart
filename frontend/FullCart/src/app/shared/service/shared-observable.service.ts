import { Injectable, inject } from '@angular/core';
import { BehaviorSubject, lastValueFrom } from 'rxjs';
import { UserService } from '../../core/auth/services/user.service';
import { SharedService } from './shared.service';

@Injectable()
export class SharedObservableService {

  public updateCartNotifyEvent = new BehaviorSubject("0")

  private user = inject(UserService).getUser();
  private sharedService = inject(SharedService);

  async updateCartCount() {
    const count = await lastValueFrom(this.sharedService.getCartItemCount(this.user?.userInfo?.id))
    this.updateCartNotifyEvent.next(count?.toString())
  }
}
