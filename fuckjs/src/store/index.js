import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    username: localStorage.getItem("Username"),
    userId: localStorage.getItem("UserId"),
    userImage: localStorage.getItem("UserImage"),
  },
  mutations: {
    setUser(state, value) {
      state.userId = value.userId
      state.username = value.username
      state.userImage = value.userImage
      localStorage.setItem("UserId", value.userId)
      localStorage.setItem("Username", value.username)
      localStorage.setItem("UserImage", value.userImage)
    },
    removeUser(state) {
      state.userId = ""
      state.username = ""
      state.userImage = ""
      localStorage.removeItem("UserId")
      localStorage.removeItem("Username")
      localStorage.removeItem("UserImage")
    },

  },
  actions: {
  },
  modules: {
  }
})
