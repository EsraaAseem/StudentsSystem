export interface User{
    isAuthenticated: boolean;
    email:string;
    userName:string;
    role:string;
    expiresOn:string;
    token: string;
  }

/*export class UserDto{
    isAuthenticated: boolean;
    email:string;
    userName:string;
    role:string;
    expiresOn:string;
    token: string;
    constructor(isauth:boolean,em:string,user:string,role:string,expir:string,tok:string)
    {
        this.isAuthenticated=isauth;
        this.email=em;
        this.userName=user;
        this.role=role;
        this.expiresOn=expir;
        this.token=tok;
    }
}*/