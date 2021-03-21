import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SharedService {
  readonly APIUrl = 'http://localhost:5000/api';

  constructor(private http: HttpClient) {}

  getDepartmentList(): Observable<any[]> {
    return this.http.get<any>(this.APIUrl + '/department');
  }

  addDepartment(val: any): Observable<any> {
    return this.http.post(this.APIUrl + '/department', val);
  }

  updateDepartment(val: any): Observable<any> {
    return this.http.put(this.APIUrl + '/department', val);
  }

  deleteDepartment(val: any): Observable<any> {
    return this.http.delete(this.APIUrl + '/department/' + val);
  }
}
