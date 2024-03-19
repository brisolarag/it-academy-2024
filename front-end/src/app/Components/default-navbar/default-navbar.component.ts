import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../../Services/Login/login.service';
import { StatusService } from '../../Services/status.service';

@Component({
  selector: 'app-default-navbar',
  standalone: true,
  imports: [],
  templateUrl: './default-navbar.component.html',
  styleUrl: './default-navbar.component.scss'
})
export class DefaultNavbarComponent {

  logoSrc: string = 'assets/navbar/delllogo.jpeg';

  changeImage(newSrc: string) {
    this.logoSrc = newSrc;
  }
  constructor(private router: Router, private loginService: LoginService) {}

  navigateToApostar() {
    this.router.navigateByUrl('/apostar');
  }

  navigateToListaApostas() {
    this.router.navigateByUrl('/apostas');
  }
  
  navigateToResultado() {
    this.router.navigateByUrl('/resultado');
  }
  navigateToSaMenu() {
    this.router.navigateByUrl('/samenu');
  }
  navigateToLoginSa() {
    this.router.navigateByUrl('/loginsa');
  }

  get token() {
    return this.loginService.getToken();
  }

  logoutSa() {
    this.loginService.logout();
  }

}
