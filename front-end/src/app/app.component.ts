import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { DefaultNavbarComponent } from './Components/default-navbar/default-navbar.component';
import { StatusService } from './Services/status.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, DefaultNavbarComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
  providers: [StatusService]
})
export class AppComponent {
  title = 'front-end';

  ngOnInit() {
    localStorage.removeItem('token');
  }
}

