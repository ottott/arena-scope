export interface PlacementDistribution {
    placement: number;
    games: number;
}

export interface DuoStats {
    gameName: string;
    tagLine: string;
    games: number;
    wins: number;
    winRate: number;
    successfulPlacements: number;
    successfulPlacementRate: number;
    averagePlacement: number;
}

export interface PlayerStats
{
    gameName: string;

    tagLine: string;

    games: number;

    wins: number;

    winRate: number;

    successfulPlacements: number;

    successfulPlacementRate: number;

    averagePlacement: number;

    placementDistribution: PlacementDistribution[];

    duoStats: DuoStats[];
    
}