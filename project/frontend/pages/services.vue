<template>
  <section class="section">

    <h1 class="title">Услуги в аэропорту</h1>

    <b-field>
      <b-input placeholder="Поиск..."
               v-model=search_req
               type="text"
               icon="magnify"
               icon-clickable
               rounded
               icon-right="close-circle"
               icon-right-clickable
               @icon-click="search"
               @icon-right-click="clearIconClick"
      >
      </b-input>
    </b-field>


        <b-menu>
          <b-menu-list>
            <b-menu-item icon="view-dashboard" :label="service.name" v-for="service in services" :key="service.id">
              <service-menu-item v-for="template in service.templates" :name="template.name" :preview="'https://rd-api.loca.lt/template/' + template.id + '/preview?secret=1233212313231'" :id="template.id" :key="template.id" />
            </b-menu-item>
          </b-menu-list>
        </b-menu>

  </section>
</template>

<script>

import VueDocPreview from 'vue-doc-preview';
import ServiceMenuItem from "~/components/ServiceMenuItem";

export default {
  data() {
    return {
      search_req: '',
      services: []
    }
  },
  components: {
    ServiceMenuItem,
    VueDocPreview
  },
  methods: {
    clearIconClick() {
      this.search_req = '';
    },
    async search() {
      // todo: real search
      if (this.search_req.startsWith('дог'))
        this.services = [{id: 1, name: 'VIP', templates:[{name: 'Договор на обслуживание в бизнес зале', id: 6}]}]
      else
        this.services = await this.$axios.$get('https://rd-api.loca.lt/service')
    },
    async fetch_services() {
      this.services = await this.$axios.$get('https://rd-api.loca.lt/service')
    }
  },
  mounted() {
    this.fetch_services()
  }

}

</script>
