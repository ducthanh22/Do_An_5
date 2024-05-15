import { ChangeDetectorRef, Component } from '@angular/core';
import { MessageService } from 'primeng/api';
import { Paging } from 'src/app/model';
import { OrderService } from 'src/app/service/order.service';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent {
  paging: Paging = { keyword: "", pageIndex: 1, pageSize: 10 };
  listOrder:any;
  totalCount!:number;
  keyword:string="";
  loading!:boolean;
  visible:boolean=false;
  dataGetId:any[]=[];
  constructor(private OrderService:OrderService, private messageService:MessageService,private changeDetector: ChangeDetectorRef){}
  ngOnInit(){

  }
  onsubmit = () => {
    this.paging.keyword = this.keyword;
    this.OrderService.Search(this.paging).subscribe({
      next: (res) => {
        this.listOrder = res.data;
        this.totalCount = res.totalFilter;
      },
      error: (e) => {
        this.loading = false;
      },
      complete: () => {
        this.loading = false;
      },
    });

  }
  ngAfterContentChecked() {
    this.changeDetector.detectChanges();
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
    this.OrderService.Search(this.paging).subscribe({
      next: (res) => {
        this.listOrder = res.data;
        this.totalCount = res.totalFilter;
      },
      error: (e) => {
        this.loading = false;
      },
      complete: () => {
        this.loading = false;
      },
    });
  };

  Update(data:any,status:number){

    this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Đổi trạng thái thành công' })

  }

  getOrderDetail(data:string){
    this.visible=true;
    this.OrderService.getbyid(data).subscribe({
      next:(res)=>{
        if(res){
          this.dataGetId=res;
        }
      }
    })
  }
}
