import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { RouterModule } from '@angular/router';
import { ProductService } from '../../services/product.service';
import { Product } from '../../models/product.model';
import { HighlightDirective } from '../../directives/highlight.directive';
import { TruncatePipe } from '../../pipes/truncate.pipe';
import { CustomRouterService } from '../../services/custom-router.service';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule, MatToolbarModule, MatCardModule, MatButtonModule, RouterModule, HighlightDirective, TruncatePipe],
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {
  products: Product[] = [];

  constructor(private productService: ProductService, private customRouter:CustomRouterService) { }

  ngOnInit(): void {
    this.productService.getProducts().subscribe(data => {
      this.products = data;
    });
  }
 navigateToProduct(slug:string) {
  this.customRouter.navigateToProduct(slug);
}
}