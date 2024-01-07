import { Component, OnInit,ElementRef,ViewChild } from '@angular/core';
import { ApiserviceService } from 'src/app/apiservice.service';

@Component({
  selector: 'app-department',
  templateUrl: './department.component.html',
  styleUrls: ['./department.component.css']
})
export class DepartmentComponent implements OnInit {

  constructor(private service: ApiserviceService) { }
  DepartmentList: any = [];
  ModalTitle = "";
  ActivateAddEditDepartComp: boolean = false;
  IsmodelShow: boolean=false;
  DepartmentIdFilter = "";
  DepartmentNameFilter = "";
  DepartmentListWithoutFilter: any = [];
  closeResult = '';
  @ViewChild('btnClose', { static: true })
  btnClose!: ElementRef;
  @ViewChild('btnModalOpen', { static: true })
  btnModalOpen!: ElementRef;

  departmentModel: any = {
    id: "",
    departmentCode: '',
    departmentName:'',
    createdById: '',
    createdDate: '',
    isDeleted: false
  }

  ngOnInit(): void {
    this.refreshDepList();
  }

  addClick() {
    this.departmentModel = {
      id: "",
      departmentName: "",
      departmentCode: ""
    }
    this.ModalTitle = "Add Department";
  }

  addDepartment() {
    var dept = {
      departmentName: this.departmentModel.departmentName,
      departmentCode:this.departmentModel.departmentCode
    };
    this.service.addDepartment(dept).subscribe(res => {
      alert(res.notificationMessage);
      this.btnClose.nativeElement.click();
    });
  }

  editClick(item: any) {
    this.departmentModel = item;
    this.ModalTitle = "Edit Department";
    this.ActivateAddEditDepartComp = true;
  }

  updateDepartment() {
    var dept = {
      id: this.departmentModel.id,
      departmentName: this.departmentModel.departmentName,
      departmentCode: this.departmentModel.departmentCode
    };
    this.service.updateDepartment(dept).subscribe(res => {
      alert(res.notificationMessage);
      this.btnClose.nativeElement.click();
    });
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

