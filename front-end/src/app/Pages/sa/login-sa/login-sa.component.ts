import { Component } from '@angular/core';
import { LoginService } from '../../../Services/Login/login.service';
import { FormsModule, NgForm } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-login-sa',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './login-sa.component.html',
  styleUrl: './login-sa.component.scss'
})
export class LoginSaComponent {
  loginData: any = {
    user: '',
    password: ''
  }


  constructor(private login: LoginService) {}

  loginStatus: number = 3;


  submitForm(f: NgForm) {
    const user = this.loginData.user;
    const pass = this.loginData.password;
    this.login.login(user, pass).subscribe({
        next: (res) => {
            if (res && res.token) {
                this.login.setToken(res.token);
                this.loginData.user = '';
                this.loginData.password = '';
                this.loginStatus = 1;
              } else {
                  this.loginStatus = 2;
                  this.login.logout();
                }
              },
              error: (error) => {
                this.loginStatus = 2;
                this.login.logout();
                  console.error("Error on login", error);
                }
              })
              console.log(this.loginStatus);
  }


  logoutSa() {
    localStorage.removeItem('token');
  }

}

