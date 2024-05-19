import { BasedbDto } from "./Common/BaseDto";


export interface ExportbillDto  {
    price: number;
    status: number;
    idStaff: string;
    userName:string
}

export interface CreateExportbillDto  {
    price: number;
    status: number;
    idStaff: string;
    detail_exportbillDto: Detail_exportbillDto[];
}
export interface Detail_exportbillDto extends BasedbDto {
    idExportbill: string;
    idproduct: string;
    idsize: string;
    price: number;
    quantity: number;
}
export interface countProduct {
    idproduct: string;
    totalQuantity: number;
}

export interface GetDetail_exportbillDto extends BasedbDto {
    idExportbill: string;
    idproduct: string;
    idsize: string;
    price: number;
    quantity: number;
    productName: string;
    image: string;
    userName: string;
    address: string;
    phone: string;
    email: string;
    toTal: number;
}