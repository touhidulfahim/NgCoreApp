import { Component, OnInit } from '@angular/core';
import { ApiserviceService } from 'src/app/apiservice.service';

@Component({
  selector: 'app-show-department',
  templateUrl: './show-department.component.html',
  styleUrls: ['./show-department.component.css']
})
export class ShowDepartmentComponent implements OnInit {

  constructor(private service: ApiserviceService) { }

  DepartmentList: any = [];
  ModalTitle = "";
  ActivateAddEditDepartComp: boolean = false;
  depart: any;
  Departments:any;

  DepartmentIdFilter = "";
  DepartmentNameFilter = "";
  DepartmentListWithoutFilter: any = [];

  ngOnInit(): void {
    this.refreshDepList();
  }

  addClick() {
    this.depart = {
      id: "",
      departmentName: "",
      departmentCode: ""
    }
    this.ModalTitle = "Add Department";
    this.ActivateAddEditDepartComp = true;
  }

  editClick(item: any) {
    this.depart = item;
    this.ModalTitle = "Edit Department";
    this.ActivateAddEditDepartComp = true;
  }

  deleteClick(item: any) {
    if (confirm('Are you sure??')) {
      this.service.deleteDepartment(item.id).subscribe(data => {
        var departments = Object.values(data);
        alert(departments[2]);
        this.refreshDepList();
      })
    }
  }

  closeClick() {
    this.ActivateAddEditDepartComp = false;
    this.refreshDepList();
  }

  refreshDepList() {
    this.service.getDepartmentList().subscribe(data => {
      var departments = Object.values(data);
      this.DepartmentList=departments[2];
      this.DepartmentListWithoutFilter = departments[2];
    });
  }

  sortResult(prop: any, asc: any) {
    this.DepartmentList = this.DepartmentListWithoutFilter.sort(function (a: any, b: any) {
      if (asc) {
        return (a[prop] > b[prop]) ? 1 : ((a[prop] < b[prop]) ? -1 : 0);
      }
      else {
        return (b[prop] > a[prop]) ? 1 : ((b[prop] < a[prop]) ? -1 : 0);
      }
    });
  }

  FilterFn() {
    debugger;
    var DepartmentIdFilter = this.DepartmentIdFilter;
    var DepartmentNameFilter = this.DepartmentNameFilter;

    this.DepartmentList = this.DepartmentListWithoutFilter.filter(
      function (el: any) {
        return el.DepartmentId.toString().toLowerCase().includes(
          DepartmentIdFilter.toString().trim().toLowerCase()
        ) &&
          el.DepartmentName.toString().toLowerCase().includes(
            DepartmentNameFilter.toString().trim().toLowerCase())
      }
    );
  }
}
