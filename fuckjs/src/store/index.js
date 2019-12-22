import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    username: localStorage.getItem("Username")
  },
  mutations: {
    setUsername(state, value) {
      state.username = value
      localStorage.setItem("Username", value)
    },
    removeUsername(state) {
      state.username = ""
      localStorage.removeItem("Username")
    }
  },
  actions: {
  },
  modules: {
  }
})
