<template>
  <div class="card">
    <div class="card-content">
      <div class="content">
        <p>{{description}}</p>
        <b-button type="is-primary" expanded>Подробнее</b-button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "DocTemplate",
  props: ['description']
}
</script>

<style scoped>

</style>
