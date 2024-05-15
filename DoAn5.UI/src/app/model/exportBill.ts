import { BasedbDto } from "./Common/BaseDto";


export interface ExportbillDto extends BasedbDto {
    price: number;
    status: number;
    idStaff: number;
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