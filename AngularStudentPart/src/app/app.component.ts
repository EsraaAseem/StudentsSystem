import { Component, OnInit } from '@angular/core';
import { User } from './service/user.model';
import { StdServiceService } from './service/std-service.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'AngularStudentPart';
  currentUser: User;
  constructor(private stdservice:StdServiceService,private router:Router){}
  ngOnInit(): void {
    this.stdservice.user.subscribe(user => this.currentUser = user);
    if(!this.currentUser)
    {
      this.router.navigate(['/Student-logo']);
    }
    }
}
