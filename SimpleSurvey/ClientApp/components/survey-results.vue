<template>
  <div style="width: 100%; float: left;">
    <h3>Survey Results</h3>
    <div v-if="!surveyResults" class="text-center">
      <p><em>Loading...</em></p>
      <h1><icon icon="spinner" pulse /></h1>
    </div>
    <template v-if="surveyResults">
      <div v-if="!surveyTaken">
        <h4>You haven't completed the survey.</h4>
      </div>
      <template v-if="surveyTaken">
        <div class="row" style="margin-top: 20px;">
          <div class="col-md-8">
            <canvas id="myChart" width="600" height="300"></canvas>
          </div>
          <div class="col-md-4">
            <div class="btn-group btn-group-toggle" data-toggle="buttons">
              <label class="btn btn-secondary" v-bind:class="{active: chartType == 1}">
                <input type="radio" name="options" id="option1" value="1" v-model="chartType" autocomplete="off"  v-on:change="doChange" checked> Bar
              </label>
              <label class="btn btn-secondary"  v-bind:class="{active: chartType == 2}">
                <input type="radio" name="options" id="option2" value="2" v-model="chartType" autocomplete="off" v-on:change="doChange"> Pie
              </label>
            </div>
          </div>
        </div>
        <div class="row" style="margin-top: 30px;">
          <div class="col-md-8">
            <nav aria-label="Page navigation">
              <ul class="pagination pagination-lg">
                <li v-for="(item, index) in surveyResults" class='page-item' v-bind:class="{active: currentPage == (index + 1)}"><a class='page-link' href="javascript:void(0)" v-on:click="gotoPage((index + 1))">{{(index + 1)}}</a></li>
               </ul>
            </nav>
          </div>
        </div>
        <div>
        </div>
      </template>
    </template>
  </div>
</template>


<script>

  import {mapState } from 'vuex'


  import Chart from 'chart.js';


  export default {
    data() {
      return {
        currentPage: 1,
        chartType: 1
      }
    },

    computed: {
      ...mapState({
        surveyResults: state => state.surveyResults,
        surveyTaken: state => state.surveyTaken
      })
    },

    methods: {

      //async loadData() {

      //  try {

      //    let item = window.localStorage.getItem('user');
      //    let user = JSON.parse(item);

      //    const auth = {
      //      headers: { Authorization: 'Bearer ' + user.access_token }
      //    };


      //    let response = await this.$http.get(`/api/survey/surveyresults`, auth);

      //    this.surveyResults = response.data;
      //    this.surveyTaken = true;
      //    console.log(response);
      //  } catch (err) {
      //    window.alert(err);
      //    console.log(err);
      //  }

      //},

      doChange: function () {

        window.myChart.destroy();

        var entry = this.surveyResults[this.currentPage - 1];
        var data = [entry.sag, entry.ag, entry.und, entry.dag, entry.sdag];
        var question = entry.surveyQuestion;

        this.renderChart(question, data, this.chartType);

      },

      gotoPage: function (index) {

        if (index == this.currentPage) return;

        this.currentPage = index;

        var entry = this.surveyResults[this.currentPage - 1];
        var data = [entry.sag, entry.ag, entry.und, entry.dag, entry.sdag];
        var question = entry.surveyQuestion;


        window.myChart.data.datasets[0].data = data;

        window.myChart.options.title.text = question;

        window.myChart.options.legend.display = this.chartType === 1 ? false : true;

        window.myChart.update();

      },

      renderChart: function (question, data, type) {


        var ctx = document.getElementById("myChart").getContext('2d');

         window.myChart = new Chart(ctx, {
           type: type == 1 ? 'horizontalBar' : 'pie',

          data: {
            labels: ["Strongly Agree", "Agree", "Undecided", "Disagree", "Strongly Disagree"],
            datasets: [{
              label: 'Responses',
              data: data,
              backgroundColor: [
                'rgba(255, 99, 132, 0.2)',
                'rgba(54, 162, 235, 0.2)',
                'rgba(255, 206, 86, 0.2)',
                'rgba(75, 192, 192, 0.2)',
                'rgba(255, 159, 64, 0.2)'
              ],
              borderColor: [
                'rgba(255,99,132,1)',
                'rgba(54, 162, 235, 1)',
                'rgba(255, 206, 86, 1)',
                'rgba(75, 192, 192, 1)',
                'rgba(255, 159, 64, 1)'
              ],
              borderWidth: 1
            }]
          },
          options: {
            title: {
              display: true,
              text: question,
              fontSize: 25
            },
            legend: {
              display: type === 1 ? false : true
            },
            scales: {
              yAxes: [{
                ticks: {
                  beginAtZero: true
                }
              }]
            }
          }
        });

      }
    },

     mounted() {

      
       var entry = this.surveyResults[this.currentPage - 1];
       var data = [entry.sag, entry.ag, entry.und, entry.dag, entry.sdag];
       var question = entry.surveyQuestion;

       this.renderChart(question, data, 1);
     

    }
    ,

    async created() {

      //await this.loadData();

 

    }
  }
</script>
<style>
</style>
