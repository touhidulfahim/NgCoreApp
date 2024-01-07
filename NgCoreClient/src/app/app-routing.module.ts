import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { StudentComponent } from './student/student.component';
import { DepartmentComponent } from './department/department.component';
import { HomeComponent } from './home/home.component';



const routes: Routes = [
  { path: 'student', component: StudentComponent },
  { path: 'department', component: DepartmentComponent },
  { path: 'home', component: HomeComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
