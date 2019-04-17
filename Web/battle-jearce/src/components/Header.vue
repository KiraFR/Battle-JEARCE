<template>
  <b-container id="size">
    <b-row>
      <b-col lg="4" offset-lg="1">
        <router-link to="/Battle-Jearce/Accueil" class="lien"><h1>Battle-JEARCE</h1></router-link>
      </b-col>
      <b-col lg="3" offset-lg="4" align-self="center">
          <b-nav v-if=!getConnected()>
              <b-nav-item><router-link to="/Battle-Jearce/Inscription" class="lien">S'inscrire</router-link></b-nav-item>
              <b-nav-item><router-link to="/Battle-Jearce/Connexion" class="lien">Se connecter</router-link></b-nav-item>
          </b-nav>
          <b-nav v-if=getConnected()>
            <b-nav-item><router-link to="" v-on:click=setdisconnected() class="lien">Se deconnecter</router-link></b-nav-item>
          </b-nav>
      </b-col>
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
    methods: {
      getConnected() {
        this.axios({
          url: "http://localhost:5000/GetSession",
          method: "get",
          useCredentails: true
        }).then(function (response) {
          console.log(response);
          if (response.data != "la variable session est vide") {
            return true;
          } else {
            return true;
          }

          }).catch(function (error) {
            console.log(error.data);
            return false;
        });
      },
      setdisconnected() {
        this.axios({
          url: "http://localhost:5000/DeleteSession",
          method: "delete",
          useCredentails: true
        }).then(function (response) {
            this.connected = false;
          }).catch(function (error) {
            alert(error.response);
        });
      }
    }
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
