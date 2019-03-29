<template>
  <b-container id="size">
    <b-row>
      <b-col lg="4" offset-lg="1">
        <a href="Accueil.html" class="lien"><h1>Battle-JEARCE</h1></a>
      </b-col>
      <b-col lg="3" offset-lg="4" align-self="center">
          <b-nav>
              <b-nav-item active>S'inscrire</b-nav-item>
              <b-nav-item active>Se connecter</b-nav-item>
          </b-nav>
      </b-col>
    </b-row>
   </b-container>
</template>

<script>
  export default {
   
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  .lien {
    text-decoration: none;
    color: black;
  }
  #size{
    padding-top: 5vh;
    padding-bottom:3vh;
  }
</style>
