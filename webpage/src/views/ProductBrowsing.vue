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
        <el-col :span="12">
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
                  <el-input type="textarea" :rows="2" placeholder="请输入内容" v-model="userComment" maxlength="255" show-word-limit></el-input>
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
      <div style="float:left; width:40%">
        <el-avatar :size="60" :src="details.user.imageUrl">
          <img src="https://cube.elemecdn.com/e/fd/0fc7d20532fdaf769a25683617711png.png" />
        </el-avatar>
      </div>
      <div style="float:left; width:40%">
        <span>{{details.user.nickname}}</span>
      </div>
      <div style="float:left; font-size:14px;">
        <p>
          <label>发布时间：</label>
          {{new Date( `${details.upTime}Z`).toLocaleString("chinese", { hour12: false })}}
        </p>
        <p>
          <label>修改于：</label>
          {{new Date( `${details.editTime}Z`).toLocaleString("chinese", { hour12: false })}}
        </p>
      </div>
    </div>
  </div>
</template>

<style >
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
  background-color: #fff;
  width: 20%;
  float: left;
  position: sticky;
  top: 2px;
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
  data() {
    return {
      userComment: "",
      details: { user: {} },
      type: ["", "success", "warning", "danger", "info"]
    };
  },
  computed: {

    userImage() {
      return this.$store.state.userImage;
    }
  },
  mounted() {
    this.LoadMerchandise();
  },
  methods: {
    LoadMerchandise() {
      console.log(this.userImage);
      this.axios({
        method: "get",
        url: "post/" + this.$route.params.postId,

      })
        .then(response => {
          console.log(response);
          this.details = response.data;
        })
        .catch(error => {
          console.log(error);
        });
    },
    postComment() {
      if (localStorage.getItem("accessToken")) {
        this.axios({
          method: "post",
          url:
            "post/" +
            this.details.postId +
            "/comment/new",
          data: JSON.stringify(this.userComment),

        })
          .then(response => {
            console.log(response);
            this.LoadMerchandise() 
            this.userComment=""
          })
          .catch(error => {
            console.log(error);
          });
      }
    }
  }
};
</script>