<template>
    <div id="app" class="container-fluid">
        <div class="row">
            <div class="col-md-2">
                <nav-menu params="route: route"></nav-menu>
            </div>
            <div class="col-md-10">
                <router-view></router-view>
            </div>
        </div>
    </div>
</template>

<script>
    import NavMenu from './nav-menu'

    export default {
      components: {
        'nav-menu': NavMenu
      },

      data () {
        return {}
      }
    }
</script>

<style>
</style>
