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