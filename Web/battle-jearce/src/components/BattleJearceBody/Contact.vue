<template>
  <b-container>
    <b-row align-h="center">
      <b-col lg="auto" cols="auto">
        <h4 class="titreStyle size">Nous Contacter</h4>
      </b-col>
    </b-row>
    <b-row>
      <b-col lg="8" cols="10" offset-lg="2" offset="1">
        <b-form>
          <b-form-group id="Mail"
                        label="Mail">
            <b-form-input></b-form-input>

          </b-form-group>
          <b-form-textarea id="textarea"
                           v-model="text"
                           placeholder="Enter something..."
                           rows="3"
                           max-rows="6"></b-form-textarea>
          <b-button type="submit" v-on:click="verifConnexion" variant="light">Submit</b-button>
        </b-form>
      </b-col>
    </b-row>
  </b-container>
</template>

<script>

</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  @media screen and (max-width: 767px) {
    .size{
      font-size:17px;
    }
  }
</style>
