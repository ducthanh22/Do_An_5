import { BasedbDto } from "./Common/BaseDto";

export interface ImportbillDto extends BasedbDto {
    price: number;
    status: number;
    idStaff: string;
}

export interface CreateImportbillDto  {
    price: number;
    status: number;
    idStaff: string;
    detail_importbill: Detail_importbill[];
}
export interface Detail_importbill {
    idExportbill: string;
    idproduct: string;
    idsize: string;
    price: number;
    quantity: number;
}