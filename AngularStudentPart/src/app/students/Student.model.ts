import { Gender } from "../service/gender.model";

export class Student{
    address:string;
    birthOfData:string;
    email:string;
    firstName:string;
    genderId:number;
    id:string;
    lastName:string;
    phone:number;
   gender:Gender
    imgUrl:string;

    constructor( id:string,fn:string,ln:string,e:string,p:number,b:string,gi:number,gen:Gender,img:string){//img:string,
             this.id=id;
             this.firstName=fn;
             this.lastName=ln;
             this.email=e;
             this.phone=p;
             this.birthOfData=b;
           this.imgUrl=img;
             this.genderId=gi;
             this.gender=gen;
    }
}