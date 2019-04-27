<template>
  <b-container>
    <b-row align-h="center">
      <b-col lg="auto" cols="auto">
        <b-table hover small class="sizeTab" :items="TabFormation"></b-table>
      </b-col>
    </b-row>
  </b-container>
</template>

<script>
  export default {
    data() {
      return {
        TabFormation: [],
        ListeFormation: [], 
        ListeCharacters: [],
        getCharacterName(namePersonnage) {
          for (var j = 0; j < this.ListeCharacters.length; j++) {
            if (namePersonnage == this.ListeCharacters[j]._id) {
              return this.ListeCharacters[j].type;
            }
          }
        }
      }
    },
    created() {
      var self = this;
      var router = this.$router;
      this.axios({
        url: "http://localhost:5000/GetSession",
        method: "get",
        useCredentails: true
      }).then(function (resSession) {
        console.log(resSession.data);
        if (resSession.data == "la variable session est vide") {
          self.$parent.$parent.connected = false;
          router.push('/Battle-Jearce/Accueil');
        }
        self.ListeFormation = resSession.data.formation;
        self.axios({
          url: "http://localhost:5000/GetCharacter",
          method: "get",
          useCredentails: true
        }).then(function (resCharacter) {
          self.ListeCharacters = resCharacter.data;
          for (var i = 0; i < self.ListeFormation.length; i++) {
            var formationIncr = self.ListeFormation[i];
            self.TabFormation.push({
              nbFormation: i+1,
              personnage1: self.getCharacterName(formationIncr.p1),
              personnage2: self.getCharacterName(formationIncr.p2),
              personnage3: self.getCharacterName(formationIncr.p3),
              personnage4: self.getCharacterName(formationIncr.p4)
            });
          }
        }).catch(function (error) {
          console.log(error);
        });
      }).catch(function (error) {
        console.log(error);
      });
    }
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  @media screen and (max-width: 767px) {
    .sizeTab{
      font-size:7px;
    }
  }
</style>
