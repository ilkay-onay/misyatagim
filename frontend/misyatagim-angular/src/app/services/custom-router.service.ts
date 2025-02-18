import { Injectable } from '@angular/core';
import { Router, Route } from '@angular/router';
import { ProductService } from './product.service';
import { Product } from '../models/product.model';

@Injectable({
    providedIn: 'root'
})
export class CustomRouterService {
    private productSlugs: string[] = []; // Slug listesini saklayalım
    constructor(private router: Router, private productService: ProductService) { }

async loadProductRoutes(): Promise<void> {
    return new Promise<void>((resolve) => {
    this.productService.getProducts().subscribe(products => {
      if (products) {
        this.productSlugs = products.map(product => product.slug);
        this.productSlugs.forEach(slug => {
          this.addDynamicRoute(slug)
          });
        resolve()
     }
    });
   });
 }
    addDynamicRoute(slug: string): void {
         const routeExists = this.router.config.find(route => route.path === `product/${slug}`);

           if (!routeExists)
           {
             this.router.config.push({
                path: `product/${slug}`,
                loadComponent: () => import('../pages/product-detail/product-detail.component').then(c => c.ProductDetailComponent)
              } as Route);
           }
    }

    navigateToProduct(slug: string): void {
        this.router.navigate(['/product', slug]);
    }
}