import axios from "axios";
import qs from "qs";

import type { PlayerStats } from "../types/PlayerStats";
import type { StatsFilter } from "../types/StatsFilter";

export const arenaApi = axios.create({
    baseURL: import.meta.env.VITE_API_URL,
    paramsSerializer: params =>
        qs.stringify(params, { arrayFormat: "repeat" })
});

export async function getPlayerStats(
    gameName: string,
    tagLine: string,
    filter: StatsFilter
): Promise<PlayerStats> {

    const response = await arenaApi.get<PlayerStats>(
        "/player/stats",
        {
            params: {
                gameName,
                tagLine,
                ...filter
            }
        });

    return response.data;
}

export async function syncPlayer(
    gameName: string,
    tagLine: string
): Promise<void> {

    await arenaApi.post(
        "/player/sync",
        null,
        {
            params: {
                gameName,
                tagLine
            }
        }
    );
}

export async function getMatchHistory(
    gameName: string,
    tagLine: string,
    filter: StatsFilter
) {
    const response = await arenaApi.get(
        "/player/match-history",
        {
            params: {
                gameName,
                tagLine,
                ...filter
            }
        });

    return response.data;
}