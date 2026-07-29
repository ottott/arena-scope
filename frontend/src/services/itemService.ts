export interface Item {
    id: number;
    name: string;
}

let itemsCache: Item[] | null = null;


export async function getItems(): Promise<Item[]> {

    if (itemsCache)
        return itemsCache;


    const response = await fetch(
        "http://localhost:5271/api/metadata/items"
    );

    itemsCache = await response.json() as Item[];


    return itemsCache;
}