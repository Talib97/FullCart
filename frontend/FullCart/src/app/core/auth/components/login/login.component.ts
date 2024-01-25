import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthRequest } from '../../types';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { MessageService } from 'primeng/api';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm = new FormGroup({
    email: new FormControl<string>('', Validators.email),
    password: new FormControl<string>('', Validators.required),
  })
  loginBtnLoader = false

  constructor(private authService: AuthService,
    private router: Router,
    private messageService: MessageService,
    private userService:UserService) {

  }

  ngOnInit() {
    if (!!this.userService.getUser()) this.router.navigate(['product'])
  }

  onLogIn() {
    if (this.loginForm.invalid) return;

    this.loginBtnLoader = true
    const request: AuthRequest = { ...this.loginForm.value }
    this.authService.login(request).subscribe({
      next: (authRespone) => {
        this.userService.setUser(authRespone)
        this.router.navigate(['product'])
      },
      error: (error: HttpErrorResponse) => {
        this.messageService.add({
          severity: 'error',
          summary: 'Error',
          detail: error?.error?.message
        })
        this.loginBtnLoader = false
      }
    })
  }

}
