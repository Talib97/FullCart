import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from './core/auth/services/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'FullCart';
  userService = inject(UserService)
  constructor() {

  }

}
