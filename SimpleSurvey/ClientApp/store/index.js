import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

// TYPES
const SURVEY_TAKEN = 'SURVEY_TAKEN'
const SURVEY_RESULTS = 'SURVEY_RESULTS'

// STATE
const state = {
  surveryTaken: false,
  surveyResults: null
}

// MUTATIONS
const mutations = {
  [SURVEY_TAKEN] (state, obj) {
    state.surveyTaken = obj.surveyTaken
  },

  [SURVEY_RESULTS] (state, obj) {
    state.surveyResults = obj.surveyResults
  }
}

// ACTIONS
const actions = ({

  setResults({ commit }, obj) {
    commit(SURVEY_RESULTS, obj)
  },

  setTaken ({ commit }, obj) {
    commit(SURVEY_TAKEN, obj)
  }
})

export default new Vuex.Store({
  state,
  mutations,
  actions
})
