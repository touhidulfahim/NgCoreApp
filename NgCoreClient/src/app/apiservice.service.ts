import { Injectable } from '@angular/core';
import { HttpClient, HttpEvent, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiserviceService {
  readonly apiUrl = 'https://localhost:7186/api/';

  constructor(private http: HttpClient) { }
  // Department
  getDepartmentList(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl + 'department');
  }
  addDepartment(dept: any): Observable<any> {
    return this.http.post<any>(this.apiUrl + 'department',dept);
  }
  updateDepartment(dept: any): Observable<any> {
    return this.http.put<any>(this.apiUrl + 'department',dept);
  }
  deleteDepartment(deptId: number): Observable<number> {
    return this.http.delete<number>(this.apiUrl + 'department/' + deptId);
  }
  // Employee
  getEmployeeList(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl + 'employee/GetEmployee');
  }

  addEmployee(emp: any): Observable<any> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.post<any>(this.apiUrl + 'employee/AddEmployee', emp, httpOptions);
  }

  updateEmployee(emp: any): Observable<any> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.put<any>(this.apiUrl + 'employee/UpdateEmployee/', emp, httpOptions);
  }

  deleteEmployee(empId: number): Observable<number> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.delete<number>(this.apiUrl + 'employee/DeleteEmployee/' + empId, httpOptions);
  }


  getAllDepartmentNames(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl + 'employee/GetAllDepartmentNames');
  }

}
