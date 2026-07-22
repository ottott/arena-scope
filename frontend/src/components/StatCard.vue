<template>
  <v-card
    elevation="2"
    rounded="lg"
    class="pa-4 text-center fill-height"
  >
    <div class="text-h4 font-weight-bold">
      {{ value }}
    </div>

    <div class="text-medium-emphasis mt-2">
      {{ title }}
    </div>
  </v-card>
</template>

<script setup lang="ts">
defineProps<{
  title: string;
  value: string | number;
}>();
</script>