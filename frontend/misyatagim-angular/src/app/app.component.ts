import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { CustomRouterService } from './services/custom-router.service';
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'misyatagim-angular';
 constructor(private customRouter: CustomRouterService){}
 ngOnInit(): void {
   this.customRouter.loadProductRoutes();
 }
}