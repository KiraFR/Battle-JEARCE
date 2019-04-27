<template>
  <b-container>
    <b-row align-h="center" id="size">
      <b-col lg="auto" cols="auto" offset-lg="1" align-self="center">
        <router-link to="/Battle-Jearce/Accueil" class="sizelien lien"><b-img :src="require('../../assets/logo_BATTLE_JEARCE.png')"></b-img></router-link>
      </b-col>
      <b-col lg="auto" cols="auto" offset-lg="1" align-self="center" v-show=!$parent.$parent.connected>
        <b-nav>
          <b-nav-item><router-link to="/Battle-Jearce/Inscription" class="lien sizelien orange">S'inscrire</router-link></b-nav-item>
          <b-nav-item><router-link to="/Battle-Jearce/Connexion" class="lien sizelien rouge ">Se connecter</router-link></b-nav-item>
        </b-nav>
      </b-col>
      <b-col lg="auto" cols="auto" offset-lg="3" v-show=$parent.$parent.connected>
          <b-nav >
            <b-nav-item v-on:click=setdisconnected><router-link to="" class="lien sizelien orange">Se deconnecter</router-link></b-nav-item>
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
  @media screen and (max-width: 767px) {
    img {
      width:60vw;
    }
    .sizelien {
      font-size: 10px;
    }
  }
  @media (min-width: 768px) {
    img {
      height: 10vh;
    }
    .sizelien {
      font-size: 17px;
    }
  }
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



    .back {
      /*background-color: #393333;*/
    }

    .lien {
      font-family: "Scream";
      text-decoration: none;
      color: black;
    }

    #size {
      /*background-color: #393333;*/
      padding-top: 3vh;
    }
</style>
