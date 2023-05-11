export class Register{
  email: string;
  password: string;
  name:string;
  city:string;
  streetAdress:string;
  postalCode:string;
  phoneNumber:string;
  state:string;
  role:string;
  userName:string;
  constructor(e:string,pass:string,na:string,city:string,street:string,postal:string,phone:string,state:string,role:string,user:string)
  {
    this.email=e;
    this.password=pass;
    this.name=na;
    this.city=city;
    this.streetAdress=street;
    this.postalCode=postal;
    this.phoneNumber=phone;
    this.state=state;
    this.role=role;
    this.userName=user;

  }
}