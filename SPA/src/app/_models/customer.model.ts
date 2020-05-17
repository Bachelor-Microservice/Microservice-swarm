import { Booking } from './booking.model';

export class Customer {
    id: string;
    supplementName: string;
    type: string;
    registrationDate: Date;
    email: string;
    address: string;
    telephonePrimary: string;
    mobilePhone: string;
    bookings: Booking[];

    constructor() {
        this.bookings = [];
    }
}