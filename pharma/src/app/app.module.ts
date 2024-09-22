import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AgGridModule } from 'ag-grid-angular';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AppLoaderComponent } from './app-loader.component';
import { AppRoutingModule } from './app-routing.module';
import { LoginComponent } from './login/login.component';

// Import your HTTP interceptor
import { LoaderInterceptor } from './load-interceptor';

// Import all your services
import { LoaderService } from './loader-service';
import { RouterService } from './RouterService';
import { DrugsService } from './DrugsService';
import { CoreModule } from './core-module/core-module.module';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
// ... import other services as needed

import { MAT_DIALOG_DEFAULT_OPTIONS, MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { SharedModule } from './_shared-module/shared.module.module';
import { MemberService } from './member.service';
import { DoctorService, PrescriptionService, RefillService, SubscriptionService } from './app.service';
import { MembersComponent } from './members/members.component';

import { PagenotfountComponent } from './pagenotfount/pagenotfount.component';


@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    AppLoaderComponent,
    LoginComponent,
    MembersComponent,
   
    PagenotfountComponent,

  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AgGridModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    CoreModule,
    SharedModule
  ],
  exports: [
    SharedModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: LoaderInterceptor, multi: true },
   DrugsService,
    RouterService,
    MemberService,
    DoctorService,
    LoaderService,
    PrescriptionService,
    MemberService,
    SubscriptionService,
    RefillService,
    provideAnimationsAsync(),
    {provide: MAT_DIALOG_DEFAULT_OPTIONS, useValue: {hasBackdrop: false}}
    // ... add other services here
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }