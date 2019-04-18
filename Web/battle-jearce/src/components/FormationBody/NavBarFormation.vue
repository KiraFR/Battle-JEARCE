<template>
  <b-container>
    <b-row>
      <b-col lg="5" offset-lg="1">
        <b-nav justified tabs>
          <b-nav-item><router-link to="/Formation/Liste">Liste</router-link></b-nav-item>
          <b-nav-item><router-link to="/Formation/New" class="lien">Création</router-link></b-nav-item>
        </b-nav>
      </b-col>
    </b-row>
  </b-container>
</template>

<script>
  export default {
    created() {
      var router = this.$router;
      this.axios({
        url: "http://localhost:5000/GetSession",
        method: "get",
        useCredentails: true
      }).then(function (response) {
        console.log(response.data);
        if (response.data == "la variable session est vide") {
          router.push('/Validation');
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
