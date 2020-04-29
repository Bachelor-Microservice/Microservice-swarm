import { ItemPriceAndCurrencyResponse } from './ItemPriceAndCurrencyResponse.model';

export class ItemPriceDTO {
    data: ItemPriceAndCurrencyResponse[];
    success: boolean;
    message: string;

    constructor() {
        this.data = [];
    }

} 