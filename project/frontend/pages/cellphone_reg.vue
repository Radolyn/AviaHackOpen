<template>
  <section class="section">

    <div class="py-5 text-center">
      <nuxt-link to="pnr_reg">
        <b-button type="is-primary">
          PNR
        </b-button>
      </nuxt-link>
      <b-button disabled type="is-primary is-light">Телефон</b-button>
    </div>
    <div class="box">

      <b-field label="Номер телефона" :type="{ 'is-success' : cellPhoneReg, 'is-warning' : addPhoneReg }">
        <b-input
          v-model=formReg.reg_cellphone
          :pattern=this.cellphone_regex
          validation-message="Пожалуйста, введите номер телефона по примеру : '8760987887'"
          placeholder="9625291020"
          required
          maxlength="11"
          type="text">
        </b-input>
      </b-field>
      <b-button type="is-primary" @click="showDigitCodeField" :disabled="!cellPhoneReg">Получить код</b-button>

      <div v-show="this.code_show === true">
        <br>
        <b-field label="Код из смс" :type="{ 'is-success' : digitCodeReg, 'is-warning' : !addDigitCodeReg }">
          <b-input
            v-model="formReg.digit_code"
            required
            placeholder="DIGIT-код"
            maxlength="4"
            validation-message="Пожалуйста, введите код из смс">
          </b-input>
        </b-field>

        <b-button type="is-success" :disabled="!digitCodeReg" :type="{ 'is-success' : digitCodeReg}">Подтвердить
        </b-button>
      </div>

    </div>

  </section>

</template>

<script>

export default {
  name: 'Auth',

  components: {},
  data() {
    return {
      formReg: {
        reg_cellphone: '',
        digit_code: ''
      },
      code_show: false,
      value: '',
      cellphone_regex: "^\\+?[78][-\\(]?\\d{3}\\)?-?\\d{3}-?\\d{2}-?\\d{2}$"
    }
  },
  methods: {
    clearCellPhone() {
      this.formReg.reg_cellphone = '';
    },
    showDigitCodeField() {
      this.code_show = true;
    }
  },
  computed: {
    cellPhoneReg() {
      return this.formReg.reg_cellphone.match(this.cellphone_regex);
    },
    digitCodeReg() {
      let logPart1 = this.formReg.digit_code.length === 4;
      let logPart2 = !isNaN(Number(this.formReg.digit_code));

      return logPart1 && logPart2;
    },
    addPhoneReg() {
      return !this.formReg.reg_cellphone.match(this.cellphone_regex) && this.formReg.reg_cellphone !== '';
    }
  }
}

</script>
