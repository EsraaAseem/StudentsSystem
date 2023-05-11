import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { StudentsComponent } from './students/students.component';
import { AddStudentComponent } from './students/add-student/add-student.component';
import { LoginComponent } from './acount/login/login.component';
import { RegisterComponent } from './acount/register/register.component';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from './service/auth.interceptor';
import { StudentLogoComponent } from './student-logo/student-logo.component';
const approute:Routes=[
 // {path:'',redirectTo:'/employess',pathMatch:'full'},

 {path:'',redirectTo:'/Student',pathMatch:'full'}, 
 {path:'Student-logo',component:StudentLogoComponent},
 {path:'Student',component:StudentsComponent},
 {path:'Student/login',component:LoginComponent},
 {path:'register',component:RegisterComponent},
 {path:'add-student',component:AddStudentComponent},
 {path:'Student/:id',component:AddStudentComponent},

]
@NgModule({
  imports:[RouterModule.forRoot(approute)],
  
  exports:[RouterModule]
})
export class AppRoutingModule { }
