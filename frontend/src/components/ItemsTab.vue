<template>
    <v-card class="pa-6">

        <v-data-table :headers="itemHeaders" :items="itemStats" items-per-page="10">

            <template #item.averagePlacement="{ item }">
                {{ item.averagePlacement.toFixed(2) }}
            </template>

            <template #item.successfulPlacementRate="{ item }">
                {{ item.successfulPlacementRate.toFixed(1) }}%
            </template>

            <template #item.winRate="{ item }">
                {{ item.winRate.toFixed(1) }}%
            </template>

        </v-data-table>

    </v-card>
</template>

<script setup lang="ts">

interface ItemStats {
    itemId: number;
    itemName: string;

    games: number;
    wins: number;
    winRate: number;
    successfulPlacements: number;
    successfulPlacementRate: number;
    averagePlacement: number;
}

const itemHeaders = [
    { title: "Item", key: "itemName" },
    { title: "Games", key: "games" },
    { title: "Avg Placement", key: "averagePlacement" },
    { title: "Top 3", key: "successfulPlacementRate" },
    { title: "Top 1", key: "winRate" }
];

defineProps<{
    itemStats: ItemStats[];
}>();

</script>