import { Component, OnInit } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Paging } from 'src/app/model';
import { ProductsService } from 'src/app/service/products.service';
import { RatingService } from 'src/app/service/rating.service';

@Component({
  selector: 'app-rating',
  templateUrl: './rating.component.html',
  styleUrl: './rating.component.css'
})
export class RatingComponent implements OnInit {
  paging: Paging = { keyword: '', pageIndex: 1, pageSize: 10 };
  listRating: any;
  loading: boolean = false;
  keyword: string = '';
  Totalcount!: number;
  constructor(private ratingService: RatingService, private productService: ProductsService, private confirmationService: ConfirmationService,private MessageSV: MessageService){ }
  ngOnInit(): void {
    this.onSubmit();
  }

 
  onSubmit() {
    this.loading = true;
    this.paging.keyword = this.keyword;
    this.ratingService.Search(this.paging).subscribe({
      next: (res) => {
        if (res) {
          this.listRating = res.data;
          console.log(this.listRating)
          this.loading = false; 
        }
      }
    })
  }
  loadListLazy (event: any)  {
    this.loading = true;
    let pageSize = event.rows;
    let pageIndex = event.first / pageSize + 1;
    this.paging = {
      pageIndex: pageIndex,
      pageSize: pageSize,
      keyword: this.keyword,
    };
    this.ratingService.Search(this.paging).subscribe({
      next: (res) => {
        this.listRating = res.data;
        this.Totalcount = res.totalFilter;
      },
      error: (e) => {
        this.loading = false;
      },
      complete: () => {
        this.loading = false;
      },
    });
  };
  update(data:any){
    if( data.status!=2){
      data.status=2;
      this.ratingService.Update(data).subscribe({
        next:(res)=>{
          if(res){
            this.MessageSV.add({ severity: 'success', summary: 'Thành công', detail: 'Đổi trạng thái thành công' })
          }
        }
      })
    }else{
      this.MessageSV.add({ severity: 'warn', summary: 'cảnh báo', detail: 'Đã đổi trạng thái' })

    }
    
  }
  confirm(event: Event, data: string) {
    this.confirmationService.confirm({
      target: event.target as EventTarget,
      message: `Bạn có muốn xóa mã ${data}?`,
      icon: 'pi pi-info-circle',
      acceptButtonStyleClass: 'p-button-danger p-button-sm',
      accept: () => {
        if (data) {
          this.ratingService.Delete(data).subscribe({
            next: res => {
              if (res) {
                this.MessageSV.add({ severity: 'error', summary: 'Error', detail: 'Xóa thành công' })
                this.onSubmit();
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
