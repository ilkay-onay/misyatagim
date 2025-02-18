import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
 import { HttpClient } from '@angular/common/http';

 @Component({
     selector: 'app-test',
     standalone: true,
     imports: [CommonModule],
     templateUrl: './test.component.html',
     styleUrls: ['./test.component.css']
 })
 export class TestComponent {
     testMessage:string | null = null;
    constructor(private http: HttpClient){
      }
     test(){
        this.http.get("http://localhost:5000/api/auth/test",{ withCredentials: true }).subscribe({
           next:(result)=>
            {
               this.testMessage= result as string;
            },
             error: (error) => {
               this.testMessage="Yetkisiz Erişim";
            }
        })
     }

 }