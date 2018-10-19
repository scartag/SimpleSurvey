<template>


  <div style="width: 100%; float: left;">
    <h3>Simple Survey</h3>
    <p>Take this simple survey and view the results of previous takers.</p>
    <div v-if="!surveyItems" class="text-center">
      <p><em>Loading...</em></p>
      <h1><icon icon="spinner" pulse /></h1>
    </div>
    <div v-if="surveyTaken">
      <h4>You have completed the survey.</h4>
    </div>
    <template v-if="surveyItems && surveyTaken == false">
      <div class="row">
        <div class="surveyItem" v-for="(item, index) in surveyItems[currentPage]" :key="item.id">
          <div class="lead">
            {{item.question}}
          </div>
          <div class="row">
            <div class="col-md-2" v-for="(option, index) in options" :key="index"><label> <input type="radio" v-model="item.responseId" v-bind:name="item.id" v-bind:value="option.id" /> {{option.optionText}}</label></div>
          </div>
        </div>

      </div>

      <div class="row" style="margin-top: 40px;">
        <div class="col-md-4">
          <button class="btn btn-info" v-show="currentPage == 0" v-on:click="nextPage">Next</button>
          <button class="btn btn-info" v-show="currentPage == 1" v-on:click="previousPage">Previous</button>
          <button class="btn btn-success" v-bind:disabled="!completed" v-on:click="postData">Submit</button>
        </div>
      </div>
    </template>
  </div>
</template>

<script>

  import { mapActions } from 'vuex'

  export default {
    data() {
      return {
        surveyItems: null,
        options: [],
        currentPage: 0,
        surveyTaken: false
      }
    },

    methods: {

      ...mapActions(['setResults','setTaken']),


      async loadData() {

        try {

          let item = window.localStorage.getItem('user');
          let user = JSON.parse(item);

          const auth = {
            headers: { Authorization: 'Bearer ' + user.access_token }
          };

          let response = await this.$http.get(`/api/survey/surveydata`, auth);

          this.surveyItems = response.data.items;
          this.options = response.data.options;
          this.surveyTaken = response.data.surveyTaken;

          this.setTaken({ surveyTaken: response.data.surveyTaken });

            let results = await this.$http.get(`/api/survey/surveyresults`, auth);

            this.setResults({ surveyResults: results.data });

          


          console.log(response);
        } catch (err) {
          window.alert(err);
          console.log(err + " " + user.access_token);
        }

      },

      nextPage() {

        this.currentPage = this.currentPage + 1;

      },

      previousPage() {

        this.currentPage = this.currentPage - 1;

      },

      async postData() {

        let data = [...this.surveyItems[0], ...this.surveyItems[1]];

        try {
          let item = window.localStorage.getItem('user');
          let user = JSON.parse(item);

          const auth = {
            headers: { Authorization: 'Bearer ' + user.access_token }
          };

          let response = await this.$http.post(`/api/survey/surveyentry`, data, auth);

          this.surveyTaken = response.data.surveyTaken;

          if (this.surveyTaken) {

            let results = await this.$http.get(`/api/survey/surveyresults`, auth);

            this.setResults({ surveyResults: results.data });

          }

        }
        catch (err) {
          window.alert(err);
          console.log(err);
        }

      },

    getToken:   function () {
    var hash = window.location.hash.substr(1);
    var result = hash.split('&').reduce(function (result, item) {
      var parts = item.split('=');
      result[parts[0]] = parts[1];
      return result;
    }, {});

      console.log(result);

    return result;
      }
    },

    computed: {

      completed: function () {

        const count = this.surveyItems[0].filter(function (x) { return x.responseId == null; }).length + this.surveyItems[1].filter(function (x) { return x.responseId == null; }).length;
        
        return count === 0;
      }

    },

    async created() {

      let token = this.getToken();
      let user = localStorage.getItem('user');
      if (!user) {
        localStorage.setItem('user', JSON.stringify(token));
      }

      this.loadData();

    }
  }
</script>
<style scoped>

  .lead {
    margin-bottom: 10px !important;
  }

  .surveyItem {
    width: 99%;
    float: left;
    border-bottom: 1px dotted #ccc;
    border-radius: 6px;
    height: 80px;
    margin-top: 20px;
    padding: 5px;
  }
</style>
