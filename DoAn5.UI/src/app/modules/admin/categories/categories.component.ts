import { Component, ViewChild } from '@angular/core';
import { CategoriesDto, Paging } from 'src/app/model';

import { CategoriesService } from 'src/app/service';
import { PrimeIcons, MenuItem } from 'primeng/api';
import { Table } from 'primeng/table';




@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})
export class CategoriesComponent  {
  @ViewChild('dt') table!: Table;
  Dscategories:any[]=[];
  loading: boolean = true;
  paging!: Paging;
  
  keyword:string='';
  Categories!:CategoriesDto[];
  datas:CategoriesDto[]=[];
  dataTotalRecords!: number;

   constructor(
    private categoriesService:CategoriesService,){
    }

   ngOnInit(){
    this.LoadCategories();
    
   }
   ngAfterViewInit() {
  }
   LoadCategories(){
    this.categoriesService.getAll().subscribe((data)=>{
      this.Dscategories=data
    })
   }
   loadListLazy = (event: any) => {
    this.loading = true;
    let pageSize = event.rows;
    let pageIndex = event.first / pageSize + 1;
    this.paging = {
      pageIndex: pageIndex,
      pageSize: pageSize,
      keyword: this.keyword,
      orderBy: event.sortField
        ? `${event.sortField} ${event.sortOrder === 1 ? 'asc' : 'desc'}`
        : '',
    };
    this.categoriesService.Search(this.paging).subscribe({
      next: (res) => {
        this.datas = res.data;
        this.dataTotalRecords = res.totalFilter;
      },
      error: (e) => {
        this.loading = false;
      },
      complete: () => {
        this.loading = false;
      },
    });
  };
  onSubmitSearch = () => {
    this.paging.keyword = this.keyword;
    this.categoriesService.Search(this.paging).subscribe({
      next: (res) => {
        this.datas = res.data;
        this.dataTotalRecords = res.totalFilter;
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
