// product-form.component.ts
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormGroup, FormBuilder, Validators, FormArray } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { ProductService } from '../../services/product.service';
import { Product } from '../../models/product.model';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { MatSelectModule } from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';
import { slugify } from '../../utils/slugify';

@Component({
  selector: 'app-product-form',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule, MatInputModule, MatFormFieldModule, MatButtonModule, RouterModule, MatSelectModule, MatIconModule],
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.css']
})
export class ProductFormComponent implements OnInit {
  productForm: FormGroup;
  productId: number | null = null;
  isEditMode = false;
  selectedFiles: File[] = [];
  imagePreviews: string[] = [];

  constructor(
    private fb: FormBuilder,
    private productService: ProductService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.productForm = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      price: [0, [Validators.required, Validators.min(0)]],
      size: [''],
      material: [''],
      color: [''],
      firmness: [''],
      imageBase64s: this.fb.array([])
    });
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      if (params['id']) {
        this.productId = +params['id'];
        this.isEditMode = true;
        this.loadProduct(this.productId);
      }
    });
  }

  loadProduct(id: number): void {
    this.productService.getProductById(id).subscribe({
        next: (product) => {
            this.productForm.patchValue(product);
            if (product.imageBase64s) {
                this.imagePreviews = product.imageBase64s.map(x => 'data:image/png;base64,' + x);
                this.productForm.setControl('imageBase64s', this.fb.array(product.imageBase64s));
            }
        },
        error: (error) => {
            console.error('Ürün yüklenirken hata:', error);
            alert('Ürün bulunamadı veya yüklenirken bir hata oluştu.');
            this.router.navigate(['/admin/products']); // Hata durumunda ürün listesine yönlendir
        }
    });
}

  onFileChange(event: any): void {
    this.selectedFiles = Array.from(event.target.files);
    this.imagePreviews = [];
    this.imageBase64s.clear();
    for (const file of this.selectedFiles) {
      this.convertFileToBase64(file);
    }
  }

  convertFileToBase64(file: File): void {
    const reader = new FileReader();
    reader.onload = (e: any) => {
      this.imagePreviews.push(e.target.result);
      const control = this.fb.control(e.target.result.split(',')[1]);
      this.imageBase64s.push(control);
    };
    reader.readAsDataURL(file);
  }

  removeImageBase64Input(index: number): void {
    this.imageBase64s.removeAt(index);
    this.selectedFiles.splice(index, 1);
    this.imagePreviews.splice(index, 1);
  }

  get imageBase64s(): FormArray {
    return this.productForm.get('imageBase64s') as FormArray;
  }

  onSubmit(): void {
    if (this.productForm.valid) {
      const product: Product = this.productForm.value;
      product.slug = slugify(product.name);

      if (this.isEditMode && this.productId) {
        product.id = this.productId;
        this.productService.updateProduct(product).subscribe({
          next: () => {
            this.router.navigate(['/admin/products']);
          },
          error: (error) => {
            console.error('Güncelleme hatası', error);
          }
        });
      } else {
        this.productService.createProduct(product).subscribe({
          next: () => {
            this.router.navigate(['/admin/products']);
          },
          error: (error) => {
            console.error('Ekleme hatası', error);
          }
        });
      }
    }
  }
}