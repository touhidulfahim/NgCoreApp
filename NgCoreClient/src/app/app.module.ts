import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DepartmentComponent } from './department/department.component';
import { StudentComponent } from './student/student.component';
import { AddEditStudentComponent } from './student/add-edit-student/add-edit-student.component';
import { ShowStudentComponent } from './student/show-student/show-student.component';
import { ApiserviceService } from './apiservice.service';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MyModalComponent } from './my-modal/my-modal.component';
//import { DataTablesModule } from "angular-datatables";

@NgModule({
  declarations: [
    DepartmentComponent,
    StudentComponent,
    ShowStudentComponent,
    AddEditStudentComponent,    
    AppComponent, MyModalComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    //NgbModule
    //DataTablesModule
  ],
  providers: [ApiserviceService],
  bootstrap: [AppComponent]
})
export class AppModule { }