import { BookedDayDTO } from './BookedDayDTO.model';

export interface CreateBookingDTO {
    arrival: string;
    email: string;
    depature: string;
    price: number;
    customerid: string;
    customerName: string;
    itemDescription: string;
    itemName: string;
    itemNo: string;
    bookedDays: BookedDayDTO[];
    currency: string;
}