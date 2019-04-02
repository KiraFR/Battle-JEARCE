<template>
  <b-container>
    <b-row>
      <b-col lg="6" offset-lg="4">
        <b-form>
          <b-form-group id="Mail"
                        label="Mail"
                        label-for="InputId">
            <b-form-input id="InputId"
                          type="email"
                          required
                          placeholder="enter email">
            </b-form-input>
          </b-form-group>
          <b-form-group id="Password"
                        label="Password"
                        label-for="InputId">
            <b-form-input id="InputId"
                          type="email"
                          required
                          placeholder="enter password">
            </b-form-input>
          </b-form-group>
          <b-button type="submit" variant="light">Submit</b-button>
        </b-form>
      </b-col>
    </b-row>
  </b-container>
</template>

<script>

</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
