export interface Augment {
    id: number;
    name: string;
}


let augmentCache: Augment[] | null = null;


export async function getAugments(): Promise<Augment[]> {

    if (augmentCache)
        return augmentCache;


    const response = await fetch(
        "https://raw.communitydragon.org/latest/cdragon/arena/en_us.json"
    );


    const json = await response.json();
    

    const augments: Augment[] = json.augments
        .map((augment: any) => ({
            id: augment.id,
            name: augment.name
        }))
        .sort((a: Augment, b: Augment) =>
            a.name.localeCompare(b.name)
        );


    augmentCache = augments;


    return augments;
}