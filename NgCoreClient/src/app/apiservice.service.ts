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
  // Student
  getStudentList(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl + 'student');
  }

  addStudent(stud: any): Observable<any> {
    return this.http.post<any>(this.apiUrl + 'student', stud);
  }

  updateStudent(stud: any): Observable<any> {
    return this.http.put<any>(this.apiUrl + 'student', stud);
  }

  deleteStudent(studId: number): Observable<number> {
    return this.http.delete<number>(this.apiUrl + 'student/' + studId);
  }


  getAllDepartmentNames(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl + 'employee/GetAllDepartmentNames');
  }

}
