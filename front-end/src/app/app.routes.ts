import { Routes, withViewTransitions } from '@angular/router';
import { ApostarPageComponent } from './Pages/apostar-page/apostar-page.component';
import { ListarApostasPageComponent } from './Pages/listar-apostas-page/listar-apostas-page.component';
import { ResultadoPageComponent } from './Pages/resultado-page/resultado-page.component';
import { SaMenuComponent } from './Pages/sa/sa-menu/sa-menu.component';
import { LoginSaComponent } from './Pages/sa/login-sa/login-sa.component';
import { AuthGuard } from './auth.guard';

export const routes: Routes = [
    { path: 'apostar', component: ApostarPageComponent },
    { path: 'apostas', component: ListarApostasPageComponent },
    { path: 'resultado', component: ResultadoPageComponent },
    { path: 'samenu', component: SaMenuComponent, canActivate: [AuthGuard] },
    { path: 'loginsa', component: LoginSaComponent },
    { path: '', redirectTo: '/apostar', pathMatch: 'full' },
];
