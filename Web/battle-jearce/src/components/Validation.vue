<template>
  <div></div>
</template>

<script>
  export default {
    created() {
      var router = this.$router;
      setTimeout(function () {
        router.push('/Battle-Jearce/Accueil');
      }, 5);
    }
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
