<template>
  <b-container>
    <b-row align-h="center">
      <b-col lg="auto">
        <h4 class="titreStyle">Connexion</h4>
      </b-col>
    </b-row>
    <b-row>
      <b-col lg="8" offset-lg="2">
        <b-form>
          <b-form-group id="Mail"
                        label="Mail"
                        label-for="mail">
            <b-form-input id="mail"
                          type="email"
                          required
                          v-model="email"
                          placeholder="enter email">
            </b-form-input>
          </b-form-group>
          <b-form-group id="Password"
                        label="Password"
                        label-for="InputId">
            <b-form-input id="InputId"
                          type="password"
                          required
                          v-model="password"
                          placeholder="enter password">
            </b-form-input>
          </b-form-group>
          <b-button type="submit" v-on:click="verifConnexion" variant="light">Submit</b-button>
        </b-form>
      </b-col>
    </b-row>
  </b-container>
</template>

<script>
  export default {
    data() {
      return {
        email: "",
        password:"",
      }
    },
    methods: {
      verifConnexion() {
        var self = this;
        var router = this.$router;
        var data = this.email + "," + this.password;
        this.axios({
          url: "http://localhost:5000/GetUser/" + data,
          method: "get",
          useCredentails: true
        }).then(function (response) {
          console.log(response);
          if (response.data != "") {
            console.log(self.$connected);
            self.$parent.$parent.connected = true;
            console.log(self.$connected);
            router.push('Accueil');
          } else {
            alert("valeur erronée");
          }
        }).catch(function (error) {
          alert("Une erreur est survenue, la connexion est a été interompue,\nveuillez rééseiller ulterieurement.\n" + error);
          console.log(error);
        });
      }
    }
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
