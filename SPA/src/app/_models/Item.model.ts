import { ItemDay } from './ItemDay.model';

export class Item {
    id: string;
    name: string;
    price: number;
    itemDays: ItemDay[];

    constructor() {
        this.itemDays = [];
    }
}