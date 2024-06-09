import { HttpErrorResponse } from '@angular/common/http';
import { ChangeDetectorRef, Component } from '@angular/core';
import { Router } from '@angular/router';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Paging } from 'src/app/model';
import { CreateExportbillDto, GetDetail_exportbillDto } from 'src/app/model/exportBill';
import { exportBillService } from 'src/app/service/Exportbill.service';

@Component({
  selector: 'app-export-bill',
  templateUrl: './export-bill.component.html',
  styleUrls: ['./export-bill.component.css']
})
export class ExportBillComponent {
  loading: boolean = false;
  paging: Paging = { keyword: "", pageIndex: 1, pageSize: 10 };
  keyword: string = ""
  listExportBill: CreateExportbillDto[] = [];
  Totalcount!: number;
  visible: boolean = false;
  listDetail: GetDetail_exportbillDto[] = [];
  constructor(private exportBillService: exportBillService, private confirmationService: ConfirmationService, private MessageSV: MessageService,
    private cdref: ChangeDetectorRef, private route: Router
  ) { }
  ngOnInit() {
    this.onsubmit()
  }
  ngAfterContentChecked() {
    this.cdref.detectChanges();
  }
  onsubmit() {
    this.paging.keyword = this.keyword;
    this.exportBillService.Search(this.paging).subscribe({
      next: (res) => {
        this.listExportBill = res.data;
        this.Totalcount = res.totalFilter;
      },
      error: (error: HttpErrorResponse) => {
        debugger
        if (error.status === 401) {
          this.route.navigate(['/admin/unauthorized'])
        } else if (error.status === 403) {
          this.route.navigate(['/admin/unauthorized'])
        } else {
          this.route.navigate(['/admin/unauthorized'])
        }
      },
      complete: () => {
        this.loading = false;
      },
    });
  }
  loadListLazy = (event: any) => {
    this.loading = true;
    let pageSize = event.rows;
    let pageIndex = event.first / pageSize + 1;
    this.paging = {
      pageIndex: pageIndex,
      pageSize: pageSize,
      keyword: this.keyword,
    };
    this.exportBillService.Search(this.paging).subscribe({
      next: (res) => {
        this.listExportBill = res.data;
        this.Totalcount = res.totalFilter;
      },
      error: (error: HttpErrorResponse) => {
        if (error.status === 401) {
          this.route.navigate(['/admin/unauthorized'])
        } else if (error.status === 403) {
          this.route.navigate(['/admin/unauthorized'])
        } else {
          this.route.navigate(['/admin/unauthorized'])
        }
      },
      complete: () => {
        this.loading = false;
      },
    });
  };
  GetDetail(id: string) {
    this.visible = true;
    this.exportBillService.GetDetail(id).subscribe({
      next: (res) => {
        if (res) {
          this.listDetail = res;
        }
      }, error: (error: HttpErrorResponse) => {
        debugger
        if (error.status === 401) {
          this.route.navigate(['/admin/unauthorized'])
        } else if (error.status === 403) {
          this.route.navigate(['/admin/unauthorized'])
        } else {
          this.route.navigate(['/admin/unauthorized'])
        }
      }

    })
  }
  confirm(event: Event, data: string) {
    this.confirmationService.confirm({
      target: event.target as EventTarget,
      message: `Bạn có muốn xóa mã ${data}?`,
      icon: 'pi pi-info-circle',
      acceptButtonStyleClass: 'p-button-danger p-button-sm',
      accept: () => {
        if (data) {
          this.exportBillService.Delete(data).subscribe({
            next: res => {
              if (res) {
                this.MessageSV.add({ severity: 'error', summary: 'Error', detail: 'Xóa thành công' })
                this.onsubmit();
              }
            },
            error: (error: HttpErrorResponse) => {
              debugger
              if (error.status === 401) {
                this.route.navigate(['/admin/unauthorized'])
              } else if (error.status === 403) {
                this.route.navigate(['/admin/unauthorized'])
              } else {
                this.route.navigate(['/admin/unauthorized'])
              }
            }
          })
        }
      },
      reject: () => {
        this.MessageSV.add({ severity: 'error', summary: 'Rejected', detail: 'You have rejected', life: 3000 });
      }
    });
  }
}
