<template>
  <div id="app" class="backfont">
    <router-view />
  </div>
</template>

<script>
  export default {
    name: 'App',
    data() {
      return {
        connected: false
      }
    }
  }
</script>

<style>
  @font-face {
    font-family: "Scream";
    src: url("assets/Scream.ttf");
  }
  * {
    font-family: Verdana, sans-serif;
  }
  .backfont{
    color: white;
    min-height:100vh;
    background-color: black;
    padding-bottom: 20px;
  }
  .blackfont {
    background-color: black;
  }
  .bodyStyle {
    color: black;
    background-color: white;
    padding-top: 5vh;
    padding-bottom: 3vh;
    min-height: 70vh;
    border-radius: 5px 10px 5px 10px;
  }
  .titreStyle {
    -webkit-text-stroke: 0.5px #FFD700;
    font-family: "Scream";
    margin-top: 2vh;
    margin-bottom: 5vh;
  }
  .espacement {
    margin-bottom: 2vh;
  }

</style>
