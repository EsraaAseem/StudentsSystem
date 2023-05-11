import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { RouterModule, Routes } from '@angular/router';
import { StudentsComponent } from '../students/students.component';
import { AddStudentComponent } from '../students/add-student/add-student.component';
const accountroute:Routes=[
 
 /* {path:'employess',component:StudentsComponent,children:[
  {path:'',component:StudentsComponent},

  ]},

  {path:'add-student', component: AddStudentComponent},
  {path:'register', component: RegisterComponent},
  {path:'login', component: LoginComponent}*/
 
]


@NgModule({
  imports:[RouterModule.forChild(accountroute)],
  exports:[RouterModule]
})
export class AcountRouteModule { }
