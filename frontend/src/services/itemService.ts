export interface Item {
    id: number;
    name: string;
    icon: string;
}

let itemsCache: Item[] | null = null;

export async function getItems(): Promise<Item[]> {
    if (itemsCache)
        return itemsCache;

    const response = await fetch(
        "http://localhost:5271/api/metadata/items"
    );

    const items = await response.json() as Omit<Item, "icon">[];

    itemsCache = items
        .map(item => ({
            ...item,
            icon: `https://ddragon.leagueoflegends.com/cdn/15.15.1/img/item/${item.id}.png`
        }))
        .sort((a, b) => a.name.localeCompare(b.name));

    return itemsCache;
}