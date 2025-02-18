import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Injectable({
    providedIn: 'root'
})
export class AuthGuard implements CanActivate {
    constructor(private authService: AuthService, private router: Router) { }

     canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        if (state.url === '/login' && this.authService.isAuthenticated()) {
            this.authService.redirectBasedOnRole();
            return false;
        }

         if (state.url.startsWith('/admin') && !this.authService.isAdmin()) {
             this.router.navigate(['/']);
             return false;
         }
         // Allow access to the home page ("/") without authentication.
        if (state.url === '/' || state.url === '/register' || state.url === '/login' )
        {
           return true;
        }
         if (!this.authService.isAuthenticated() ) {
             this.router.navigate(['/login']);
             return false;
         }

        return true;
    }
}