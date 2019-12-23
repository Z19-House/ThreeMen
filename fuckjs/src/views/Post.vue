<template>
  <q-page v-if="post">
    <q-carousel
      v-if="post.medias.length > 0"
      swipeable
      animated
      v-model="slide"
      navigation
      infinite
      :autoplay="fullscreen ? false : 5000"
      :fullscreen.sync="fullscreen"
      height="300px"
    >
      <q-carousel-slide
        v-for="(media, index) in post.medias"
        :key="index"
        :name="index"
        class="column flex-center"
        style="padding: 0px"
      >
        <q-img :src="media.resUri" spinner-color="red" contain />
      </q-carousel-slide>

      <template v-slot:control>
        <q-carousel-control position="bottom-right" :offset="[18, 18]">
          <q-btn
            push
            round
            dense
            color="white"
            text-color="primary"
            :icon="fullscreen ? 'fullscreen_exit' : 'fullscreen'"
            @click="fullscreen = !fullscreen"
          />
        </q-carousel-control>
      </template>
    </q-carousel>

    <div class="q-pa-md column q-gutter-md">
      <q-card>
        <q-card-section>
          <div class="text-h5 text-red">￥{{ post.price }}</div>
          <div class="text-h6">{{ post.title }}</div>
        </q-card-section>

        <q-separator />

        <q-card-section>
          <div class="text-subtitle2">{{ post.content }}</div>
        </q-card-section>

        <q-separator />

        <q-item
          v-if="post.user.userId != $store.state.userId"
          :to="{name: 'user', params: {id: post.user.userId}}"
        >
          <q-item-section avatar>
            <q-avatar size="2.2rem">
              <img :src="post.user.imageUrl" />
            </q-avatar>
          </q-item-section>
          <q-item-section>
            <q-item-label>{{ post.user.nickname }}</q-item-label>
          </q-item-section>
        </q-item>
        <q-item v-else>
          <q-item-section avatar>
            <q-avatar size="2.2rem">
              <img :src="post.user.imageUrl" />
            </q-avatar>
          </q-item-section>
          <q-item-section>
            <q-item-label>{{ post.user.nickname }}</q-item-label>
          </q-item-section>
          <q-card-actions align="right" style="padding:0px">
            <q-btn dense color="primary" @click="editPost" style="width:80px">编辑</q-btn>
            <q-btn dense color="red" @click="confirmPostDelete = true">删除</q-btn>
          </q-card-actions>
        </q-item>
      </q-card>

      <q-dialog v-model="confirmPostDelete">
        <q-card style="min-width: 300px">
          <q-card-section>
            <div class="text-h6">确认删除？</div>
          </q-card-section>

          <q-card-actions align="right" class="text-primary">
            <q-btn flat label="取消" v-close-popup />
            <q-btn flat label="删除" color="red" @click="deletePost" v-close-popup />
          </q-card-actions>
        </q-card>
      </q-dialog>

      <q-card v-if="post.comments.length > 0">
        <q-card-section>
          <div class="text-h6">评论</div>
        </q-card-section>
        <div v-for="(comment, index) in post.comments" :key="index">
          <q-separator />
          <q-item
            v-if="comment.user.userId != $store.state.userId"
            :to="{name: 'user', params: {id: comment.user.userId}}"
          >
            <q-item-section avatar>
              <q-avatar size="2.2rem">
                <img :src="comment.user.imageUrl" />
              </q-avatar>
            </q-item-section>
            <q-item-section>
              <q-item-label>{{ comment.user.nickname }}</q-item-label>
              <q-item-label caption>{{ comment.upTime }}</q-item-label>
            </q-item-section>
          </q-item>
          <q-item v-else>
            <q-item-section avatar>
              <q-avatar size="2.2rem">
                <img :src="comment.user.imageUrl" />
              </q-avatar>
            </q-item-section>
            <q-item-section>
              <q-item-label>{{ comment.user.nickname }}</q-item-label>
              <q-item-label caption>{{ comment.upTime }}</q-item-label>
            </q-item-section>
            <q-card-actions align="right" style="padding:0px">
              <q-btn dense color="red" @click="prepareToDeleteComment(comment.commentId)">删除</q-btn>
            </q-card-actions>
          </q-item>
          <q-card-section>
            <div class="text-subtitle2">{{ comment.content }}</div>
          </q-card-section>
        </div>
      </q-card>

      <q-dialog v-model="confirmCommentDelete">
        <q-card style="min-width: 300px">
          <q-card-section>
            <div class="text-h6">确认删除评论？</div>
          </q-card-section>

          <q-card-actions align="right" class="text-primary">
            <q-btn flat label="取消" v-close-popup />
            <q-btn flat label="删除" color="red" @click="deleteComment" v-close-popup />
          </q-card-actions>
        </q-card>
      </q-dialog>

      <q-dialog v-model="inputComment">
        <q-card style="min-width: 300px">
          <q-card-section>
            <div class="text-h6">评论</div>
          </q-card-section>

          <q-card-section>
            <q-input dense v-model="commentString" autofocus @keyup.enter="addComment" />
          </q-card-section>

          <q-card-actions align="right" class="text-primary">
            <q-btn flat label="取消" v-close-popup />
            <q-btn flat label="发表评论" @click="addComment" v-close-popup />
          </q-card-actions>
        </q-card>
      </q-dialog>
    </div>
    <q-page-sticky position="bottom-right" :offset="[18, 18]">
      <q-btn fab icon="message" color="accent" @click="inputComment = true" />
    </q-page-sticky>
  </q-page>
</template>

<script>
// @ is an alias to /src
import api from "@/api";

export default {
  name: "post",
  props: {
    id: [String, Number]
  },
  components: {},
  data() {
    return {
      post: null,
      slide: 0,
      fullscreen: false,
      inputComment: false,
      commentString: "",
      confirmPostDelete: false,
      confirmCommentDelete: false,
      commentId: ""
    };
  },
  methods: {
    async getPostDetail() {
      try {
        let response = await api.getPostDetail(this.id);
        this.post = response.data;
      } catch (error) {
        console.log(error.response.data);
      }
    },
    async addComment() {
      this.inputComment = false;
      if (this.commentString) {
        try {
          await api.newComment(this.post.postId, this.commentString);
          this.commentString = "";
          this.getPostDetail();
        } catch (error) {
          console.log(error.response.data);
        }
      }
    },
    editPost() {
      this.$router.push({
        name: "edit-post",
        params: { id: this.post.postId }
      });
    },
    async deletePost() {
      try {
        await api.deletePost(this.id);
        this.$router.replace("/");
      } catch (error) {
        console.log(error.response.data);
      }
    },
    prepareToDeleteComment(commentId) {
      this.commentId = commentId;
      this.confirmCommentDelete = true;
    },
    async deleteComment() {
      try {
        if (this.commentId) {
          await api.deleteComment(this.id, this.commentId);
          this.getPostDetail();
        }
        this.commentId = "";
      } catch (error) {
        console.log(error.response.data);
      }
    }
  },
  created() {
    this.getPostDetail();
  }
};
</script>
