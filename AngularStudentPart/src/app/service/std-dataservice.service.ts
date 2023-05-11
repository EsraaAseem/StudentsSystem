import { formatDate } from '@angular/common';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, map, Observable, Subject, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Student } from '../students/Student.model';
import { Gender } from './gender.model';
import { User } from './user.model';
import { StudentRequest } from './StudentRequest.model';
import { StudentUpdateRequest } from './StudentUpdateRequest.model';

@Injectable({
  providedIn: 'root'
})
export class StdDataServiceService {
      student:Student[]=[];
      editStd:Student;
    private userSubject: BehaviorSubject<User>;
    public user: Observable<User>;
  baseapiurl:string=environment.baseurl;
    constructor(private http:HttpClient,private router:Router) {
      this.userSubject = new BehaviorSubject<User>(
        JSON.parse(localStorage.getItem('currentUser')));
        this.user = this.userSubject.asObservable();
     }
     public get userValue(): User {
      return this.userSubject.value;
    }
  getStudents():Observable<Student[]>{
     return this.http.get<Student[]>(this.baseapiurl+'/Students');  
  }
  getStudent(stdId:string):Observable<Student>{
    return this.http.get<Student>(this.baseapiurl+'/Students/'+stdId)
 }

 deleteStudent(stdId:string):Observable<Student>{
  return this.http.delete<Student>(this.baseapiurl+'/Students/'+stdId)
}
 addStudent(student:StudentRequest):Observable<StudentRequest>{
    return this.http.post<StudentRequest>(this.baseapiurl+'/Students/Add',student)
 .pipe(
    map(({firstName,lastName,phone,email,genderId,birthOfData,address,imgUrl}) => {
      let std: StudentRequest = {
        firstName:firstName,
        lastName: lastName,
        email:email,
        phone:phone,
        birthOfData:birthOfData,
        genderId: genderId,
        address:address ,
       imgUrl:imgUrl
      }
      return std;
    })
  );  
}


updateStudent(id:string,student:StudentRequest):Observable<StudentRequest>{
  
  return this.http.post<StudentRequest>(this.baseapiurl+'/Students/'+id,student)
 .pipe(
    map(({firstName,lastName,phone,email,genderId,birthOfData,address,imgUrl}) => {
      let std: StudentRequest = {
        firstName:firstName,
        lastName: lastName,
        email:email,
        phone:phone,
        birthOfData:birthOfData,
        genderId: genderId,
        address:address ,
       imgUrl:imgUrl,
      }
      return std;
    })
  );  
}

uploadImg(file:File):Observable<any>{
const formDate=new FormData();
formDate.append("profimgfile",file);
return this.http.post(this.baseapiurl+'/Students/upload-img',formDate,{responseType:'text'})
}
deleteimg(idimg:string):Observable<Student>{
  return this.http.delete<Student>(this.baseapiurl+'/Students/'+idimg+'/upload-img')
}
getimgpath(relativepath:string){
 return `${this.baseapiurl}${relativepath}`;
}

  getGender():Observable<Gender[]>{
    return this.http.get<Gender[]>(this.baseapiurl+'/Gender');  
 }
 private errorHandling(errorRes:HttpErrorResponse){
  let errormessage="there is error is occured"
  if(!errorRes.error||!errorRes.error.error){
    return throwError(errormessage);
  }
  switch(errorRes.error.error.message){
      case 'EMAIL_EXISTS':
        errormessage="The email address is already exist"
        break;
        case 'EMAIL_NOT_FOUND':
          errormessage="There is no user record corresponding to this identifier";
          break;
          case 'INVALID_PASSWORD':
              errormessage=" The password is invalid ";
              break;
              case 'USER_DISABLED':
              errormessage=" The user account has been disabled by an administrator.";
                  break;
  }
  return throwError(errormessage);
 }
}
