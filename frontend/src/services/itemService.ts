export interface Item {
    id: number;
    name: string;
}

let itemsCache: Item[] | null = null;


export async function getItems(): Promise<Item[]> {

    if (itemsCache)
        return itemsCache;


    const response = await fetch(
        "https://ddragon.leagueoflegends.com/cdn/15.15.1/data/en_US/item.json"
    );

    const json = await response.json();


    itemsCache = Object.entries(json.data)
        .map(([id, item]: [string, any]) => ({
            id: Number(id),
            name: item.name
        }))
        .filter(item => item.name.length > 0)
        .sort((a, b) => a.name.localeCompare(b.name));


    return itemsCache;
}