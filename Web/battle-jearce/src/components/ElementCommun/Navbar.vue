<template>
  <b-container>
    <b-row align-v="center">
      <b-col>
        <b-nav vertical justified>
          <b-nav-item v-show=$parent.$parent.connected><router-link to="/Profil/Stats" class="lien orange">Profil</router-link></b-nav-item>
          <b-nav-item v-show=$parent.$parent.connected><router-link to="/Formation/Liste" class="lien rouge">Formation</router-link></b-nav-item>
          <b-nav-item><router-link to="/Battle-Jearce/Accueil" class="lien orange">Accueil</router-link></b-nav-item>
          <!--<b-nav-item><router-link to="/Boutique/BoutiquePersonnages" class="lien">Boutique</router-link></b-nav-item>-->
          <b-nav-item><router-link to="/Battle-Jearce/Classement" class="lien rouge">Classement</router-link></b-nav-item>
          <b-nav-item><router-link to="/Battle-Jearce/Reglement" class="lien orange">Reglement</router-link></b-nav-item>
          <b-nav-item><router-link to="/Battle-Jearce/Contact" class="lien rouge">Nous contacter</router-link></b-nav-item>
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

  .rouge {
    -webkit-text-stroke: 0.5px #B22222;
  }

  .orange {
    -webkit-text-stroke: 0.5px #FFD700;
  }

  .rouge:hover {
    -webkit-text-stroke: 0.5px #FFD700;
  }

  .orange:hover {
    -webkit-text-stroke: 0.5px #B22222;
  }

  .lien {
    font-size: 12px;
    font-family: "Scream";
    text-decoration: none;
    color: black;
  }
</style>
