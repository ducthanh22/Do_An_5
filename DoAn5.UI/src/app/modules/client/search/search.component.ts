import { Component, OnInit } from '@angular/core';
import { Paging } from 'src/app/model/Paging';
import { ShareService } from 'src/app/service/Common/share.service';
import { ProductsService } from 'src/app/service/products.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  loading: boolean = true;
  paging: Paging = { keyword: "", pageIndex: 1, pageSize: 10 };
  ListProducts: any[] = [];
  Totalcount!: number;
  receivedKeyword: string = '';
  layout: 'grid' | 'list' = 'grid';

  constructor(private ProductSV: ProductsService, private ShareService: ShareService) { }

  ngOnInit() {
    this.ShareService.keyword$.subscribe(keyword => {
      if (this.receivedKeyword !== keyword) {
        this.receivedKeyword = keyword;
        this.loadData(this.receivedKeyword);
      }
    });
  }

  loadData(data: any) {
    this.paging.keyword = data;
    this.ProductSV.Search(this.paging).subscribe({
      next: (res) => {
        this.ListProducts = res.data;
        this.Totalcount = res.totalFilter;
      },
      error: (err) => {
        console.error('Error fetching products', err);
      },
      complete: () => {
        this.loading = false;
      }
    });
  }

  loadListLazy = (event: any) => {
    this.loading = true;
    let pageSize = event.rows;
    let pageIndex = Math.floor(event.first / pageSize) + 1;
    this.paging = {
      pageIndex: pageIndex,
      pageSize: pageSize,
      keyword: this.receivedKeyword,
    };
    this.ProductSV.Search(this.paging).subscribe({
      next: (res) => {
        this.ListProducts = res.data;
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
}
