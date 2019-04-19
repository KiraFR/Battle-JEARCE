<template>
  <b-container>
    <b-row align-v="center">
      <b-nav vertical justified>
        <b-nav-item v-show=connected><router-link to="/Profil/Stats" class="lien">Profil</router-link></b-nav-item>
        <b-nav-item v-show=connected><router-link to="/Formation/Liste" class="lien">Formation</router-link></b-nav-item>
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
    data() {
      return {
        connected: false
      }
    },
    created() {
      var self = this;
      this.axios({
        url: "http://localhost:5000/GetSessionOuverte",
        method: "get",
        useCredentails: true
      }).then(function (response) {
        console.log(response.data);
        self.connected = response.data;
      }).catch(function (error) {
        console.log(error);
      });
    }
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  .lien {
    text-decoration: none;
    color: black;
  }
</style>
