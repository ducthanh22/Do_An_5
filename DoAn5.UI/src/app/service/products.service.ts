import { Injectable } from '@angular/core';
import { BaseService } from './Common/base.service';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environment/environment';
import { ProductsDto } from '../model';
import { Observable, first } from 'rxjs';
import { BaseCommandResponse } from '../model/Common/BaseCommandResponse';

@Injectable({
  providedIn: 'root'
})
export class ProductsService extends BaseService<ProductsDto>{
  constructor(http: HttpClient) {
    super(http, `${environment.apiUrl}/Product`);
  }
  Upload(data: FormData): Observable<BaseCommandResponse> {
    return this._http
      .post<BaseCommandResponse>(`${environment.apiUrl}/Product/UploadFile`, data)
      .pipe(first());
  }
  Getproductnew():Observable<ProductsDto[]>{
    return this._http.get<ProductsDto[]>(`${environment.apiUrl}/Product/GetProductNew`)
    .pipe(first());
  }


  GetCart(){
    let jsonCart =sessionStorage.getItem('cart');
    if(jsonCart){
      return JSON.parse(jsonCart)
    }else{
      return []
    }
  }
  saveCart(cart:any){
    let jsonCart = JSON.stringify(cart);
    sessionStorage.setItem('cart',jsonCart)
  }
}
