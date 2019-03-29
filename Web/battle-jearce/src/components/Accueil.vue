<template>
  <b-container>
    <b-row>
      <router-view />
    </b-row>
    <b-row >
      <b-col lg="2">
        <router-view name="navBar" />
      </b-col>
      <b-col offset-lg="1" lg="8">
        <router-view name="body" />
      </b-col>
    </b-row>
  </b-container>
</template>

<script>


</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
