import { BasedbDto } from "./Common/BaseDto";


export interface OrderDto extends BasedbDto {
    id_customer: number;
    status: number;
    price: number;
}

export interface CreateOrderDto extends BasedbDto {
    id_customer: number;
    status: number;
    price: number;
    orderList: Order_detailDto[];
}

export interface Order_detailDto extends BasedbDto {
    id_Order: string;
    id_product: string;
    idsize: string;
    quantity: number;
    price: number;
}