import { BasedbDto } from "./Common/BaseDto";


export interface OrderDto extends BasedbDto  {
    id_customer: string;
    status: number;
    price: number;
    address:string;
    payment:string;

}

export interface CreateOrderDto  {
    id_customer: string;
    status: number;
    price: number;
    address:string;
    payment:string;
    orderList: Order_detailDto[]; 
}

export interface Order_detailDto {
    // id_Order: string|undefined;
    id_product: string;
    idsize: string;
    quantity: number;
    price: number;
}