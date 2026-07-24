import axios from "axios";
import type { PlayerStats } from "../types/PlayerStats";

const arenaApi = axios.create({
    baseURL: "http://localhost:5271/api"
});

export async function getPlayerStats(
    gameName: string,
    tagLine: string
): Promise<PlayerStats> {

    const response = await arenaApi.get<PlayerStats>(
        "/player/stats",
        {
            params: {
                gameName,
                tagLine
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
    tagLine: string
) {
    const response = await arenaApi.get(
        "/player/match-history",
        {
            params: {
                gameName,
                tagLine
            }
        });

    return response.data;
}