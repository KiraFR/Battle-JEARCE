<template>
<b-container>
</b-container>
</template>

<script>
  export default {
    data() {
      return {
        /* liste de si besoin variable
         * connected: false
         */
      }
    },
    created() {
      var self = this;
      this.axios({
        url: "http://localhost:5000/GetSessionOuverte",
        method: "get",    // changer si besoin
        /*data: {
          //idem que data ci dessus
        },*/
        useCredentails: true
      }).then(function (response) {
        console.log(response);

      }).catch(function (error) {
        console.log(error);
      });
    }
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
