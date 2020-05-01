import { ItemDayDTO } from './ItemDayDTO.model';

export class ItemDTO {
    id: string;
    name: string;
    price: number;
    groupId: number | null;
    itemDays: ItemDayDTO[];
}