import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  private url = "http://localhost:4652";


  constructor(private httpClient: HttpClient) { }
  
  login(username: string, password: string) {
    return this.httpClient.post<any>(`${this.url}/login`, { user: username, password: password });
  }

  logout() {
    if (typeof localStorage !== 'undefined') {
      localStorage.removeItem('token');
    }
  }

  getToken() {
    if (typeof localStorage !== 'undefined') {
      return localStorage.getItem('token');
    }
    return null; 
  }

  setToken(tokenSet: string) {
    if (typeof localStorage !== 'undefined') {
      localStorage.setItem('token', tokenSet);
    }
  }

  isAuthenticated(): boolean {
    const token = this.getToken();
    return !!token;
  }

}

