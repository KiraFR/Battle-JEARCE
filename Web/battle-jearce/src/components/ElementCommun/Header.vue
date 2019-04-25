<template>
  <b-container>
    <b-row id="size">
      <b-col lg="4" offset-lg="1" align-self="center">
        <router-link to="/Battle-Jearce/Accueil" class="lien"><b-img :src="require('../../assets/logo_BATTLE_JEARCE.png')"></b-img></router-link>
      </b-col>
      <b-col lg="6" offset-lg="1" align-self="center" v-show=!$parent.$parent.connected>
        <b-nav>
          <b-nav-item><router-link to="/Battle-Jearce/Inscription" class="lien orange">S'inscrire</router-link></b-nav-item>
          <b-nav-item><router-link to="/Battle-Jearce/Connexion" class="lien rouge ">Se connecter</router-link></b-nav-item>
        </b-nav>
      </b-col>
      <b-col lg="4" offset-lg="3" align-self="center"v-show=$parent.$parent.connected>
          <b-nav >
            <b-nav-item v-on:click=setdisconnected><router-link to="" class="lien orange">Se deconnecter</router-link></b-nav-item>
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
  img{
    height:10vh;
  }
  .back {
    /*background-color: #393333;*/
  }
  .lien {
    font-size: 17px;
    font-family: "Scream";
    text-decoration: none;
    color: black;
  }
  #size {
    /*background-color: #393333;*/
    padding-top: 3vh;
  }
</style>
