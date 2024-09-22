import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private isLoggedInSubject = new BehaviorSubject<boolean>(false);

  
  isLoggedIn$: Observable<boolean> = this.isLoggedInSubject.asObservable();

  private apiUrl = 'http://localhost:5119/api'; // Replace with your actual API URL

  constructor(private http: HttpClient) {}

  public setLoggedIn(value: boolean): void {
    this.isLoggedInSubject.next(value);
  }
  
  login(loginData: { username: string; password: string }): Observable<any> {
    return this.http.post(`${this.apiUrl}/Auth/login`, loginData);
  }

  logout(): Observable<any> {
    return this.http.post(`${this.apiUrl}/Auth/logout`,{},{ responseType: 'text' }) as Observable<any>;
  }

  signup(signupData: { username: string; email: string; password: string; mobileNo: string }): Observable<any> {
    return this.http.post(`${this.apiUrl}/Auth/signup`, signupData,{ responseType: 'text' });
  }
}