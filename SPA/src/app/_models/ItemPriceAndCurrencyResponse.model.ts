import { Group } from './Group.model';

export class ItemPriceAndCurrencyResponse {
    currency: string;
    groups: Group[];

    constructor() {
        this.groups = [];
    }
}