import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from "@angular/forms";
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  form: FormGroup;


  public userArray: [{}] = [{}];
 
  constructor(
    public fb: FormBuilder,
    private http: HttpClient
  ) {
    this.form = this.fb.group({
      name: [''],
      age: [null]
    })
    this.userArray.pop();
  }


  ngOnInit(): void {
  }

  submitForm() {
    var formData: any = new FormData();
    formData.append("name", this.form.get('name').value);
    formData.append("age", this.form.get('age').value);


    this.userArray.push(this.form.value);
    this.form.reset();
  }


  remove(user) {
    const index: number = this.userArray.indexOf(user);
    if (index !== -1) {
        this.userArray.splice(index, 1);
    }      
      
  }

}


