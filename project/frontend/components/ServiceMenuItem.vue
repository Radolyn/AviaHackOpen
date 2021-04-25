<template>
  <div>
    <b-menu-item icon="apps" :label="name" @click="isCardModalActive = true"></b-menu-item>
    <b-modal v-model="isCardModalActive" :width="1000" scroll="clip" can-cancel>
      <div class="card overflow-x-hidden">
        <p class="card-header-title">
          {{name}}
        </p>
        <div class="card-content flex flex-col overflow-x-hidden justify-center">
          <VueDocPreview class = "overflow-x-hidden" value="https://cdn.discordapp.com/attachments/693826284953796718/835797717221244948/preview_4.docx" type="office" /><br/>
          <div class="pb-10 flex justify-center w-full">
            <drawing v-if="!buttonActive" height="128" brush-size="2" @updated="updateImage"/>
          </div>
          <b-button class="button-animate" type = "is-success" position = "is-right" outlined style = "position : relative;" @click="sign()" :loading="buttonActive">Подписать</b-button>
        </div>
      </div>
    </b-modal>
  </div>
</template>

<script>
import VueDocPreview from 'vue-doc-preview';
import Drawing from "~/components/Drawing";
import { ToastProgrammatic as Toast } from 'buefy'

export default {
  name: "ServiceMenuItem",
  props: ['id', 'name', 'preview'],
  data() {
    return {
      signImage: null,
      buttonActive: false,
      isCardModalActive: false
    }
  },
  components: {Drawing, VueDocPreview},
  methods: {
    async sign() {
      this.buttonActive = true;

      let res = await this.$axios.$post('https://rd-api.loca.lt/template/' + this.id, {

      }, {
        headers: {'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9zaWQiOiIxIiwibmJmIjoxNjE5MzE2MjMzLCJleHAiOjE2MTk0MDI2MzMsImlzcyI6IlJhZG9seW4iLCJhdWQiOiJSYWRvbHluIn0.svi8YfYR4wX2j6vUPEC3vNTukKQE0TD-UmiqPuVaKhk'},
        progress: true
      })

      Toast.open('Документ подписан! Копия отправлена вам на почту.')

      this.isCardModalActive = false;
      this.buttonActive = false;
    }
  }
}
</script>

<style scoped>

</style>
