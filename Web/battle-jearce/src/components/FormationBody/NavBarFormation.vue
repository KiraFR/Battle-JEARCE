<template>
  <b-container>
    <b-row>
      <b-col lg="10" offset-lg="1">
        <b-nav justified>
          <b-nav-item><router-link to="/Formation/Liste" class="lien orange">Liste</router-link></b-nav-item>
          <b-nav-item><router-link to="/Formation/New" class="lien rouge">Création</router-link></b-nav-item>
        </b-nav>
      </b-col>
    </b-row>
  </b-container>
</template>

<script>

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
  .lien {
    font-size: 17px;
    font-family: "Scream";
    text-decoration: none;
    color: black;
  }
</style>
