<template>
  <div class="chart-container">
    <Bar
      :data="chartData"
      :options="chartOptions"
    />
  </div>
</template>

<script setup lang="ts">
import {
  Chart as ChartJS,
  BarElement,
  CategoryScale,
  LinearScale,
  Tooltip,
  Legend
} from "chart.js";

import { computed } from "vue";
import { Bar } from "vue-chartjs";

ChartJS.register(
  BarElement,
  CategoryScale,
  LinearScale,
  Tooltip,
  Legend
);

interface PlacementDistribution {
  placement: number;
  games: number;
}

const props = defineProps<{
  data: PlacementDistribution[];
}>();

const chartData = computed(() => ({
  labels: props.data.map(x => `${x.placement}${x.placement === 1 ? "st" : x.placement === 2 ? "nd" : x.placement === 3 ? "rd" : "th"}`),
  datasets: [
    {
      label: "Games",
      data: props.data.map(x => x.games),
      backgroundColor: "#1976D2",
      borderRadius: 6
    }
  ]
}));

const chartOptions = {
  responsive: true,
  maintainAspectRatio: false,

  plugins: {
    legend: {
      display: false
    }
  },

  scales: {
    y: {
      beginAtZero: true
    }
  }
};
</script>

<style scoped>
.chart-container {
  height: 350px;
}
</style>