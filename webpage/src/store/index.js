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
    login(state, accessToken) {
      state.token = "Bearer " + accessToken;
    },
    username(state,username){
      state.username=username;
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
          context.commit('username',response.data.username)
        })
        .catch(error => {
          console.log(error);
        });
    }
  },
  modules: {
  }
})
