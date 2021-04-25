<template>
  <section>
    <br>
    <center>

      <div class="box rounded-3xl" style="max-width: 600px">

        <b-icon
          icon="account"
          type = "is-primary"
          custom-size="mdi-48px">
        </b-icon>

        <div class="column rounded-3xl" v-for="document in documents" :key="document.id">
          <Document :name="document.type.name" :value="document.value" :vmask="document.type.mask"/>
        </div>

      </div>
    </center>
  </section>

</template>

<script>

import Document from "~/components/Document";

export default {
  data() {
    return {
      documents: []
    }
  },
  components: {
    Document
  },
  methods: {
    async fetch_documents() {
      // fetch all documents

      this.documents = await this.$axios.$get('https://rd-api.loca.lt/user/me', {
        headers: {'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9zaWQiOiIxIiwibmJmIjoxNjE5MzE2MjMzLCJleHAiOjE2MTk0MDI2MzMsImlzcyI6IlJhZG9seW4iLCJhdWQiOiJSYWRvbHluIn0.svi8YfYR4wX2j6vUPEC3vNTukKQE0TD-UmiqPuVaKhk'},
        progress: true
      })
    }
  },
  mounted() {
    this.fetch_documents()
  }
}

</script>
