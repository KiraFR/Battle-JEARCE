<template>
  <b-container>
    <b-row align-h="center">
      <b-col lg="5">
        <h3 class="titrePartie">Classement Mondial</h3>
      </b-col>
    </b-row>
    <b-row>
      <b-col lg="10" offset-lg="1">
        <b-table striped small :items="ClassementGlobal"></b-table>
      </b-col>
    </b-row>
    <b-row v-show=connected>
      <b-col offset-lg="1" lg="4">
        <h5>Ma Position</h5>
      </b-col>
    </b-row>
    <b-row v-show=connected>
      <b-col lg="10" offset-lg="1">
        <b-table striped small :items="PositionGlobal"></b-table>
      </b-col>
    </b-row>
    <b-row align-h="center" v-show=connected>
      <b-col lg="5">
        <h3 class="titrePartie">Classement Amical</h3>
      </b-col>
    </b-row>
    <b-row v-show=connected>
      <b-col lg="10" offset-lg="1">
        <b-table striped small :items="ClassementAmis"></b-table>
      </b-col>
    </b-row>
    <b-row v-show=connected>
      <b-col offset-lg="1" lg="4">
        <h5>Ma Position</h5>
      </b-col>
    </b-row>
    <b-row v-show=connected>
      <b-col lg="10" offset-lg="1">
        <b-table striped small :items="PositionAmis"></b-table>
      </b-col>
    </b-row>
  </b-container>
</template>

<script>
export default {
    data() {
      return {
        ClassementGlobal: [
          { "#": "1", "Pseudo": "Alusat", "Score": "1000" },
          { "#": "2", "Pseudo": "Solymr", "Score": "970" },
          { '#': '3', 'Pseudo': 'Zera-Light', 'Score': '930' },
          { '#': '4', 'Pseudo': 'Itouette', 'Score': '899' },
          { '#': '5', 'Pseudo': 'Kira', 'Score': '850' }
        ],
        PositionGlobal: [
          { '#': '6', 'Pseudo': 'Valzaris', 'Score': '666' }
        ],
        ClassementAmis: [
          { "#": "1", "Pseudo": "Alusat", "Score": "1000" },
          { "#": "2", "Pseudo": "Solymr", "Score": "970" },
          { '#': '3', 'Pseudo': 'Zera-Light', 'Score': '930' },
          { '#': '4', 'Pseudo': 'Itouette', 'Score': '899' },
          { '#': '5', 'Pseudo': 'Kira', 'Score': '850' }
        ],
        PositionAmis: [
          { '#': '6', 'Pseudo': 'Valzaris', 'Score': '666' }
        ],
        connected: false
      }
    },
    created() {
      var self = this;
      this.axios({
        url: "http://localhost:5000/GetSession",
        method: "get",
        useCredentails: true
      }).then(function (response) {
        console.log(response.data);
        if (response.data != "la variable session est vide") {
          self.connected = true;
        } else {
          self.connected = false;
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
