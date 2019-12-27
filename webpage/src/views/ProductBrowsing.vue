<template>
  <div class="productBrowsing">
    <div class="productContent">
      <el-row :gutter="20">
        <el-col :span="12">
          <el-carousel indicator-position="outside">
            <el-carousel-item v-for="(url,index) in details.medias" :key="index">
              <el-image style="width: 100%; height: 100%" :src="url.resUri" fit="contain"></el-image>
            </el-carousel-item>
          </el-carousel>
        </el-col>
        <el-col :span="8">
          <div>
            <span>{{details.content}}</span>
          </div>
          <div>
            <p>
              <label>商品名称：</label>
              {{details.title}}
            </p>
            <p>
              <label>价格：</label>
              {{details.price}}
            </p>
            <p>
              <label>商品状态：</label>
              {{details.status}}
            </p>
          </div>
          <div v-if="details.tags">
            <el-tag
              v-for="(tags,index) in details.tags.split(',')"
              :key="index"
              :type="type[index]"
              style="margin-right:3px"
            >{{tags}}</el-tag>
          </div>
        </el-col>
      </el-row>
      <el-row>
        <div class="comment">
          <el-collapse>
            <el-collapse-item style="width:100%">
              <template slot="title">
                <h2 style="margin-left: 10px;">商品评论</h2>
                <i class="header-icon el-icon-info"></i>
              </template>
              <div class="commentInput">
                <div class="commentInputUser">
                  <el-avatar :size="50" :src="userImage">
                    <img src="https://cube.elemecdn.com/e/fd/0fc7d20532fdaf769a25683617711png.png" />
                  </el-avatar>
                </div>
                <div class="commentInputBox">
                  <el-input
                    type="textarea"
                    :rows="2"
                    placeholder="请输入内容"
                    v-model="userComment"
                    maxlength="255"
                    show-word-limit
                  ></el-input>
                </div>
                <div class="commentInputBtn">
                  <el-button
                    size="mini"
                    type="primary"
                    style="margin: auto;"
                    @click="postComment"
                    round
                  >发布</el-button>
                </div>
              </div>
            </el-collapse-item>
          </el-collapse>
          <div class="commentView">
            <div class="commentItem" v-for="(comments,index) in details.comments" :key="index">
              <div class="commentUser">
                <el-avatar
                  :size="40"
                  :src="comments.user.imageUrl"
                  style="margin-right: 10px;float:left;"
                >
                  <img src="https://cube.elemecdn.com/e/fd/0fc7d20532fdaf769a25683617711png.png" />
                </el-avatar>
                <div class="commentUserNickName">{{comments.user.nickname}}</div>
              </div>
              <div class="commentContent">
                <p class="commentP">{{comments.content}}</p>
                <div class="clearfix"></div>
                <div class="commentMessage">
                  <div class="messageTime">
                    <span>{{new Date( `${comments.upTime}Z`).toLocaleString("chinese", { hour12: false })}}发布</span>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </el-row>
    </div>
    <div class="Publisher">
      <div style="width:100%;margin-bottom: 5px;">
        <span>发布者：</span>
      </div>
      <router-link
        :to="{ name: 'user', params: { username: details.user.username } }"
        class="PublisherImage"
      >
        <el-avatar :size="60" :src="details.user.imageUrl">
          <img src="https://cube.elemecdn.com/e/fd/0fc7d20532fdaf769a25683617711png.png" />
        </el-avatar>
      </router-link>
      <router-link
        :to="{ name: 'user', params: { username: details.user.username } }"
        class="PublisherInfo"
      >
        <span>{{details.user.nickname}}</span>
      </router-link>
      <el-popover placement="bottom-start" width="160" trigger="hover" :disabled="disable">
        <el-switch
          v-model="privates"
          active-text="私密"
          :active-value="1"
          :inactive-value="0"
          @change="privacyStatusChange"
          style="marign-left:9px"
        ></el-switch>
        <el-checkbox
          slot="reference"
          v-model="collection"
          true-label="已收藏"
          false-label="收藏"
          @change="collectionStatusChange"
          border
        >{{collection}}</el-checkbox>
      </el-popover>
      <div class="PublishTime">
        <p class="timeItem">
          <span class="timeLable">发布时间：</span>
          <span
            class="time"
          >{{new Date( `${details.upTime}Z`).toLocaleString("chinese", { hour12: false })}}</span>
        </p>
        <p class="timeItem" style>
          <span class="timeLable" style>修改于：</span>
          <span
            class="time"
            style
          >{{new Date( `${details.editTime}Z`).toLocaleString("chinese", { hour12: false })}}</span>
        </p>
      </div>
    </div>
  </div>
</template>

<style  scoped>
a {
  color: inherit;
  text-decoration: none;
  transition: color 0.2s ease, background-color 0.2s ease;
}
p {
  display: block;
  margin-block-start: 1em;
  margin-block-end: 1em;
  margin-inline-start: 0px;
  margin-inline-end: 0px;
}
.productBrowsing {
  margin: 20px 18% 0;
  text-align: left;
}
.productBrowsing::after {
  content: ".";
  display: block;
  height: 0;
  clear: both;
  visibility: hidden;
}
.productContent {
  width: 75%;
  float: left;
  margin-right: 10px;
}
.Publisher {
  padding: 10px;
  background-color: #fff;
  width: 22%;
  float: left;
  position: sticky;
  top: 2px;
  display: block;
  margin-bottom: 30px;
  padding-right: 10px;
  box-shadow: 0 0 0 1px #eee;
  box-sizing: border-box;
}
.Publisher .PublisherImage {
  position: relative;
  display: block;
  float: left;
  width: 60px;
  height: 100px;
  overflow: hidden;
  text-decoration: none;
  border-radius: 4px;
  box-shadow: 0 0 2px 0 rgba(0, 0, 0, 0.1);
}
.Publisher .PublisherInfo {
  display: block;
  position: relative;
  padding-left: 80px;
  height: 100px;
  text-align: left;
  text-decoration: none;
}
.Publisher .PublishTime {
  text-align: left;
  font-size: 13px;
  color: #999;
}
.PublishTime::after {
  content: ".";
  display: block;
  height: 0;
  clear: both;
  visibility: hidden;
}
.PublishTime .timeItem {
  height: 15px;
  line-height: 15px;
}
.timeItem .timeLable {
  text-align: right;
  width: 65px;
  float: left;
  display: block;
  position: relative;
  text-decoration: none;
  line-height: 15px;
}
.timeItem .time {
  padding-left: 65px;
  display: block;
  position: relative;
  text-decoration: none;
  line-height: 15px;
}
.el-carousel__item {
  background-color: #99a9bf;
}
.comment {
  margin: 20px 0 0;
  width: 100%;
  height: 50px;
}
.comment::after {
  content: ".";
  display: block;
  height: 0;
  clear: both;
  visibility: hidden;
}
.commentInputUser {
  margin: 0 10px 0;
  float: left;
  width: 8%;
}
.commentInputBox {
  float: left;
  width: 70%;
}
.commentInputBtn {
  float: left;
  width: 10%;
  height: 100%;
  margin-left: 5px;
}
.commentView {
  margin-top: 2px;
  background-color: #fff;
}
.comment .commentItem {
  zoom: 1;
  padding: 15px;
  border-bottom: 1px solid #ddd;
}
.comment .commentItem .commentUser {
  width: 140px;
  float: left;
  white-space: nowrap;
  text-overflow: ellipsis;
  overflow: hidden;
}
.comment .commentItem .commentUserNickName {
  width: 90px;
  white-space: nowrap;
  text-overflow: ellipsis;
  overflow: hidden;
}
.comment .commentItem .commentContent {
  margin-left: 150px;
}
.comment .commentItem .commentP {
  padding: 10px 0;
  line-height: 180%;
}

.messageTime {
  color: #999;
  font-size: 14px;
}
</style>

<script>
export default {
  name: "productBrowsing",
  data() {
    return {
      userComment: "",
      collection: "收藏",
      disable: true,
      privates: 0,
      details: { user: {} },
      type: ["", "success", "warning", "danger", "info"]
    };
  },
  computed: {
    userImage() {
      return this.$store.state.userImage;
    },
    postId() {
      // this.LoadMerchandise(this.$store.state.postId)
      return this.$store.state.postId;
    }
  },
  watch: {
    postId() {
      //   console.log(newData)
      this.LoadMerchandise(this.postId);
      this.LoadCollectionStatus(this.postId);
    },
    collection() {
      if (this.collection == "收藏") {
        this.disable = true;
      } else {
        this.disable = false;
      }
    }
  },
  mounted() {
    this.LoadMerchandise(this.postId);
    this.LoadCollectionStatus(this.postId);
  },
  methods: {
    LoadMerchandise(postId) {
      console.log(this.userImage);
      this.axios({
        method: "get",
        url: "post/" + postId
      })
        .then(response => {
          console.log(response);
          this.details = response.data;
        })
        .catch(error => {
          console.log(error);
        });
    },
    LoadCollectionStatus(postId) {
      this.axios({
        method: "get",
        url: "collection/" + postId
      })
        .then(response => {
          console.log("sdadasd", response);
          this.collection = "已收藏";
          this.privates = response.data.privacy;
          console.log("私密：", this.privates);
        })
        .catch(error => {
          console.log(error);
          this.collection = "收藏";
          this.privates = 0;
        });
    },
    postComment() {
      if (localStorage.getItem("accessToken")) {
        this.axios({
          method: "post",
          url: "post/" + this.details.postId + "/comment/new",
          data: JSON.stringify(this.userComment)
        })
          .then(response => {
            console.log(response);
            this.LoadMerchandise(this.postId);
            this.userComment = "";
          })
          .catch(error => {
            console.log(error);
          });
      }
    },
    privacyStatusChange(status) {
      this.axios({
        method: "post",
        url: "collection/" + this.postId + "?privacy=" + status
      })
        .then(response => {
          console.log(response, "laalalalalal");
        })
        .catch(error => {
          console.log(error);
        });
    },
    collectionStatusChange(status) {
      if (status == "已收藏") {
        this.axios({
          method: "post",
          url: "collection/" + this.postId + "?privacy=" + this.privates
        })
          .then(response => {
            console.log(response);
          })
          .catch(error => {
            console.log(error);
          });
      }else{
        this.axios({
          method: "delete",
          url: "collection/" + this.postId
        })
          .then(response => {
            console.log(response);
          })
          .catch(error => {
            console.log(error);
          });
      }
    }
  }
};
</script>