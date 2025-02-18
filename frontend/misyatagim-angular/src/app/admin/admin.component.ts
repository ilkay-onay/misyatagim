import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';

@Component({
 selector: 'app-admin',
 standalone: true,
 imports: [CommonModule, RouterModule, MatToolbarModule],
 templateUrl: './admin.component.html',
 styleUrls: ['./admin.component.css']
})
export class AdminComponent { }