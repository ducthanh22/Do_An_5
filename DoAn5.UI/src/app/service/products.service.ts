import { Injectable ,EventEmitter } from '@angular/core';
import { BaseService } from './Common/base.service';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environment/environment';
import { GetProductsDto, ProductsDto, bestSellingProducts } from '../model';
import { Observable, first } from 'rxjs';
import { BaseCommandResponse } from '../model/Common/BaseCommandResponse';

@Injectable({
  providedIn: 'root'
})
export class ProductsService extends BaseService<ProductsDto>{
  cartUpdated = new EventEmitter<void>();
  constructor(http: HttpClient) {
    super(http, `${environment.apiUrl}/Product`);
  }
  Upload(data: FormData): Observable<BaseCommandResponse> {
    return this._http
      .post<BaseCommandResponse>(`${environment.apiUrl}/Product/UploadFile`, data)
      .pipe(first());
  }
  Getproductnew():Observable<GetProductsDto[]>{
    return this._http.get<GetProductsDto[]>(`${environment.apiUrl}/Product/GetProductNew`)
    .pipe(first());
  }
  GetproductSale():Observable<GetProductsDto[]>{
    return this._http.get<GetProductsDto[]>(`${environment.apiUrl}/Product/GetProductSale`)
    .pipe(first());
  }
  GetBestSellingProducts():Observable<bestSellingProducts[]>{
    return this._http.get<bestSellingProducts[]>(`${environment.apiUrl}/Product/GetBestSellingProducts`)
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
    sessionStorage.setItem('cart',jsonCart);
    this.cartUpdated.emit(); 
  }
}
