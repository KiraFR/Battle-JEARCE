<template>
  <b-container >
    <b-row id="size">
      <b-col lg="4" offset-lg="1" align-self="center">
        <router-link to="/Battle-Jearce/Accueil" class="lien"><h1>Battle-JEARCE</h1></router-link>
      </b-col>
      <b-col lg="3" offset-lg="4" align-self="center">
          <b-nav v-show=!$parent.$parent.connected>
              <b-nav-item><router-link to="/Battle-Jearce/Inscription" class="lien">S'inscrire</router-link></b-nav-item>
              <b-nav-item><router-link to="/Battle-Jearce/Connexion" class="lien">Se connecter</router-link></b-nav-item>
          </b-nav>
          <b-nav v-show=$parent.$parent.connected>
            <b-nav-item v-on:click=setdisconnected><router-link to=""  class="lien">Se deconnecter</router-link></b-nav-item>
          </b-nav>
      </b-col>
    </b-row>
   </b-container>
</template>

<script>
  export default {
    methods: {
      setdisconnected() {
        var self = this;
        var router = this.$router;
        this.axios({
          url: "http://localhost:5000/DeleteSession",
          method: "post",
          useCredentails: true
        }).then(function (response) {
          self.$parent.$parent.connected = false;
          router.push('/Battle-Jearce/Accueil');
        }).catch(function (error) {
          alert("pass err")
        });
      }
    }
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  template{
    
  }
  .lien {
    text-decoration: none;
    color: white;
  }
  #size {
    background-color: black;
    padding-top: 5vh;
    padding-bottom: 3vh;
  }
</style>
