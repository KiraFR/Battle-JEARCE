<template>
  <b-container>
    <b-row align-h="center">
      <b-col lg="auto" cols="auto">
        <h5 class="titreStyle size orange">Classement Mondial</h5>
      </b-col>
    </b-row>
    <b-row>
      <b-col lg="10" offset-lg="1">
        <b-table striped small :items="ClassementGlobal"></b-table>
      </b-col>
    </b-row>
    <b-row v-show=connected align-h="center">
      <b-col lg="auto" cols="auto">
        <h5 class="titreStyle size rouge">Ma Position</h5>
      </b-col>
    </b-row>
    <b-row v-show=connected>
      <b-col lg="10" offset-lg="1">
        <b-table striped small :items="PositionGlobal"></b-table>
      </b-col>
    </b-row>
    <!--<b-row align-h="center" v-show=connected>
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
  </b-row>-->
  </b-container>
</template>

<script>
  var json = { Rank: '', Pseudo: '', Score: '' };
  export default {
    data() {
      return {
        ClassementGlobal: [],
        PositionGlobal: [{ Rank: '', Pseudo: '', Score: '' }],
        /*ClassementAmis: [
          { "#": "1", "Pseudo": "Alusat", "Score": "1000" },
          { "#": "2", "Pseudo": "Solymr", "Score": "970" },
          { '#': '3', 'Pseudo': 'Zera-Light', 'Score': '930' },
          { '#': '4', 'Pseudo': 'Itouette', 'Score': '899' },
          { '#': '5', 'Pseudo': 'Kira', 'Score': '850' }
        ],
        PositionAmis: [
          { '#': '6', 'Pseudo': 'Valzaris', 'Score': '666' }
        ],*/
        connected: false
      }
    },
    created() {
      var self = this;
        this.axios({
          url: "http://localhost:5000/GetClassement",
          method: "get",
          useCredentails: true,
        }).then(function (res) {
          
          self.axios({
          url: "http://localhost:5000/GetSession",
          method: "get",
          useCredentails: true
          }).then(function (response) {
            if (response.data != "la variable session est vide") {
              console.log("test");
              for (var j in self.ClassementGlobal) {
                if (self.ClassementGlobal[j].Pseudo == response.data.pseudo) {
                  self.PositionGlobal[0].Rank = self.ClassementGlobal[j].Rank;
                }
              }
              self.connected = true;
              
            self.PositionGlobal[0].Pseudo = response.data.pseudo;
            self.PositionGlobal[0].Score = response.data.score;
          
            console.log("responce session");
          } else {
            self.connected = false;
          
          }   
        }).catch(function (error) {
          console.log(error);
        });
          for (var i in res.data) {
             json.Rank = i;
             json.Rank++;
              json.Pseudo = res.data[i].pseudo;
              json.Score = res.data[i].score;
              self.ClassementGlobal.push(json);
              json = {};
            }
          
      }).catch(function (error) {
        console.log(error);
      });

    }
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  @media screen and (max-width: 767px) {
    .size {
      font-size: 12px;
    }
  }
  .rouge {
    -webkit-text-stroke: 0.5px #B22222;
  }

  .orange {
    -webkit-text-stroke: 0.5px #FFD700;
  }

</style>
