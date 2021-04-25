<template>
  <section class="section">

    <div class="py-5 text-center">
      <b-button disabled type="is-primary is-light">PNR</b-button>

      <nuxt-link to="cellphone_reg">
        <b-button type="is-primary">
          Телефон
        </b-button>
      </nuxt-link>
    </div>

    <center><span><strong>PNR</strong> - запись в базе данных компьютерной системы бронирования о маршруте пассажира, находящаяся на посадочном талоне</span></center><br>

    <div class="box">

      <b-field label="PNR">
        <b-input v-model="formLog.userPnr"
                 required
                 placeholder="8602158775">
        </b-input>
      </b-field>

      <b-button type = "is-primary" @click = "reqRadApi" :loading = "pnr_loading" :disabled = "!userPnrReg">
        Продолжить
      </b-button>
    </div>
  </section>
</template>

<script>

export default {
  name: 'Auth',

  components: {},
  data() {
    return {
      formLog: {
        userCellphone: "",
        userPnr: ""
      },
      token : '',
      pnr_loading : false
    }
  },
  methods: {
    clearEmail() {
      this.formLog.user_email = '';
    },
    async reqRadApi() {
      this.pnr_loading = true;
      this.token = await this.$axios.$post('https://rd-api.loca.lt/user/pnr', {pnr: this.formLog.userPnr});
      await this.$router.push('/account');
    }
  },
  computed: {
    userPnrReg() {
      return this.formLog.userPnr.length > 7;
    }
  }
}

</script>
