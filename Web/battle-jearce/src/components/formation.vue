<template>
  <b-container>
    <b-row>
      <b-col lg="6" offset-lg="4">
        <b-form>
          <b-form-group label="personnage1"
                        label-for="personnage1">
            <b-form-input id="personnage1"
                          type="text"
                          placeholder="enter nom p1"
                          v-model="p1">
            </b-form-input>
          </b-form-group>

          <b-form-group label="personnage2"
                        label-for="personnage2">
            <b-form-input id="personnage2"
                          type="text"
                          placeholder="enter nom p2"
                          v-model="p2">
            </b-form-input>
          </b-form-group>

          <b-form-group label="personnage3"
                        label-for="personnage3">
            <b-form-input id="personnage3"
                          type="text"
                          placeholder="enter nom p3"
                          v-model="p3">
            </b-form-input>
          </b-form-group>

          <b-form-group label="personnage4"
                        label-for="personnage4">
            <b-form-input id="personnage4"
                          type="text"
                          placeholder="enter nom p4"
                          v-model="p4">
            </b-form-input>
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
        p1: '',
        p2: '',
        p3: '',
        p4: ''
      }
    },
    methods:{
      senddata() {
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
        }).catch(function (error) {
          alert("Une erreur est survenue, l'ajout de formation a été interompue,\nveuillez rééseiller ulterieurement.\n" + error);
          console.log(error);
        });
      }
    }
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
