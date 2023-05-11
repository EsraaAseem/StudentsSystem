import { Component, ElementRef, Injectable, Input, OnInit, Self, ViewChild } from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';

@Component({
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.css']
})
@Injectable({
  providedIn: 'root',
})
export class TextInputComponent implements OnInit,ControlValueAccessor{
  @ViewChild("input",{static:true}) input:ElementRef;
  @Input() type='text';
  @Input() label:string;

  constructor(@Self() public ControlDir:NgControl){
    this.ControlDir.valueAccessor=this;
  }
  ngOnInit(): void {
    const control=this.ControlDir.control;
    const validators=control.validator?[control.validator]:[];
    const asyncvalidators=control.asyncValidator?[control.asyncValidator]:[];
    control.setValidators(validators);
    control.setAsyncValidators(asyncvalidators);
    control.updateValueAndValidity();
  }
  onChange(event){
    return (event.target as HTMLInputElement).value;
  };
  onTouched(){};
  writeValue(obj: any): void {
    this.input.nativeElement.value=obj||'';
  }
  registerOnChange(fn: any): void {
    this.onChange=fn;
  }
  registerOnTouched(fn: any): void {
    this.onTouched=fn;
  }
 
}
