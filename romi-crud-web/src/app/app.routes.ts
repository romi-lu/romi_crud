import { Routes } from '@angular/router';
import { authGuard } from './core/auth.guard';

export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'personas' },
  {
    path: 'login',
    loadComponent: () =>
      import('./pages/login/login.component').then((m) => m.LoginComponent)
  },
  {
    path: 'personas',
    canActivate: [authGuard],
    loadComponent: () =>
      import('./pages/persons/persons.component').then((m) => m.PersonsComponent)
  },
  { path: '**', redirectTo: 'personas' }
];
