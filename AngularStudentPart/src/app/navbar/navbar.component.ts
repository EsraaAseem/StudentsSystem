import { Component, OnInit } from '@angular/core';
import { User } from '../service/user.model';
import { Subscription } from 'rxjs';
import { StdDataServiceService } from '../service/std-dataservice.service';
import { StdServiceService } from '../service/std-service.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit{
  public gfg = false;
  currentUser: User;
  constructor(private stdservice:StdServiceService,private stdDataServ:StdDataServiceService,private router:Router){}
  ngOnInit(): void {
    this.stdservice.user.subscribe(user => this.currentUser = user);
    console.log(this.currentUser); 
   }
    onLogout(){
      this.stdservice.LogOut();
      this.router.navigate(['/Student-logo']);
    }
}
