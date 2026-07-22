<template>
    <v-container class="py-10" max-width="900">

        <v-card class="pa-6">

            <h1 class="text-h4 font-weight-bold mb-2">
                Arena Scope
            </h1>

            <p class="text-medium-emphasis mb-6">
                Personal Arena Statistics Explorer
            </p>

            <v-row align="center">

                <v-col cols="12" md="5">
                    <v-text-field v-model="gameName" label="Riot ID" variant="outlined" hide-details />
                </v-col>

                <v-col cols="12" md="3">
                    <v-text-field v-model="tagLine" label="Tag" variant="outlined" hide-details />
                </v-col>

                <v-col cols="12" md="2">
                    <v-btn color="primary" size="large" block :loading="loading" :disabled="loading" @click="analyze">
                        Analyze
                    </v-btn>
                </v-col>

            </v-row>


            <v-divider v-if="stats" class="my-8" />

            <v-text-field v-if="stats" label="Filter dataset by champion, item or augment..."
                prepend-inner-icon="mdi-magnify" variant="outlined" clearable />



            <v-row v-if="stats" class="mt-6">

                <v-col cols="12" sm="6" md="3">
                    <StatCard title="Games" :value="stats.games" />
                </v-col>

                <v-col cols="12" sm="6" md="3">
                    <StatCard title="Average Placement" :value="stats.averagePlacement.toFixed(2)" />
                </v-col>

                <v-col cols="12" sm="6" md="3">
                    <StatCard title="Top 3 Rate" :value="stats.successfulPlacementRate.toFixed(1) + '%'" />
                </v-col>

                <v-col cols="12" sm="6" md="3">
                    <StatCard title="Top 1 Rate" :value="stats.winRate.toFixed(1) + '%'" />
                </v-col>

            </v-row>

            <v-tabs v-if="stats" v-model="currentTab" class="mt-4">
                <v-tab value="overall">
                    Overall
                </v-tab>

                <v-tab value="items">
                    Items
                </v-tab>

                <v-tab value="augments">
                    Augments
                </v-tab>

                <v-tab value="matches">
                    Matches
                </v-tab>
            </v-tabs>

            <v-window v-if="stats" v-model="currentTab" class="mt-6">

                <v-window-item value="overall">
                    <OverallTab :placement-distribution="stats!.placementDistribution"
                        :performance-stats="stats!.performanceStats" :duo-stats="stats!.duoStats"
                        :team-champion-stats="stats!.teamChampionStats" :champion-stats="stats!.championStats" />
                </v-window-item>

                <v-window-item value="items">

                    <ItemsTab :item-stats="stats!.itemStats" />

                </v-window-item>

                <v-window-item value="augments">
                    
                    <AugmentsTab :augment-stats="stats!.augmentStats"/>

                </v-window-item>

                <v-window-item value="matches">
                    <p>Matches table goes here.</p>
                </v-window-item>

            </v-window>

        </v-card>

    </v-container>
</template>


<script setup lang="ts">
import { ref } from "vue";
import StatCard from "../components/StatCard.vue";
import OverallTab from "../components/OverallTab.vue";
import ItemsTab from "../components/ItemsTab.vue";
import AugmentsTab from "../components/AugmentsTab.vue";
import { getPlayerStats, syncPlayer } from "../api/arenaApi";
import type { PlayerStats } from "../types/PlayerStats";


const gameName = ref(localStorage.getItem("gameName") ?? "");
const tagLine = ref(localStorage.getItem("tagLine") ?? "");
const stats = ref<PlayerStats | null>(null);

const currentTab = ref("overall");

const loading = ref(false);

async function analyze() {

    loading.value = true;

    localStorage.setItem("gameName", gameName.value);
    localStorage.setItem("tagLine", tagLine.value);

    try {

        await syncPlayer(
            gameName.value,
            tagLine.value
        );

        stats.value = await getPlayerStats(
            gameName.value,
            tagLine.value
        );

    }
    catch (error) {

        console.error(error);

    }
    finally {

        loading.value = false;

    }
}

</script>