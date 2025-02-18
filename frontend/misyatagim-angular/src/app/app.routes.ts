import { Routes } from '@angular/router';
import { ProductListComponent } from './pages/product-list/product-list.component';
 import { ProductDetailComponent } from './pages/product-detail/product-detail.component';
import { AdminComponent } from './admin/admin.component';
import { AuthGuard } from './guards/auth.guard';
  import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
 import { TestComponent } from './pages/test/test.component';
  import { HomeComponent } from './pages/home/home.component';

export const routes: Routes = [
  { path: '', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'product/:slug', component: ProductDetailComponent },
  { path: 'admin', loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule), canActivate: [AuthGuard] },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
     { path: 'test', component: TestComponent, canActivate: [AuthGuard] },
  { path: '**', redirectTo: '' }
];