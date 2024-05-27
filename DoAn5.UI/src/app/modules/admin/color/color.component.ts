import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { ColorDto, Paging } from 'src/app/model';
import { ColorService } from 'src/app/service/color.service';

@Component({
  selector: 'app-color',
  templateUrl: './color.component.html',
  styleUrls: ['./color.component.css']
})
export class ColorComponent {
  loading: boolean = false;
  keyword: string = '';
  paging: Paging = { keyword: '', pageIndex: 1, pageSize: 10 };
  datas: ColorDto[] = [];
  Totalcount!: number;
  visible: boolean = false;
  checkbtn!: boolean;
  Titile: string = '';
  formColor!: FormGroup;
  getIdColor!:string;
  constructor(private colorService: ColorService, private fb: FormBuilder, private MessageSV: MessageService) { }
  ngOnInit() {
    this.formColor = this.fb.group({
      nameColor: new FormControl('', Validators.required),
      colorformat: new FormControl('#6466f1', Validators.required),
    })
    this.formColor.get('colorformat')?.valueChanges.subscribe(value => {
      if (/^#[0-9A-F]{6}$/i.test(value)) {
        this.formColor.get('colorformat')?.setValue(value, { emitEvent: false });
      }
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
    this.colorService.Search(this.paging).subscribe({
      next: (res) => {
        this.datas = res.data;
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
  onsubmit() {
    this.paging.keyword = this.keyword;
    this.colorService.Search(this.paging).subscribe({
      next: (res) => {
        this.datas = res.data;
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
  Add() {
    this.visible = true;
    this.checkbtn = true;
    this.Titile = "Thêm màu";
  }
  edit(data:ColorDto) {
    this.visible = true;
    this.checkbtn = false;
    this.Titile = "Sửa màu";
    this.formColor.controls['nameColor'].setValue(data.nameColor);
    this.formColor.controls['colorformat'].setValue(data.colorformat);
    this.getIdColor=data.id;


  }
  SubmitBtn() {
    this.checkbtn == true ? this.save() : this.upDate()
  }
  close() {
    this.visible = !this.visible;
    this.formColor.reset();
  }
  save() {
    if (this.formColor) {
      this.colorService.create(this.formColor.value).subscribe({
        next: (res) => {
          if (res) {
            this.close();
            this.onsubmit();
            this.MessageSV.add({ severity: 'success', summary: 'Success', detail: 'Thêm thành công' });

          }
        }
      })
    }
  }

  upDate() {
    if (this.formColor) {
      this.formColor.value.id=this.getIdColor;
      this.colorService.Update(this.formColor.value).subscribe({
        next: (res) => {
          if (res) {
            this.close();
            this.onsubmit();
            this.MessageSV.add({ severity: 'success', summary: 'Success', detail: 'Thêm thành công' });

          }
        }
      })
    }
  }
}
