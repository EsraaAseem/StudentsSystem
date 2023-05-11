import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AcountComponent } from './acount.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { AcountRouteModule } from './acount-route.module';
import { RouterModule } from '@angular/router';
import { ServiceModule } from '../service/service.module';
@NgModule({
  declarations: [
    AcountComponent,LoginComponent,
    RegisterComponent,

  ],
  imports: [
    CommonModule,
    BrowserModule,
    RouterModule,
    HttpClientModule,
    AcountRouteModule ,
    ReactiveFormsModule,FormsModule,
    ServiceModule
  ]
})
export class AcountModule { }
