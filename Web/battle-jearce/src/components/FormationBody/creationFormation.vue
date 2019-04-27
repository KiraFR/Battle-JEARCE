<template>
  <b-container>
    <b-row align-h="center">
      <b-col lg="10" cols="10" offset-lg="1">
        <b-form>
          <b-form-group label="personnage1"
                        label-for="personnage1">
            <b-form-select id="personnage1" v-model="p1"  placeholder="choisisez le p1" :options="personnageDispo"></b-form-select>
          </b-form-group>

          <b-form-group label="personnage2"
                        label-for="personnage2">
            <b-form-select id="personnage2" v-model="p2" placeholder="choisisez le p2" :options="personnageDispo"></b-form-select>
          </b-form-group>

          <b-form-group label="personnage3"
                        label-for="personnage3">
            <b-form-select id="personnage3" v-model="p3" placeholder="choisisez le p3" :options="personnageDispo"></b-form-select>
          </b-form-group>

          <b-form-group label="personnage4"
                        label-for="personnage4">
            <b-form-select id="personnage4" v-model="p4" placeholder="choisisez le p4" :options="personnageDispo"></b-form-select>
          </b-form-group>


          <b-button type="submit" v-on:click="senddata" variant="light">Submit</b-button>
        </b-form>
      </b-col>
    </b-row>
  </b-container>
</template>

<script>
  export default {
    data() {
      return {
        //nombreDispo:[], 
        personnageDispo:[],
        p1: '',
        p2: '',
        p3: '',
        p4: ''
      }
    },
    methods:{
      senddata() {
        var router = this.$router;
        this.axios({
          url: "http://localhost:5000/AddFormation",
          method: "post",
          data: {
            p1: this.p1,
            p2: this.p2,
            p3: this.p3,
            p4: this.p4
          },
          useCredentails: true
        }).then(function (response) {
          console.log(response);
          router.push("Liste");
        }).catch(function (error) {
          alert("Une erreur est survenue, l'ajout de formation a été interompue,\nveuillez rééseiller ulterieurement.\n" + error);
          console.log(error);
        });
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
        console.log(resSession)
        if (resSession.data == "la variable session est vide") {
          console.log('pass')
          self.$parent.$parent.connected = false;
          router.push('/Battle-Jearce/Accueil');
        }
        self.axios({
          url: "http://localhost:5000/GetCharacter",
          method: "get",
          useCredentails: true
        }).then(function (resCharacter) {
          for (var i = 0; i < resSession.data.personnage.length; i++) {
            for (var j = 0; j < resCharacter.data.length; j++) {
              if (resSession.data.personnage[i].personnage == resCharacter.data[j]._id) {
                self.personnageDispo.push({ value: resCharacter.data[j]._id, text:resCharacter.data[j].type });
                //self.nombreDispo.push(resSession.data.personnage[i].nombre)
              }
            }
          }
        }).catch(function (error) {
          console.log(error);
        });
        }).catch(function (error) {
          console.log('pass')
        console.log(error);
      });
    }
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
