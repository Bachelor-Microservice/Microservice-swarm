import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthService } from './auth.service';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UsermanagerService {

  IdentityURL = environment.identity + '/usermanager' ;

  

  constructor(private http: HttpClient, private authServive: AuthService) { }


  public createUser(model) {
    return this.http.post(this.IdentityURL , model);
  }

  public UpdatePassword(model) {
    return this.http.put(this.IdentityURL , model);
  }

  public UpdateEmail(model) {
    return this.http.put(this.IdentityURL + '/email' , model);
  }


  public GetAllUsers() {
    return this.http.get(this.IdentityURL);
  }

  public DeleteUser(id) {
    return this.http.delete(this.IdentityURL + '?id=' + id);
  }



}
