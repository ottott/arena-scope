<template>
    <div class="stat-card">

        <div class="title">
            {{ title }}
        </div>

        <div class="value">
            {{ value }}
        </div>

    </div>
</template>

<script setup lang="ts">

defineProps<{

    title: string;

    value: string | number;

}>();

</script>

<style scoped>

.stat-card {

    border: 1px solid #cccccc;

    border-radius: 8px;

    padding: 20px;

    width: 180px;

    text-align: center;

}

.title {

    font-size: 14px;

    color: gray;

    margin-bottom: 12px;

}

.value {

    font-size: 32px;

    font-weight: bold;

}

</style>