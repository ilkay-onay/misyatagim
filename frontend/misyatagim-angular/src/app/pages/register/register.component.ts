import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { Router } from '@angular/router';
import { MatSelectModule } from '@angular/material/select';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';


@Component({
    selector: 'app-register',
    standalone: true,
    imports: [CommonModule, FormsModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatCardModule, MatSelectModule, ReactiveFormsModule],
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.css']
})
export class RegisterComponent {
    registerForm: FormGroup;
    errorMessage: string | null = null;
    roles: string[] = ['admin', 'user'];

    constructor(private authService: AuthService, private router: Router, private fb: FormBuilder) {
        this.registerForm = this.fb.group({
            username: ['', Validators.required],
            password: ['', Validators.required],
            role: ['', Validators.required],
        });
    }

    register() {
        if (this.registerForm.valid) {
            this.authService.register(this.registerForm.value.username, this.registerForm.value.password, this.registerForm.value.role).subscribe({
                next: () => {
                    this.errorMessage = null;
                    this.router.navigate(['/login']);
                },
                error: (error) => {
                    this.errorMessage = 'Kayıt olurken bir sorun oluştu. Lütfen girdiğiniz bilgileri kontrol ediniz.';
                    console.error('Register Error:', error);
                }
            });
        }
        else {
            this.errorMessage = 'Lütfen tüm alanları doldurunuz.';
        }
    }

}