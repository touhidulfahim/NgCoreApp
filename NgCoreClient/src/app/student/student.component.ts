import { Component, OnInit,ElementRef,ViewChild } from '@angular/core';
import { ApiserviceService } from 'src/app/apiservice.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-student',
  templateUrl: './student.component.html',
  styleUrls: ['./student.component.css']
})
export class StudentComponent implements OnInit {
  constructor(private service: ApiserviceService,private datePipe: DatePipe) { }
  DepartmentList: any = [];
  StudentList: any = [];
  ModalTitle = "";
  StudentListWithoutFilter: any = [];
  closeResult = '';
  @ViewChild('btnClose', { static: true })
  btnClose!: ElementRef;
  @ViewChild('btnModalOpen', { static: true })
  btnModalOpen!: ElementRef;

  ngOnInit(): void {
    this.refreshDepList();
  }

  studentModel: any = {
    id: "",
    departmentId: '',
    departmentName:'',
    studentID:'',
    studentName: '',
    fathersName: '',
    birthDate: "",
    dob:'',
    mobile: '',
    email:'',
    postalAddress:'',
    createdById: '',
    createdDate: '',
    isDeleted: false
  }
  addClick() {
    this.studentModel = {
      id: "",
      departmentId: "",
      departmentName: "",
      studentID: "",
      studentName: "",
      fathersName: "",
      birthDate: "",
      mobile: "",
      email: "",
      postalAddress: "",
      createdById: "",
      createdDate: "",
      isDeleted: false
    }
    this.ModalTitle = "Add Student";
  }

  refreshDepList() {
    this.service.getStudentList().subscribe(data => {
      var students = Object.values(data);
      this.StudentList=students[2];
      this.StudentListWithoutFilter = students[2];
    });
    this.service.getDepartmentList().subscribe(data => {
      var departments = Object.values(data);
      this.DepartmentList=departments[2];
    });

  }

addStudent(){
  var stu = {
    departmentId: this.studentModel.departmentId,
    studentID: this.studentModel.studentID,
    studentName:  this.studentModel.studentName,
    fathersName:  this.studentModel.fathersName,
    birthDate:  this.studentModel.birthDate,
    mobile:  this.studentModel.mobile,
    email: this.studentModel.email,
    postalAddress: this.studentModel.postalAddress,
  };
  this.service.addStudent(stu).subscribe(res => {
    alert(res.notificationMessage);
    this.btnClose.nativeElement.click();
  });
}
editClick(item: any) {
  this.studentModel = item;
  let bDate = this.datePipe.transform(this.studentModel.birthDate, 'yyyy-MM-dd');
  this.studentModel.birthDate=bDate;
  this.ModalTitle = "Edit Student";
}
updateStudent(){
  var stu = {
    id:this.studentModel.id,
    departmentId: this.studentModel.departmentId,
    studentID: this.studentModel.studentID,
    studentName:  this.studentModel.studentName,
    fathersName:  this.studentModel.fathersName,
    birthDate:  this.studentModel.birthDate,
    mobile:  this.studentModel.mobile,
    email: this.studentModel.email,
    postalAddress: this.studentModel.postalAddress,
  };
  this.service.updateStudent(stu).subscribe(res => {
    alert(res.notificationMessage);
    this.btnClose.nativeElement.click();
  });
}
deleteClick(item: any) {
  if (confirm('Are you sure??')) {
    this.service.deleteStudent(item.id).subscribe(data => {
      var departments = Object.values(data);
      alert(departments[2]);
      this.refreshDepList();
    })
  }
}
closeClick(){
  this.refreshDepList();
}


}
