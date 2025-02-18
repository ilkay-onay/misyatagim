import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { HttpErrorResponse } from '@angular/common/http';
import { CommonModule } from '@angular/common'; // Import CommonModule


@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css'],
    standalone: true,
    imports: [FormsModule, MatCardModule, MatFormFieldModule, MatInputModule, MatButtonModule, CommonModule] // Add CommonModule here
})
export class LoginComponent implements OnInit {
    username = '';
    password = '';
    loginError: string | null = null;

    constructor(private authService: AuthService, private router: Router) { }

    ngOnInit() {
        if (this.authService.isAuthenticated()) {
            this.authService.redirectBasedOnRole();
        }
    }

    login() {
        this.loginError = null;
        this.authService.login(this.username, this.password).subscribe({
            next: () => {
                // Redirection is handled by AuthService
            },
            error: (error: HttpErrorResponse) => {
                this.loginError = 'Login failed. Please check your credentials.';
                console.error('Login Error:', error);
            }
        });
    }
}