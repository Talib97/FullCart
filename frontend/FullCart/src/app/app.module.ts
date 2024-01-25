import { InjectionToken, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AvatarModule } from 'primeng/avatar';
import { AvatarGroupModule } from 'primeng/avatargroup';
import { ChipModule } from 'primeng/chip';
import { MenubarModule } from 'primeng/menubar';
import { ToastModule } from 'primeng/toast';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TopBarComponent } from './app.topbar.component';
import { CoreModule } from './core/core.module';
import { InitialsPipe } from './pipe/initials.pipe';
import { BadgeModule } from 'primeng/badge'
import { SharedModule } from './shared/shared.module';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService } from 'primeng/api';

export const BASE_API_URL = new InjectionToken<string>('BASE_API_URL')

@NgModule({
  declarations: [
    AppComponent,
    TopBarComponent,
    InitialsPipe
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    CoreModule,
    AppRoutingModule,
    ToastModule,
    MenubarModule,
    ChipModule,
    AvatarModule,
    AvatarGroupModule,
    BadgeModule,
    SharedModule,
    ConfirmDialogModule
  ],
  providers: [{
    provide: BASE_API_URL, useValue:"http://localhost:5048"
  },
    ConfirmationService, TopBarComponent],
  bootstrap: [AppComponent]
})
export class AppModule { }
