import Vue from 'vue'
import Vuex from 'vuex'
import axios from '@/api/api'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    token: "Bearer " + localStorage.getItem("accessToken"),
    username: localStorage.getItem("username"),
    userImage: localStorage.getItem("userImage"),
    postId: ""
  },
  mutations: {
    login(state, {accessToken, username}) {
      state.token = "Bearer " + accessToken;
      state.username = username;
    },
    userImage(state, userImage) {
      state.userImage = userImage;
      localStorage.setItem("userImage", userImage);
    },
    logout(state) {
      state.token = "Bearer " + localStorage.getItem("accessToken");
      state.username = localStorage.getItem("username");
      state.userImage = localStorage.getItem("userImage");
    },
    setPostId(state,postId){
      console.log("wo ti joa le")
      state.postId=postId;
    }
  },
  actions: {
    userLoad(context, username) {
      axios({
        method: "get",
        url: "user/" + username
      })
        .then(response => {
          context.commit('userImage',response.data.imageUrl);
        })
        .catch(error => {
          console.log(error);
        });
    }
  },
  modules: {
  }
})
