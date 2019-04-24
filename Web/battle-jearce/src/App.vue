<template >
  <div id="app">
    <router-view />
  </div>
</template>

<script>
  export default {
    name: 'App',
    data() {
      return {
        connected: false
      }
    }
  }
</script>

<style>
  @font-face {
    font-family: "Scream";
    src: url("assets/Scream.ttf");
  }
</style>
