import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { StudentsComponent } from './students/students.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AddStudentComponent } from './students/add-student/add-student.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AcountModule } from './acount/acount.module';
import { RouterModule } from '@angular/router';
import { AuthInterceptor } from './service/auth.interceptor';
import { StudentLogoComponent } from './student-logo/student-logo.component';
import { NavbarComponent } from './navbar/navbar.component';
import { TextInputComponent } from './service/text-input/text-input.component';
import { ServiceModule } from './service/service.module';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
@NgModule({
  declarations: [
    AppComponent,
    StudentsComponent,
    AddStudentComponent,
    StudentLogoComponent,
    NavbarComponent,
  ],
 providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    },
    TextInputComponent
  ],
  imports: [
    BrowserModule,
    AcountModule,
    AppRoutingModule,
    HttpClientModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    ServiceModule,
    BrowserAnimationsModule,
      ToastrModule.forRoot({
        timeOut: 10000,
        positionClass: 'toast-bottom-right',
        preventDuplicates: true,     
        enableHtml:true 
     })
  ],
 
  bootstrap: [AppComponent]
})
export class AppModule { }
