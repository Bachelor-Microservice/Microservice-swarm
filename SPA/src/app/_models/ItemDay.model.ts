import { CustomerType } from './CustomerType.model';

export class ItemDay {
    id: number;
    date: Date;
    price: number;
    priority: string;
    pricePackage: string;
    customerType: CustomerType;
}