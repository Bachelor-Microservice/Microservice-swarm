import { BookedDay } from './BookedDay.model';

export interface Booking {
    id: string;
    arrival: string;
    depature: string;
    price: number;
    customerid: string;
    customerName: string;
    email: string;
    itemDescription: string;
    itemName: string;
    itemNo: string;
    bookedDays: BookedDay[];
    currency: string;
}