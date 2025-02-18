import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Comment } from '../models/comment.model';
import { AuthService } from './auth.service';
import { tap } from 'rxjs/operators'; // tap operatörünü import ediyoruz.

@Injectable({
    providedIn: 'root'
})
export class CommentService {
    private apiUrl = 'http://localhost:5000/api/comment';
    private identityUrl = 'http://localhost:5005/api/auth'; // IdentityService URL

    constructor(private http: HttpClient, private authService: AuthService) { }

    getComments(productId: number): Observable<Comment[]> {
        return this.http.get<Comment[]>(`${this.apiUrl}/${productId}`).pipe(
            tap((comments: Comment[]) => console.log('Gelen yorumlar:', comments)) // tap operatörünü import ediyoruz, comments'e tip ekliyoruz
        );
    }

    addComment(comment: Comment): Observable<Comment> {
        const token = this.authService.getToken();
        const headers = new HttpHeaders({
            'Authorization': `Bearer ${token}`
        });
        return this.http.post<Comment>(this.apiUrl, comment, { headers });
    }

    deleteComment(id: string): Observable<boolean> {
        const token = this.authService.getToken();
        const headers = new HttpHeaders({
            'Authorization': `Bearer ${token}`
        });
        return this.http.delete<boolean>(`${this.apiUrl}/${id}`, { headers });
    }

    getUsername(userId: string): Observable<{ Username: string }> {
        return this.http.get<{ Username: string }>(`${this.identityUrl}/username/${userId}`);
    }
}