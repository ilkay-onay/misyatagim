import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private tokenKey = 'authToken';
    private userSubject = new BehaviorSubject<string | null>(null);
    private identityServerUrl = 'http://localhost:5000/api/auth';
    private jwtHelper = new JwtHelperService();

    constructor(private http: HttpClient, private router: Router) {
        const token = localStorage.getItem(this.tokenKey);
        if (token) {
            this.userSubject.next(token);
        }
    }

    login(username: string, password: string): Observable<{ token: string }> {
        return this.http.post<{ token: string }>(`${this.identityServerUrl}/login`, { username, password }, { withCredentials: true }).pipe(
            tap(response => {
                localStorage.setItem(this.tokenKey, response.token);
                this.userSubject.next(response.token);
                 this.redirectBasedOnRole();
           })
      );
    }

    logout() {
        localStorage.removeItem(this.tokenKey);
        this.userSubject.next(null);
        this.router.navigate(['/login']);
    }

    isAuthenticated(): boolean {
        const token = this.getToken();
        return token !== null && !this.jwtHelper.isTokenExpired(token);
    }

    isAdmin(): boolean {
        const token = this.getToken();
        if (token) {
            const decodedToken = this.jwtHelper.decodeToken(token);
            const roleClaim = decodedToken && decodedToken['role'];
            console.log('Decoded token:', decodedToken);
            console.log('Role claim:', roleClaim);
            const isAdmin = roleClaim === 'admin';
            console.log("isAdmin:", isAdmin)
            return isAdmin
        }
        return false;
    }
   redirectBasedOnRole() {
       if (this.isAdmin()) {
           this.router.navigate(['/admin']);
       } else {
           this.router.navigate(['/']);
       }
   }

getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
}
register(username: string, password: string, role:string): Observable<any> {
        return this.http.post(`${this.identityServerUrl}/register`, { username, password, role },{ withCredentials: true });
    }
    getUserInfo():any {
        const token = this.getToken();
          if (token) {
            const decodedToken = this.jwtHelper.decodeToken(token);
           return decodedToken;
          }
          return null;

    }

}