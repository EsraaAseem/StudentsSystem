import { Gender } from "./gender.model";

export class StudentRequest{
    address:string;
    birthOfData:string;
    email:string;
    firstName:string;
    genderId:number;
    lastName:string;
    phone:number;
  imgUrl:string
    constructor( fn:string,ln:string,e:string,p:number,b:string,gi:number,imgurl:string){//img:string,
             this.firstName=fn;
             this.lastName=ln;
             this.email=e;
             this.phone=p;
             this.birthOfData=b;
             this.genderId=gi;
             this.imgUrl=imgurl
    }
}