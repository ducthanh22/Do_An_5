import { BasedbDto } from "./Common/BaseDto";


export interface SaleDto extends BasedbDto {
    idProduct: string;
    salePrice: number;
    percent: number;
    saleTime: number;
}

export interface GetSaleDto extends BasedbDto {
    idProduct: string;
    salePrice: number;
    percent: number;
    saleTime: number;
    name: string;
    image: string;
    price_product: number;
}