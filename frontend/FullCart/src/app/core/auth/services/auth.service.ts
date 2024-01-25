import { Inject, Injectable } from '@angular/core';
import { BASE_API_URL } from '../../../app.module';
import { HttpClient } from '@angular/common/http';
import { AuthRequest, AuthResponse } from '../types';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(@Inject(BASE_API_URL) private baseUrl: string,private http:HttpClient) { }

  signUp(request:AuthRequest) {
    return this.http.post<AuthResponse>(`${this.baseUrl}/api/auth/signup`, request)
  }

  login(request: AuthRequest) {
    return this.http.post<AuthResponse>(`${this.baseUrl}/api/auth/login`, request)
  }
}
