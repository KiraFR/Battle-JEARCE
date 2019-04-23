<template>
  <b-container>
    <b-row align-v="center">
      <b-nav vertical justified>
        <b-nav-item v-show=$parent.$parent.connected><router-link to="/Profil/Stats" class="lien">Profil</router-link></b-nav-item>
        <b-nav-item v-show=$parent.$parent.connected><router-link to="/Formation/Liste" class="lien">Formation</router-link></b-nav-item>
        <b-nav-item><router-link to="/Battle-Jearce/Accueil" class="lien">Accueil</router-link></b-nav-item>
        <!--<b-nav-item><router-link to="/Boutique/BoutiquePersonnages" class="lien">Boutique</router-link></b-nav-item>-->
        <b-nav-item><router-link to="/Battle-Jearce/Classement" class="lien">Classement</router-link></b-nav-item>
        <b-nav-item><router-link to="/Battle-Jearce/Reglement" class="lien">Reglement</router-link></b-nav-item>
        <b-nav-item><router-link to="/Battle-Jearce/Contact" class="lien">Nous contacter</router-link></b-nav-item>
      </b-nav>
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
</style>
