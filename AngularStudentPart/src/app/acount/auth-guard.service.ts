import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from "@angular/router";
import { map, Observable, take } from "rxjs";
import { StdServiceService } from "../service/std-service.service";
@Injectable({providedIn:'root'})
export class AuthGuardService implements CanActivate{
    constructor(private authService:StdServiceService,private router:Router){}
    canActivate(route: ActivatedRouteSnapshot, router: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
        return this.authService.user.pipe(take(1),map(user=>{
            const authUser=!!user;
            if(authUser){
                return true;
            }
            return this.router.createUrlTree(['/auth']);
        }))
    }
}