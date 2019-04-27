<template>
  <b-container>
    <b-row align-h="center">
      <b-col cols="auto" lg="auto">
        <h4  class=" sizetitre titreStyle">Mon Profil</h4>
      </b-col>
    </b-row>
    <b-row align-h="center">
      <b-col lg="auto" cols="auto" class="titreStyle sizeText rouge">
        email :
      </b-col>
      <b-col lg="auto" cols="auto" class="titreStyle sizeText orange">
        {{email}}
      </b-col>
    </b-row>
    <b-row align-h="center">
      <b-col lg="auto" cols="auto" class="titreStyle sizeText rouge">
        pseudo :
      </b-col>
      <b-col lg="auto" cols="auto" class="titreStyle sizeText orange">
        {{pseudo}}
      </b-col>
    </b-row>
    <b-row align-h="center">
      <b-col lg="auto" cols="auto" class="titreStyle sizeText rouge">
        argent :
      </b-col>
      <b-col lg="auto" cols="auto" class="titreStyle sizeText orange">
        {{argent}}
      </b-col>
    </b-row>
    <b-row align-h="center">
      <b-col lg="auto" cols="auto" class="titreStyle sizeText rouge">
        score :
      </b-col>
      <b-col lg="auto" cols="auto" class="titreStyle sizeText orange">
        {{score}}
      </b-col>
    </b-row>
  </b-container>
</template>

<script>
  export default {
    data() {
      return {
        pseudo: "",
        email: "",
        argent: "",
        score:""

      }
    },
    created() {
      var self = this;
      var router = this.$router;
      this.axios({
        url: "http://localhost:5000/GetSession",
        method: "get",
        useCredentails: true
      }).then(function (response) {
        console.log(response.data);
        if (response.data != "la variable session est vide") {
          self.pseudo = response.data.pseudo;
          self.email = response.data.email;
          self.argent = response.data.argent;
          self.score = response.data.score;
        } else {
          self.$parent.$parent.connected = false;
          router.push('/Battle-Jearce/Accueil');
        }
      }).catch(function (error) {
        console.log(error);
      });
    }
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  @media screen and (max-width: 767px) {
    .sizetitre{
      font-size: 20px;
    }
    .sizeText {
      font-size: 10px;
    }
  }
  .rouge {
    -webkit-text-stroke: 0.5px #B22222;
  }

  .orange {
    -webkit-text-stroke: 0.5px #FFD700;
  }

</style>
