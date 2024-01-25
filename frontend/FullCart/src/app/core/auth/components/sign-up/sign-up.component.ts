import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthRequest } from '../../types';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { HttpErrorResponse } from '@angular/common/http';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {

  signUpForm = new FormGroup({
    name: new FormControl<string>('', Validators.required),
    email: new FormControl<string>('', Validators.email),
    password: new FormControl<string>('', Validators.required),
    confirmPassword:new FormControl<string>('',Validators.required)
  })

  signUpBtnLoader: boolean = false;

  constructor(private authService: AuthService,
    private router: Router,
    private messageService: MessageService,
    private userService:UserService) { }

  ngOnInit() {
    if (!!this.userService.getUser()) this.router.navigate(['/'])
  }


  onSignUp() {
    if (this.signUpForm.invalid) return;  

    this.signUpBtnLoader = true
    const request: AuthRequest = { ...this.signUpForm.value }
    this.authService.signUp(request).subscribe({
      next: (authRespone) => {
        this.userService.setUser(authRespone)
        this.router.navigate(['product'])
      },
      error: (error:HttpErrorResponse) => {
        this.messageService.add({
          severity: 'error',
          summary:'Error',
          detail: error?.message
        })
        this.signUpBtnLoader = false
      }
    })
  }

}
