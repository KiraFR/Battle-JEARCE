<template>
  <b-container>
    <b-row  align-h="center">
      <b-col lg="auto" cols="auto">
        <b-nav justified>
          <b-nav-item><router-link to="/Formation/Liste" class="lien sizeNav orange">Liste</router-link></b-nav-item>
          <b-nav-item><router-link to="/Formation/New" class="lien sizeNav rouge">Création</router-link></b-nav-item>
        </b-nav>
      </b-col>
    </b-row>
  </b-container>
</template>

<script>

</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  @media screen and (max-width: 767px) {
    .sizeNav {
      font-size: 15px;
    }
  }
  @media (min-width: 768px) {
    .sizeNav {
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
  .lien {
    font-family: "Scream";
    text-decoration: none;
    color: black;
  }
</style>
