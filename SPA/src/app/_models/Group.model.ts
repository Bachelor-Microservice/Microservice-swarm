import { Item } from './Item.model';

export class Group {
    id: number;
    description: string;
    items: Item[];

    constructor() {
        this.items = [];
    }
}