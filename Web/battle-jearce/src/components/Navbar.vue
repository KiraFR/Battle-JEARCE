<template>
  <b-container>
    <b-row align-v="center">
      <b-nav vertical justified>
        <b-nav-item><router-link to="/Battle-Jearce/Profil"class="lien">Profil</router-link></b-nav-item>
        <b-nav-item><router-link to="/Battle-Jearce/Boutique"class="lien">Boutique</router-link></b-nav-item>
        <b-nav-item><router-link to="/Battle-Jearce/Classement"class="lien">Classement</router-link></b-nav-item>
        <b-nav-item><router-link to="/Battle-Jearce/Accueil"class="lien">accueil</router-link></b-nav-item>
        <b-nav-item><router-link to="/Battle-Jearce/Reglement"class="lien">Reglement</router-link></b-nav-item>
        <b-nav-item><router-link to="/Battle-Jearce/Contact"class="lien">Contact</router-link></b-nav-item>
      </b-nav>
    </b-row>
  </b-container>
</template>

<script>

</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  .lien {
    text-decoration: none;
    color: black;
  }
</style>
