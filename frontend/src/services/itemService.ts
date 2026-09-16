import { arenaApi } from "../api/arenaApi";

export interface Item {
    id: number;
    name: string;
    icon: string;
}

let itemsCache: Item[] | null = null;

export async function getItems(): Promise<Item[]> {
    if (itemsCache)
        return itemsCache;

    const response = await arenaApi.get<Omit<Item, "icon">[]>("/metadata/items");
    const items = response.data;

    itemsCache = items
        .map(item => ({
            ...item,
            icon: `https://ddragon.leagueoflegends.com/cdn/15.15.1/img/item/${item.id}.png`
        }))
        .sort((a, b) => a.name.localeCompare(b.name));

    return itemsCache;
}