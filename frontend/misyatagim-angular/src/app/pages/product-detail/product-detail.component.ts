import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductService } from '../../services/product.service';
import { Product } from '../../models/product.model';
import { CommentService } from '../../services/comment.service';
import { Comment } from '../../models/comment.model';
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';

@Component({
    selector: 'app-product-detail',
    standalone: true,
    imports: [
        CommonModule,
        MatToolbarModule,
        MatCardModule,
        MatButtonModule,
        FormsModule,
        MatFormFieldModule,
        MatInputModule,
        MatListModule,
        MatIconModule
    ],
    templateUrl: './product-detail.component.html',
    styleUrls: ['./product-detail.component.css']
})
export class ProductDetailComponent implements OnInit {
    product: Product | undefined;
    commentText = '';
    comments: Comment[] = [];
    isLoggedIn = false;
    userInfo: any | null = null;

    constructor(
        private route: ActivatedRoute,
        private productService: ProductService,
        private commentService: CommentService,
        private authService: AuthService
    ) { }

    ngOnInit(): void {
        this.isLoggedIn = this.authService.isAuthenticated();
         this.userInfo = this.authService.getUserInfo();
        const slug = this.route.snapshot.paramMap.get('slug');
        if (slug) {
            this.productService.getProductBySlug(slug).subscribe(data => {
                this.product = data;
                if (this.product) {
                    this.loadComments(this.product.id);
                }
            });
        }
    }

    loadComments(productId: number): void {
      this.commentService.getComments(productId).subscribe(comments => {
            console.log("Comments:", comments);
            this.comments = comments;
      });
    }

    onSubmit(): void {
        if (this.product) {
            const comment: Comment = {
                productId: this.product.id,
                text: this.commentText,
                 userId: this.userInfo?.nameid,
                username: this.userInfo?.unique_name
            };
            this.commentService.addComment(comment).subscribe(() => {
                this.commentText = '';
               this.loadComments(this.product?.id ?? 0);
                 alert('Yorumunuz eklendi!');
            });
        }
    }
  deleteComment(comment: Comment): void {
        if (comment.id) {
            this.commentService.deleteComment(comment.id).subscribe(success => {
                if (success) {
                  this.loadComments(this.product?.id ?? 0);
                } else {
                    alert('Silme İşlemi Başarısız');
                }
            });
        }
    }

    isAdmin(): boolean {
        return this.authService.isAdmin();
    }

     formatDate(date: string | Date | undefined): string {
    if (!date) {
      return ''; // Return an empty string if date is undefined
    }
      return new Date(date).toLocaleString(); // Format the date
  }
}