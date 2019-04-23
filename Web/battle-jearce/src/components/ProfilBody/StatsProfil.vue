<template>
  <b-container>
    <p>{{pseudo}}</p>
    <p>{{email}}</p>
    <p>{{argent}}</p>
    <p>{{score}}</p>
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
</style>
