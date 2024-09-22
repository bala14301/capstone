import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { DashboardComponent } from './dashboard/dashboard.component'; // Add this import
import { RouterService } from './RouterService';
import { MembersComponent } from './members/members.component';
import { PagenotfountComponent } from './pagenotfount/pagenotfount.component';

const routes: Routes = [
  { 
    path: 'login', 
    component: LoginComponent,
    canActivate: [RouterService],
    data: { checkIfLoggedIn: true }
  },
  { 
    path: 'dashboard', 
    component: DashboardComponent, 
    canActivate: [RouterService]
  },
  { 
    path: 'members', 
    component: MembersComponent, 
    canActivate: [RouterService]
  },

  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: '**', component: PagenotfountComponent }
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}



