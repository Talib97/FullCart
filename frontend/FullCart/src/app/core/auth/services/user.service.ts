import { Injectable } from '@angular/core';
import { AuthResponse } from '../types';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor() { }


  getUser() {
    return JSON.parse(sessionStorage.getItem("User")!) as AuthResponse
  }

  setUser(authUser: AuthResponse) {
    sessionStorage.setItem("User", JSON.stringify(authUser))
  }

  removeUser() {
    sessionStorage.removeItem("User")
  }
}
