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


            <v-alert v-if="analysisError" type="error" variant="tonal" class="mt-4" role="alert">
                {{ analysisError }}
            </v-alert>

            <v-divider v-if="stats" class="my-8" />

            <v-autocomplete v-model="selectedFilters" :items="filterOptions" item-title="name" return-object multiple
                chips closable-chips label="Filter dataset by champion, item or augment..."
                @update:model-value="updateFilters">
                <!-- Dropdown items -->
                <template #item="{ props, item }">
                    <v-list-item v-bind="props">
                        <template #prepend>
                            <v-avatar size="28">
                                <v-img v-if="item.icon" :src="item.icon" :alt="item.name" cover />
                            </v-avatar>
                        </template>
                    </v-list-item>
                </template>

                <!-- Selected chips -->
                <template #chip="{ props, item }">
                    <v-chip v-bind="props">
                        <template #prepend>
                            <v-avatar size="18">
                                <v-img v-if="item.icon" :src="item.icon" :alt="item.name" cover />
                            </v-avatar>
                        </template>

                        {{ item.name }}
                    </v-chip>
                </template>
            </v-autocomplete>


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

                    <AugmentsTab :augment-stats="stats!.augmentStats" />

                </v-window-item>

                <v-window-item value="matches">

                    <MatchHistoryTab :match-history="matchHistory" />

                </v-window-item>

            </v-window>

        </v-card>

    </v-container>
</template>


<script setup lang="ts">
import { onMounted, ref, watch } from "vue";
import StatCard from "../components/StatCard.vue";
import OverallTab from "../components/OverallTab.vue";
import ItemsTab from "../components/ItemsTab.vue";
import AugmentsTab from "../components/AugmentsTab.vue";
import MatchHistoryTab from "../components/MatchHistoryTab.vue";
import { getChampions } from "../services/championService";
import { getItems } from "../services/itemService";
import { getAugments } from "../services/augmentService";
import { getPlayerStats, syncPlayer, getMatchHistory } from "../api/arenaApi";
import type { PlayerStats } from "../types/PlayerStats";
import type { MatchHistory } from "../types/MatchHistory";
import type { StatsFilter } from "../types/StatsFilter";
import type { FilterOption } from "../types/FilterOption";


const gameName = ref(localStorage.getItem("gameName") ?? "");
const tagLine = ref(localStorage.getItem("tagLine") ?? "");

const filter = ref<StatsFilter>({});

const filterOptions = ref<FilterOption[]>([]);

const selectedFilters = ref<FilterOption[]>([]);

onMounted(async () => {

    const champions = await getChampions();
    const items = await getItems();
    const augments = await getAugments();

    filterOptions.value = [
        ...champions.map<FilterOption>(c => ({
            type: "champion",
            id: c.name,
            name: c.name,
            icon: c.icon
        })),

        ...items.map<FilterOption>(i => ({
            type: "item",
            id: i.id,
            name: i.name,
            icon: i.icon
        })),

        ...augments.map<FilterOption>(a => ({
            type: "augment",
            id: a.id,
            name: a.name,
            icon: a.icon
        }))
    ];

});

function updateFilters(options: FilterOption[]) {

    filter.value = {};

    const champion = options.find(
        x => x.type === "champion"
    );

    if (champion) {
        filter.value.championName = champion.name;
    }


    const items = options
        .filter(x => x.type === "item")
        .map(x => Number(x.id));


    if (items.length > 0) {
        filter.value.itemIds = items;
    }


    const augments = options
        .filter(x => x.type === "augment")
        .map(x => Number(x.id));


    if (augments.length > 0) {
        filter.value.augmentIds = augments;
    }

    if (stats.value) {
        refreshData();
    }

}

async function refreshData() {

    stats.value = await getPlayerStats(
        gameName.value,
        tagLine.value,
        filter.value
    );

    if (currentTab.value === "matches") {

        matchHistory.value = await getMatchHistory(
            gameName.value,
            tagLine.value,
            filter.value
        );

    }
}

const stats = ref<PlayerStats | null>(null);
const matchHistory = ref<MatchHistory[]>([]);

const currentTab = ref("overall");

watch(currentTab, async (tab) => {

    if (tab !== "matches")
        return;

    matchHistory.value = await getMatchHistory(
        gameName.value,
        tagLine.value,
        filter.value
    );

});

const loading = ref(false);
const analysisError = ref("");

async function analyze() {

    analysisError.value = "";
    loading.value = true;
    stats.value = null;
    matchHistory.value = [];

    localStorage.setItem("gameName", gameName.value);
    localStorage.setItem("tagLine", tagLine.value);

    try {

        await syncPlayer(
            gameName.value,
            tagLine.value
        );

        stats.value = await getPlayerStats(
            gameName.value,
            tagLine.value,
            filter.value
        );

    }
    catch {

        analysisError.value = "Unable to analyze this player. Check the Riot ID and tag, make sure the backend is running with a valid Riot API key, and try again.";

    }
    finally {

        loading.value = false;

    }
}

</script>