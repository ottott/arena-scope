<template>
  <div>

    <!-- Placement Distribution -->
    <v-card class="pa-6 mb-6">
      <h2 class="text-h5 mb-4">
        Placement Distribution
      </h2>

      <PlacementChart :data="placementDistribution" />
    </v-card>


    <!-- Two column stats row -->
    <v-row>

      <!-- Combat -->
      <v-col cols="12" md="6">

        <v-card class="pa-6 mb-6">
          <h2 class="text-h5 mb-4">
            Combat Performance
          </h2>

          <p>
            <v-row>

              <v-col cols="6">
                <v-sheet class="pa-4 text-center rounded">
                  <div class="text-caption text-medium-emphasis">
                    Avg Kills
                  </div>

                  <div class="text-h5 font-weight-bold">
                    {{ performanceStats.averageKills.toFixed(1) }}
                  </div>
                </v-sheet>
              </v-col>


              <v-col cols="6">
                <v-sheet class="pa-4 text-center rounded">
                  <div class="text-caption text-medium-emphasis">
                    Avg Deaths
                  </div>

                  <div class="text-h5 font-weight-bold">
                    {{ performanceStats.averageDeaths.toFixed(1) }}
                  </div>
                </v-sheet>
              </v-col>


              <v-col cols="6">
                <v-sheet class="pa-4 text-center rounded">
                  <div class="text-caption text-medium-emphasis">
                    Avg Assists
                  </div>

                  <div class="text-h5 font-weight-bold">
                    {{ performanceStats.averageAssists.toFixed(1) }}
                  </div>
                </v-sheet>
              </v-col>


              <v-col cols="6">
                <v-sheet class="pa-4 text-center rounded">
                  <div class="text-caption text-medium-emphasis">
                    KDA
                  </div>

                  <div class="text-h5 font-weight-bold">
                    {{ performanceStats.averageKda.toFixed(1) }}
                  </div>
                </v-sheet>
              </v-col>

            </v-row>
          </p>
        </v-card>

      </v-col>


      <!-- Damage -->
      <v-col cols="12" md="6">

        <v-card class="pa-6 mb-6">
          <h2 class="text-h5 mb-4">
            Damage Performance
          </h2>

          <p>
            <v-row>

              <v-col cols="6">
                <v-sheet class="pa-4 text-center rounded">
                  <div class="text-caption text-medium-emphasis">
                    Avg Damage Dealt
                  </div>

                  <div class="text-h5 font-weight-bold">
                    {{ Math.round(performanceStats.averageDamageDealt).toLocaleString() }}
                  </div>
                </v-sheet>
              </v-col>


              <v-col cols="6">
                <v-sheet class="pa-4 text-center rounded">
                  <div class="text-caption text-medium-emphasis">
                    Avg Damage Taken
                  </div>

                  <div class="text-h5 font-weight-bold">
                    {{ Math.round(performanceStats.averageDamageTaken).toLocaleString() }}
                  </div>
                </v-sheet>
              </v-col>


              <v-col cols="6">
                <v-sheet class="pa-4 text-center rounded">
                  <div class="text-caption text-medium-emphasis">
                    Avg Healing
                  </div>

                  <div class="text-h5 font-weight-bold">
                    {{ Math.round(performanceStats.averageHealing).toLocaleString() }}
                  </div>
                </v-sheet>
              </v-col>


              <v-col cols="6">
                <v-sheet class="pa-4 text-center rounded">
                  <div class="text-caption text-medium-emphasis">
                    Avg Shielding
                  </div>

                  <div class="text-h5 font-weight-bold">
                    {{ Math.round(performanceStats.averageShielding).toLocaleString() }}
                  </div>
                </v-sheet>
              </v-col>

            </v-row>
          </p>
        </v-card>

      </v-col>

    </v-row>


    <!-- Two column tables row -->
    <v-row>

      <!-- Teammates -->
      <v-col cols="12" md="6">

        <v-card class="pa-6 mb-6">

          <h2 class="text-h5 mb-4">
            Best Teammates
          </h2>

          <v-table>
            <thead>
              <tr>
                <th>Player</th>
                <th>Games</th>
                <th>Avg Placement</th>
                <th>Top 3</th>
                <th>Top 1</th>
              </tr>
            </thead>

            <tbody>

              <tr v-for="duo in duoStats.slice(0, 5)" :key="duo.gameName + duo.tagLine">
                <td>{{ duo.gameName }}</td>

                <td>{{ duo.games }}</td>

                <td>{{ duo.averagePlacement.toFixed(2) }}</td>

                <td>{{ duo.successfulPlacementRate.toFixed(1) }}%</td>

                <td>{{ duo.winRate.toFixed(1) }}%</td>
              </tr>

            </tbody>

          </v-table>

        </v-card>

      </v-col>

      <!-- Champion synergy -->
      <v-col cols="12" md="6">

        <v-card class="pa-6 mb-6">

          <h2 class="text-h5 mb-4">
            Best Team Champions
          </h2>

          <v-table>
            <thead>
              <tr>
                <th>Champion</th>
                <th>Games</th>
                <th>Avg Placement</th>
                <th>Top 3</th>
                <th>Top 1</th>
              </tr>
            </thead>

            <tbody>

              <tr v-for="champion in teamChampionStats.slice(0, 5)" :key="champion.championName">
                <td>{{ champion.championName }}</td>

                <td>{{ champion.games }}</td>

                <td>{{ champion.averagePlacement.toFixed(2) }}</td>

                <td>{{ champion.successfulPlacementRate.toFixed(1) }}%</td>

                <td>{{ champion.winRate.toFixed(1) }}%</td>
              </tr>

            </tbody>

          </v-table>

        </v-card>

      </v-col>

    </v-row>

    <!-- Champion performance -->
    <v-card class="pa-6">

      <h2 class="text-h5 mb-4">
        Champion Performance
      </h2>

      <v-data-table :headers="championHeaders" :items="championStats" items-per-page="10">

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


  </div>
</template>

<script setup lang="ts">
import PlacementChart from "./PlacementChart.vue";

interface PlacementDistribution {
  placement: number;
  games: number;
}

interface DuoStats {
  gameName: string;
  tagLine: string;
  games: number;
  wins: number;
  winRate: number;
  successfulPlacements: number;
  successfulPlacementRate: number;
  averagePlacement: number;
}

interface TeamChampionStats {
  championName: string;
  games: number;
  wins: number;
  winRate: number;
  successfulPlacements: number;
  successfulPlacementRate: number;
  averagePlacement: number;
}

interface ChampionStats {
  championName: string;
  games: number;
  wins: number;
  winRate: number;
  successfulPlacements: number;
  successfulPlacementRate: number;
  averagePlacement: number;
}

interface PerformanceStats {
  averageKills: number;
  averageDeaths: number;
  averageAssists: number;
  averageKda: number;

  averageDamageDealt: number;
  averageDamageTaken: number;
  averageHealing: number;
  averageShielding: number;
}

const championHeaders = [
  { title: "Champion", key: "championName" },
  { title: "Games", key: "games" },
  { title: "Avg Placement", key: "averagePlacement" },
  { title: "Top 3", key: "successfulPlacementRate" },
  { title: "Top 1", key: "winRate" }
];

defineProps<{
  placementDistribution: PlacementDistribution[];
  performanceStats: PerformanceStats;
  duoStats: DuoStats[];
  teamChampionStats: TeamChampionStats[];
  championStats: ChampionStats[];
}>();

</script>