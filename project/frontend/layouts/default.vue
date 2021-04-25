<template>
  <div>
    <nav
      class="navbar header has-shadow is-primary"
      role="navigation"
      aria-label="main navigation"
    >

      <div style="width : 150px; position : relative; margin-left : auto; top: 7px; ">
        <b-dropdown position="is-bottom-left" aria-role="list">
          <template #trigger="{ active }">
            <b-button
              position="is-top-right"
              label="Меню"
              type="is-primary"
              icon-left="home"
              :icon-right="active ? 'menu-up' : 'menu-down'"/>
          </template>

          <nuxt-link to="pnr_reg">
            <b-dropdown-item aria-role="listitem">
              Авторизация
            </b-dropdown-item>
          </nuxt-link>
          <nuxt-link to="services">
            <b-dropdown-item aria-role="listitem">
              Услуги
            </b-dropdown-item>
          </nuxt-link>
          <nuxt-link to="account">
            <b-dropdown-item>
              Личный кабинет
            </b-dropdown-item>
          </nuxt-link>
        </b-dropdown>
      </div>
    </nav>

    <section class="main-content columns">
      <div class="container column is-10 min-h-screen">
        <nuxt/>
      </div>

    </section>
    <div>
      <footer class="is-primary footer"
              style="background : #7289DA !important;">
        <div class="grid grid-cols-7">
          <div class="col-start-2">
            <b-menu>
              <b-menu-list label = "Сайт">
                <b-menu-item icon="apps" label="Перейти">
                  <nuxt-link to = "pnr_reg" class = "custom_dropdown">
                    <b-menu-item label="Авторизация" class ="focus:outline-none"></b-menu-item>
                  </nuxt-link>
                  <nuxt-link to = "services" class = "custom_dropdown">
                    <b-menu-item label="Услуги"></b-menu-item>
                  </nuxt-link>
                  <nuxt-link to = "account" class = "custom_dropdown">
                    <b-menu-item label="Личный кабинет"></b-menu-item>
                  </nuxt-link>
                </b-menu-item>
              </b-menu-list>
            </b-menu>

          </div>

          <div class="col-start-7">

            <b-menu>
              <b-menu-list label = "Команда">
                <b-menu-item icon="link" label="Ссылки">
                  <b-menu-item label="Сайт" onClick="location.href = 'https://radolyn.com/'"></b-menu-item>
                  <b-menu-item label="Github" onClick = "location.href = 'https://github.com/Radolyn'"></b-menu-item>
                </b-menu-item>
              </b-menu-list>
            </b-menu>

          </div>
        </div>


<!--        <div class="content has-text-centered is-primary">-->
<!--          <p>-->
<!--            <strong>Document Recycling</strong> by <a href="https://radolyn.com/" style="color : #363636"><strong>Radolyn</strong></a>-->
<!--          </p>-->
<!--        </div>-->

        <center><br><img src="~../assets/Rd_x64.png"/></center>

      </footer>
    </div>
  </div>
</template>

<style scoped>

.custom_dropdown {
  padding : 3px !important;
  color : white !important
}

* {
  overflow: visible;
}

*:focus {
  outline: none;
}

p, span {
  color : white !important
}

* {
  text-decoration: none;
}

</style>

<script>

export default {
  data() {
    return {
      isActive : true
    }
  }
}

</script>
