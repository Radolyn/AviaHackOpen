<template>
  <div class="card">
    <header class="card-header">
      <p class="card-header-title">{{name}}</p>
      <button class="card-header-icon focus:outline-none" @click="edit = !edit">
        <span class="icon">
          <i aria-hidden="true" class="mdi" :class="edit ? 'mdi-send' : 'mdi-pencil'"></i>
        </span>
      </button>
    </header>
    <div class="card-content">
      <div class="content">
        <b-input v-if="vmask !== '.'" v-mask="vmask" rounded :value="value" :disabled="!edit" />
        <b-input v-if="vmask === '.'" rounded :value="value" :disabled="!edit" />
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "Document",
  props: ['name', 'vmask', 'value'],
  data() {
    return {
      edit: false
    }
  }
}
</script>

<style scoped>

</style>
