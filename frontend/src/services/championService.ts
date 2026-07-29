export interface Champion {
    id: string;
    name: string;
}

let championsCache: Champion[] | null = null;

export async function getChampions(): Promise<Champion[]> {

    if (championsCache)
        return championsCache;

    const response = await fetch(
        "https://ddragon.leagueoflegends.com/cdn/15.15.1/data/en_US/champion.json"
    );

    const json = await response.json();

    championsCache = Object.values(json.data)
        .map((c: any) => ({
            id: c.id,
            name: c.name
        }))
        .sort((a, b) => a.name.localeCompare(b.name));

    return championsCache;
}