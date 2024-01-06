import { Component, OnInit, Input } from '@angular/core';
import { ApiserviceService } from 'src/app/apiservice.service';

@Component({
  selector: 'app-add-edit-department',
  templateUrl: './add-edit-department.component.html',
  styleUrls: ['./add-edit-department.component.css']
})
export class AddEditDepartmentComponent implements OnInit {

  constructor(private service: ApiserviceService) { }

  @Input() depart: any;
  id = "";
  departmentCode = "";
  departmentName = "";


  ngOnInit(): void {
    this.id = this.depart.id;
    this.departmentName = this.depart.departmentName;
    this.departmentCode= this.depart.departmentCode;
    //this.refreshDepList();
  }

  // refreshDepList() {
  //   this.service.getDepartmentList().subscribe(data => {
  //     var departments = Object.values(data);
  //     this.DepartmentList=departments[2];
  //     this.DepartmentListWithoutFilter = departments[2];
  //   });
  // }

  addDepartment() {
    var dept = {
      departmentName: this.departmentName,
      departmentCode:this.departmentCode
    };
    this.service.addDepartment(dept).subscribe(res => {
      alert(res.notificationMessage);
    });

  }

  updateDepartment() {
    var dept = {
      id: this.id,
      departmentName: this.departmentName,
      departmentCode: this.departmentCode
    };
    this.service.updateDepartment(dept).subscribe(res => {
      alert(res.notificationMessage);
    });
  }
}
