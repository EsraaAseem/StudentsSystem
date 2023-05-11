import { Gender } from "./gender.model";

export class StudentUpdateRequest{
  id:string;
    address:string;
    birthOfData:string;
    email:string;
    firstName:string;
    genderId:number;
    lastName:string;
    phone:number;
    imgFileUrl:File;
  imgUrl:string
    constructor( id:string,fn:string,ln:string,e:string,p:number,b:string,gi:number,img:File,imgurl:string){//img:string,
             this.id=id;
            this.firstName=fn;
             this.lastName=ln;
             this.email=e;
             this.phone=p;
             this.birthOfData=b;
            this.imgFileUrl=img;
             this.genderId=gi;
             this.imgUrl=imgurl
    }
}