import { BasedbDto } from "./Common/BaseDto";


export interface Produces extends BasedbDto {
    name: string;
    address: string;
    email: string;
    phone: string;
    image: string;
}

export interface UpFile {
    id: string;
    img: any;
    image: string | null;
}