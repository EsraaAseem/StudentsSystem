import { Component, OnInit } from '@angular/core';
//import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { first, Observable } from 'rxjs';
import { Gender } from 'src/app/service/gender.model';
import { StdDataServiceService } from 'src/app/service/std-dataservice.service';
import { User } from 'src/app/service/user.model';
import { Student } from '../Student.model';
import { NotificationService } from 'src/app/service/notification.service';

@Component({
  selector: 'app-add-student',
  templateUrl: './add-student.component.html',
  styleUrls: ['./add-student.component.css']
})
export class AddStudentComponent implements OnInit{
  //StudentForm:FormGroup;
  EditMode=false;
  index:string;
  genders:Gender[]=[];
  currentUser: User;
  loginerror:string=null;
   show:boolean=true;
   errorsub:string=null;
   profileimg='/assets/student-logo.jpg';
  //student:Student;
  student:Student={
      id:'',
     firstName:'',
     lastName:' ',
     email:' ',
     phone:0,
     address:'',
     birthOfData:'',
     genderId:2,
     imgUrl:'',
     gender:{
      genderId:0,
      description:''
     }
   };

  error: any;
   imagephoto:any;
  constructor(private stdservice:StdDataServiceService,private router:Router,private route:ActivatedRoute,private toastr:NotificationService){}
  ngOnInit(): void {
    console.log(this.imagephoto);
    //this.stdservice.editStd.subscribe((st:Student)=>{this.student=st})
    this.route.params.subscribe((params:Params)=>{
      this.index=params['id'];
      this.EditMode=params['id']!=null;
      if(this.index)
      {
         this.stdservice.getStudent(this.index).subscribe((success)=>{
          this.student=success;
          if(success.imgUrl!=null)
          {
            console.log(success.imgUrl)
         // this.setimg();
         this.profileimg=this.stdservice.getimgpath(success.imgUrl);
         console.log(this.profileimg)

          }
          else{
            this.profileimg='/assets/student-logo.jpg';
          }
          console.log(success);
         })
            //   this.student=this.stdservice.editStd
      }
       }
    )
    this.stdservice.getGender().subscribe((gender)=>{
      this.genders=gender;
     // console.log(this.genders);
     })
    //this.privateform();
    this.stdservice.user.subscribe(user => this.currentUser = user);
   
  }
 

  onSubmit(){
    this.show=true;
    if(this.currentUser)
    {
      if(this.EditMode==true)
      {

        this.stdservice.updateStudent(this.index,this.student)
        .subscribe(() => {
         console.log('Student updated successfully');
         this.toastr.showSuccess("Student updated successfully","Success!")
        this.router.navigate(['/Student']);},error=>{
          this.errorsub=error
        });
        
        this.EditMode=true;
        this.setimg();
      }
      else{
      this.stdservice.addStudent(this.student)
      .subscribe(() => {
       console.log('Student added successfully');
       this.toastr.showSuccess("data added successfully","Success!");
      this.router.navigate(['/Student']);},error=>{
        this.errorsub=error
        this.toastr.showError("data not added successfully","Error!")
      })
      this.EditMode=false;
      }
    }
  else{
    this.EditMode=false;
    this.loginerror="You must Login"
  }
  this.EditMode=false;
  }
  onclose()
  {
    this.errorsub=null;
  }
  uploadImage(itme:any){
    if(this.EditMode)
    {
      this.stdservice.deleteimg(this.index).subscribe();
    }
const file:File=itme.target.files[0];
console.log(file);
this.stdservice.uploadImg(file).subscribe((success)=>{

  this.student.imgUrl=success,
  console.log(success)
  this.setimg()
  //this.student.imgUrl=success
})
  }

  setimg(){
   if(this.student.imgUrl !=null||this.student.imgUrl !='')
    {
      this.profileimg=this.stdservice.getimgpath(this.student.imgUrl);
    }
    else{
      console.log(this.student.imgUrl);
      this.profileimg='/assets/category.jpg';

    }

  }
  onDelete(){
    this.stdservice.deleteimg(this.index).subscribe();
    this.stdservice.deleteStudent(this.index)
    .subscribe(() => {
     console.log('Student deleted successfully');
     this.toastr.showSuccess("deleted successfully","Success!");
    this.router.navigate(['/Student']);},error=>{
      this.toastr.showError("data not deleted","Delete!");
    })
    this.EditMode=false;
  }
  onCancel(){
    this.router.navigate(['/Student']);
  }
  onClose(){
    this.loginerror='';
    return this.show=!this.show;
  }
}
