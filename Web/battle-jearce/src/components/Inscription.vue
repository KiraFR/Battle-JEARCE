<template>
  <b-container>
    <b-row>
      <b-col lg="6" offset-lg="4">
        <b-form>
          <b-form-group label="Mail"
                        label-for="InputId">
            <b-form-input id="InputId"
                          type="email"
                          required
                          placeholder="enter email"
                          v-model="email">
            </b-form-input>
          </b-form-group>

          <b-form-group label="Password"
                        label-for="Inputpw">
            <b-form-input id="Inputpw"
                          type="password"
                          required
                          placeholder="enter password"
                          v-model="password">
            </b-form-input>
          </b-form-group>

          <b-form-group label-for="Inputpwverif">
            <b-form-input id="Inputpwverif"
                          type="password"
                          required
                          placeholder="re-enter password"
                          v-model="verifpassword">
            </b-form-input>
            <b-form-invalid-feedback :state="validation">
              mots de passe différant
            </b-form-invalid-feedback>
          </b-form-group>

          <b-form-group id="Pseudo"
                        label="Pseudo"
                        label-for="Inputpseudo">
            <b-form-input id="Inputpseudo"
                          type="text"
                          required
                          placeholder="enter pseudo"
                          v-model="pseudo">
            </b-form-input>
          </b-form-group>

          <b-button type="submit" v-on:click="senddata" :disabled="!validation" variant="light">Submit</b-button>
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
        password: "",
        verifpassword: "",
        pseudo:""
      }
    },
    computed: {
      validation() {
        return this.password == this.verifpassword
      }
    },
    methods: {
      senddata() {
        var router = this.$router;
        this.axios({
          url: "http://localhost:5000/AddUser",
          method: "post",
          data: {
            pseudo: this.pseudo,
            password: this.password,
            email: this.email
          },
          useCredentails: true
        }).then(function (response) {
          console.log(response);
          router.push('Accueil');
        }).catch(function (error) {
          alert("Une erreur est survenue, l'inscription est a été interompue,\nveuillez rééseiller ulterieurement.\n" + error);
          console.log(error);
        });
      }
    }
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
