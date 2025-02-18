import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ProductService } from '../../services/product.service';
import { Product } from '../../models/product.model';
import { MatCardModule } from '@angular/material/card';
import { TruncatePipe } from '../../pipes/truncate.pipe'; // Import the TruncatePipe

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    CommonModule,
    MatToolbarModule,
    MatButtonModule,
    MatCardModule,
    RouterModule,
    TruncatePipe // Add TruncatePipe here
  ],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  isLoggedIn: boolean = false;
  username: string | null = null;
  products: Product[] = [];
  jwtHelper = new JwtHelperService();

  constructor(
    private authService: AuthService,
    private router: Router,
    private productService: ProductService
  ) { }

  ngOnInit(): void {
    this.isLoggedIn = this.authService.isAuthenticated();
    if (this.isLoggedIn) {
      const token = this.authService.getToken();
      if (token) {
        const decodedToken = this.jwtHelper.decodeToken(token);
        this.username = decodedToken ? decodedToken['unique_name'] : null;
      }
    }
    this.loadProducts();
  }

  loadProducts(): void {
    this.productService.getProducts().subscribe({
      next: (data) => {
        this.products = data;
      },
      error: (error) => {
        console.error('Ürünler yüklenirken hata:', error);
      }
    });
  }

  navigateToProduct(slug: string): void {
    this.router.navigate(['/product', slug]);
  }

  logout(): void {
    this.authService.logout();
  }
}