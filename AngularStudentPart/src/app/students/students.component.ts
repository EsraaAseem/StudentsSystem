import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { StdDataServiceService } from '../service/std-dataservice.service';
import { StdServiceService } from '../service/std-service.service';
import { User } from '../service/user.model';
import { Student } from './Student.model';

@Component({
  selector: 'app-students',
  templateUrl: './students.component.html',
  styleUrls: ['./students.component.css']
})
export class StudentsComponent implements OnInit{
  students:Student[]=[];
  currentUser: User;
  sub:Subscription;
 @Input()student:Student;
 // isAuth:Observable<User>;
  constructor(private stdservice:StdServiceService,private stdDataServ:StdDataServiceService,private router:Router,private route:ActivatedRoute){}
  ngOnInit(): void {
   this.stdDataServ.getStudents().subscribe((success)=>{  this.students=success;});
/*  this.stdservice.user.subscribe(user => this.currentUser = user);
console.log(this.currentUser);*/
}
/*onLogout(){
  this.stdservice.LogOut();
  
}
*/
onEdit(item:any){
   this.stdDataServ.editStd=item;
   localStorage.setItem('editstd', JSON.stringify(this.stdDataServ.editStd));

}
}
