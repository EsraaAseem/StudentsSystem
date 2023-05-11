import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { first, Observable, Subscription } from 'rxjs';
import { Login } from 'src/app/service/login.model';
import { StdServiceService } from 'src/app/service/std-service.service';
import { User } from 'src/app/service/user.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit{
  error:string=null;
  currentuser:Subscription;
   authobsuser:Observable<User>;
    login:Login={
      email:'',
      password:''
    }
 // loginform:FormGroup
  constructor(private stdservice:StdServiceService,private router:Router){}

  ngOnInit(): void {
    //this.privateform();
   // console.log(localStorage.getItem("token"));
  // this.currentuser=this.stdservice.user.subscribe();
   
  }
  onSubmitForm(sub:NgForm){
    if(!sub.valid)
    {
      return;
    }
    let authobs:Observable<User>;
    
    const email=sub.value.email;
    const password=sub.value.password;
      authobs= this.stdservice.Login(email,password);
    
    authobs.pipe(first())
    .subscribe(
       res => { 
        console.log(res.token);
        localStorage.setItem('authToken', res.token);
        this.router.navigate(['/Student']);},error=>{
          this.error=error
        });
  }
  onclose()
  {
    this.error=null;
  }
 /* private privateform(){
    let email=' ';
    let password=' ';
 
   /* if(this.EditMode)
    {
         const recipe=this.recipeService.getRecipe(this.index)
         recipeName=recipe.name;
         recipeDescrption=recipe.description;
         recipeImg=recipe.imgurl;
         
    };
      this.loginform=new FormGroup({
       'email':new FormControl(email,Validators.required),
       'password':new FormControl(password,Validators.required),
       
      })


   }*/
  /* onSubmit(){
    console.log(this.loginform.value);

    this.stdservice.Login(this.loginform.value).subscribe(()=>{
      console.log("user has been login")
    } ,error=>{console.log(error)}
    )
    
    
   }*/
}
