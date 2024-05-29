import { BasedbDto } from "./Common/BaseDto";


export interface Detail_warehouseDto extends BasedbDto {
    idwarehouse: string;
    idproduct: string;
    idsize: string;
    quantity: number;
}

export interface GetDetail_warehouseDto extends BasedbDto {
    idwarehouse: string;
    idproduct: string;
    nameProduct: string;
    image: string;
    idsize: string;
    nameSize: string;
    quantity: number;
    nameColor: string;
}