<template>
  <b-container>
    <b-row>
      <router-view />
    </b-row>
    <b-row class="toogleZone">
      <h1 v-on:click=toogle id="couleur">{{OpenClose}}</h1>
    </b-row>
    <b-row >
      <b-col v-show=boolOpenClose class="nav" lg=2>
        <router-view name="navBar" />
      </b-col>
      <b-col offset-lg="2" lg="8">
        <router-view name="body" />
      </b-col>
    </b-row>
  </b-container>
</template>

<script>
  export default {
    data() {
      return {
        OpenClose: ">",
        boolOpenClose: false
      }
    },
    methods:{
      toogle() {
        this.boolOpenClose = !this.boolOpenClose;
        if (this.OpenClose == ">") {
          this.OpenClose = "<";
        } else{
          this.OpenClose = ">";
        }
      }
    }
  }

</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  #couleur {
    color: #FFD700;
  }
  #couleur:hover {
    color: #B22222;
  }
  .toogleZone {
    z-index:10000;
    background-color: #393333
  }
  .nav {
    position:absolute;
    height: 80%;
    background-color: #393333;
  }
</style>
