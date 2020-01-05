import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    username: localStorage.getItem("Username"),
    userId: localStorage.getItem("UserId"),
    userImage: "",
    userGroup: ""
  },
  mutations: {
    setUser(state, value) {
      state.userId = value.userId
      state.username = value.username
      state.userImage = value.userImage
      state.userGroup = value.userGroup
      localStorage.setItem("UserId", value.userId)
      localStorage.setItem("Username", value.username)
    },
    removeUser(state) {
      state.userId = ""
      state.username = ""
      state.userImage = ""
      state.userGroup = ""
      localStorage.removeItem("UserId")
      localStorage.removeItem("Username")
    },

  },
  actions: {
  },
  modules: {
  }
})
