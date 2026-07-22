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

export interface TeamChampionStats {
  championName: string;
  games: number;
  wins: number;
  winRate: number;
  successfulPlacements: number;
  successfulPlacementRate: number;
  averagePlacement: number;
}

export interface ChampionStats {
  championName: string;
  games: number;
  wins: number;
  winRate: number;
  successfulPlacements: number;
  successfulPlacementRate: number;
  averagePlacement: number;
}

export interface PerformanceStats {
    averageKills: number;
    averageDeaths: number;
    averageAssists: number;
    averageKda: number;

    averageDamageDealt: number;
    averageDamageTaken: number;
    averageHealing: number;
    averageShielding: number;
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

    performanceStats: PerformanceStats;

    teamChampionStats: TeamChampionStats[];

    championStats: ChampionStats[];
    
}