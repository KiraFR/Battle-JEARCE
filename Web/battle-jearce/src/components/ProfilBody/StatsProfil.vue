<template>
  <b-container>
  </b-container>
</template>

<script>
  export default {
    data() {
      return {
        listeFormation:[]
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
        if (response.data) {
          self.listeFormation = response.data.formation;
        } else {
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
