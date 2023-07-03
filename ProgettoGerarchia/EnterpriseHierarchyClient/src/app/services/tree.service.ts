import { Injectable } from '@angular/core';
import { HttpClient }  from '@angular/common/http';
import { EnterprieseTree } from '../model/EnterprieseTree';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TreeService {

  constructor(private http : HttpClient) { }

  GetTreeFromDB() : Observable<EnterprieseTree[]> {
    const url = environment.url + "get-tree";

    return this.http.get<EnterprieseTree[]>(url);
  }

  SelectWholeTree(fatherId: number): Observable<EnterprieseTree[]> {
    const url = (environment.url + "select-tree/" + fatherId);
    
    return this.http.post<EnterprieseTree[]>(url, fatherId);
  }

  SelectSingleEnterprise(enterpriseId: number): Observable<EnterprieseTree[]> {
    const url = (environment.url + "select-enterprise/" + enterpriseId);
    
    return this.http.post<EnterprieseTree[]>(url, enterpriseId);
  }

  AddChild(newChild: EnterprieseTree, fatherId: number): Observable<EnterprieseTree[]> {
    const url = (environment.url + "add-child/" + fatherId);
    
    return this.http.post<EnterprieseTree[]>(url, newChild);
  }
}
