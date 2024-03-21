import { BasedbDto } from "./Common/BaseDto";



export interface ProductsDto extends BasedbDto {
    name: string;
    idcategories: string;
    idproduces: string;
    describe: string;
    image: string;
    idcolor: string;
    idsize: string;
    price_product: number | null;
}

// export interface UpLoadFile {
//     id: string;
//     img: any;
//     image: string | null;
// }