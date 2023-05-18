import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../model/User';

@Injectable({
  providedIn: 'root'
})
export class RegistrationService {

  constructor(private http : HttpClient) {}

  registration(username : string, password : string) : Observable<boolean>{

    const user : User = {
      UserName : username,
      UserPsw : password
    }

    const url = "https://localhost:7189/NewUser"
    
    return this.http.post<boolean>(url, user);
  }

  
}
