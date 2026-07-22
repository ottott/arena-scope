<template>
  <main>
    <h1>Arena Scope</h1>

    <div>
      <label>Game Name</label>
      <input v-model="gameName" />
    </div>

    <div>
      <label>Tag Line</label>
      <input v-model="tagLine" />
    </div>

    <button @click="analyze">
      Analyze
    </button>

    <div
        v-if="stats"
        class="stats-row"
    >

        <StatCard
            title="Games"
            :value="stats.games"
        />

        <StatCard
            title="Average Placement"
            :value="stats.averagePlacement.toFixed(2)"
        />

        <StatCard
            title="Top 3 Rate"
            :value="stats.successfulPlacementRate.toFixed(1) + '%'"
        />

        <StatCard
            title="Top 1 Rate"
            :value="(stats.winRate).toFixed(1) + '%'"
        />

    </div>

  </main>
</template>


<style scoped>

.stats-row {

    display: flex;

    gap: 20px;

    margin-top: 30px;

}

</style>

<script setup lang="ts">
import { ref } from "vue";
import StatCard from "../components/StatCard.vue";
import { getPlayerStats } from "../api/arenaApi";
import type { PlayerStats } from "../types/PlayerStats";


const gameName = ref("");
const tagLine = ref("");
const stats = ref<PlayerStats | null>(null);

async function analyze() {

    try {

        stats.value = await getPlayerStats(
            gameName.value,
            tagLine.value
        );


        console.log(stats.value);

    }
    catch (error) {

        console.error(error);

    }
}

</script>