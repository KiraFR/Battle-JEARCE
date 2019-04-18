<template>
  <b-container>
    <b-row>
      <router-view />
    </b-row>
    <b-row>
      <b-col lg="2">
        <router-view name="navBar" />
      </b-col>
      <b-col offset-lg="1" lg="8">
        <b-row align-h="center">
          <b-col lg="5">
            <h3 class="titrePartie">Formation</h3>
          </b-col>
        </b-row>
        <b-row>
          <b-col lg="12">
            <router-view name="navBarF" />
          </b-col>
        </b-row>
        <b-row>
          <b-col lg="12">
            <router-view name="body" />
          </b-col>
        </b-row>
      </b-col>
    </b-row>
  </b-container>
</template>

<script>
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
