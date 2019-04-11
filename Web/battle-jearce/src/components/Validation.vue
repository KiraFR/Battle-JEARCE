<template>
  <b-container>
        <b-row class="espacement">
          <b-col lg="4" offset-lg="4">
            <h3>Inscription réussite</h3>
          </b-col>
        </b-row>
        <b-row class="espacement">
          <b-col lg="3" offset-lg="2">
            <router-link to="/Battle-Jearce/Accueil" class="lien">
              Retourner a l'accueil
            </router-link>
          </b-col>
          <b-col lg="3" offset-lg="2">
            <router-link to="/Battle-Jearce/Connexion" class="lien">
              Aller ce connecter
            </router-link>
          </b-col>
        </b-row>
  </b-container>
</template>

<script>
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  .espacement{
    margin-top:20vh;
  }
</style>
