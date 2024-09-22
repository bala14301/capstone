import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthService } from '../AuthService';

@Injectable({
  providedIn: 'root'
})
export class RouterService implements CanActivate {
  constructor(private router: Router, public authService: AuthService) {}
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const token = sessionStorage.getItem('authToken');
    const isLoginPage = state.url === '/login';
    const checkIfLoggedIn = route.data['checkIfLoggedIn'];

    if (token) {  
      this.authService.setLoggedIn(true);
      if (isLoginPage && !checkIfLoggedIn) {
        this.router.navigate(['/dashboard']);
        return false;
      }
      return true;
    } else {
      if (!isLoginPage) {
        this.router.navigate(['/login']);
        return false;
      }
      return true;
    }
  }
}