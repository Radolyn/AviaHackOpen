<template>
  <section class="section">
    <h1 class="title">Шаблоны для подписи</h1>
    <div>
      <nuxt-link to="documents">
        <b-button class="button-animate" outlined rounded type="is-primary">← Вернуться к документам</b-button>
      </nuxt-link>
    </div>
    <div class="pt-8 columns">
      <div class="column">
        <DocTemplate description="Заявление о возможности ребёнка выходить во время перемен на улицу" />
      </div>
      <div class="column">
        <DocTemplate description="Заявление об увольнении" />
      </div>
      <div class="column">
        <DocTemplate description="Заявление на добровольную вакцинацию от COVID-19" />
      </div>
    </div>
  </section>
</template>

<script>
import DocTemplate from "~/components/DocTemplate";

export default {
  name: "templates",
  components: {DocTemplate}
}
</script>

<style scoped>

</style>
