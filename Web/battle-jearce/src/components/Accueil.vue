<template>
  <b-container>
    <b-row>
      <router-view class="blackfont" />
    </b-row>
    <b-row class="toogleZone">
      <h1 v-on:click=toogle class="blackfont" id="couleur">{{OpenClose}}</h1>
    </b-row>
    <b-row >
      <div v-show=boolOpenClose id="nav">
        <router-view name="navBar" />
      </div>
      <b-col offset-lg="1" lg="10" class="bodyStyle">
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

  #nav {
    opacity: 0.95;
    z-index: 100;
    position: absolute;
    height: 70vh;
    background-color: #894040;
    border-radius: 0px 0px 5px 5px;
  }
</style>
