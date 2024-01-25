import { Inject, Injectable } from '@angular/core';
import { BASE_API_URL } from '../../app.module';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class AppHttpService {

  constructor(@Inject(BASE_API_URL) private baseUrl: string, private http: HttpClient) { }


  // GET request
  get<T>(url: string): Observable<T> {
    return this.http.get<T>(`${this.baseUrl}/${url}`);
  }

  // POST request
  post<T>(url: string, data: unknown): Observable<T> {
    return this.http.post<T>(`${this.baseUrl}/${url}`, data);
  }

  // PUT request
  put<T>(url: string, data: unknown): Observable<T> {
    return this.http.put<T>(`${this.baseUrl}/${url}`, data);
  }

  patch<T>(url: string, data: unknown): Observable<T> {
    return this.http.patch<T>(`${this.baseUrl}/${url}`, data);
  }

  // DELETE request
  delete<T>(url: string): Observable<T> {
    return this.http.delete<T>(`${this.baseUrl}/${url}`);
  }
}
