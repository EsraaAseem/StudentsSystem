import { formatDate } from '@angular/common';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, catchError, map, Observable, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Student } from '../students/Student.model';
import { Gender } from './gender.model';
import { Login } from './login.model';
import { User } from './user.model';
import { Register } from './register.model';
import { StudentRequest } from './StudentRequest.model';

@Injectable({
  providedIn: 'root'
})
export class StdServiceService {
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
 Login(email:string,password:string){
  return this.http.post<User>(this.baseapiurl+'/Login',{
    email:email,
    password:password
  }).pipe(
    map(({isAuthenticated,userName,role,expiresOn,token}) => {
      let user: User = {
        isAuthenticated:isAuthenticated,
        email: email,
        userName:userName,
        expiresOn:expiresOn,
        role:role,
        token: token,
      };
      localStorage.setItem('currentUser', JSON.stringify(user));
      this.userSubject.next(user);
      return user;
    })
  );
  //.pipe(catchError(this.errorHandling))
 }
 LogOut(){
  localStorage.removeItem('currentUser');
  localStorage.removeItem('authToken');
  this.userSubject.next(null);
  //this.router.navigate(['/employess'])
 }
  RegisterUser(valu:Register){
    return this.http.post<User>(this.baseapiurl+'/Register',valu)}

  Register(valu:Register){
    return this.http.post<User>(this.baseapiurl+'/Register',valu)
    .pipe(
      map(({email,isAuthenticated,userName,role,expiresOn,token}) => {
        let user: User = {
          isAuthenticated:isAuthenticated,
          email:email,
          userName:userName,
          expiresOn:expiresOn,
          role:role,
          token: token,
        };
        localStorage.setItem('currentUser', JSON.stringify(user));
        this.userSubject.next(user);
        return user;
      })
    );
    /*.pipe(
      catchError(this.errorHandling));*/
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
