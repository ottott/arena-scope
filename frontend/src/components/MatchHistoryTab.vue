<template>
  <v-card class="pa-6">

    <h2 class="text-h5 mb-4">
      Match History
    </h2>

    <v-data-table
      :headers="matchHeaders"
      :items="matchHistory"
      items-per-page="10"
    >

      <template #item.gameCreation="{ item }">
        {{ new Date(item.gameCreation).toLocaleDateString() }}
      </template>

      <template #item.gameDuration="{ item }">
        {{ formatDuration(item.gameDuration) }}
      </template>

      <template #item.kda="{ item }">
        {{ item.kills }} / {{ item.deaths }} / {{ item.assists }}
      </template>

    </v-data-table>

  </v-card>
</template>

<script setup lang="ts">

import type { MatchHistory } from "../types/MatchHistory";

const matchHeaders = [
    { title: "Date", key: "gameCreation" },
    { title: "Champion", key: "championName" },
    { title: "Placement", key: "placement" },
    { title: "K / D / A", key: "kda", sortable: false },
    { title: "Duration", key: "gameDuration" }
];

defineProps<{
    matchHistory: MatchHistory[];
}>();

function formatDuration(seconds: number) {

    const minutes = Math.floor(seconds / 60);
    const remainingSeconds = seconds % 60;

    return `${minutes}:${remainingSeconds.toString().padStart(2, "0")}`;
}

</script>