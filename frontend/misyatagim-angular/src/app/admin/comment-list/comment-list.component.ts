import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { CommentService } from '../../services/comment.service';
import { Comment } from '../../models/comment.model';
import { MatDialog } from '@angular/material/dialog';
import { DeleteConfirmationDialogComponent } from '../shared/delete-confirmation-dialog/delete-confirmation-dialog.component';
import { RouterModule } from '@angular/router';
@Component({
  selector: 'app-comment-list',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, RouterModule],
  templateUrl: './comment-list.component.html',
  styleUrls: ['./comment-list.component.css']
})
export class CommentListComponent implements OnInit {
    comments: Comment[] = [];
    displayedColumns: string[] = ['id', 'productId', 'text', 'actions'];
  constructor(private commentService: CommentService, private dialog: MatDialog) { }

  ngOnInit(): void {
      this.loadComments();
  }
   loadComments() {
         //TODO: ürünleri buraya listeleyelim
          this.comments = []; //TODO: test amaçlı.
      //    this.commentService.getComments().subscribe(data => {
      //          this.comments = data;
       //   });
    }
    openDeleteDialog(commentId: string) {
      const dialogRef = this.dialog.open(DeleteConfirmationDialogComponent, {
          width: '400px',
          data: { message: 'Bu yorumu silmek istediğinize emin misiniz?' }
      });

      dialogRef.afterClosed().subscribe(result => {
          if (result) {
              this.deleteComment(commentId);
          }
      });
  }
  deleteComment(commentId: string) {
    this.commentService.deleteComment(commentId).subscribe(success => {
        if(success)
        {
           this.loadComments();
        } else {
            alert("Silme İşlemi Başarısız");
        }
    });
  }
}