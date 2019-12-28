<template>
  <div class="homeSearchResult">
    <div class="body-contain">
      <div class="flow-loader" id="user-list">
        <div>
          <div class="total-wrap">
            <p class="total-text">共{{total}}个用户</p>
          </div>
        </div>
        <ul class="user-list clearfix">
          <li class="user-item" v-for="(user,index) in page[currentPage-1].user" :key="index">
            <div class="user-image">
              <router-link
                :to="{ name: 'user',params: { username: user.username}}"
                class="img"
                target="_blank"
              >
                <el-avatar :size="90" :src="user.imageUrl" fit="cover">
                  <img src="../../errorImage/errorImage.png" />
                </el-avatar>
              </router-link>
            </div>
            <div class="info-wrap">
              <div class="headline">
                <el-link
                  :href="'#/user/'+user.username"
                  :underline="false"
                  class="title"
                  target="_blank"
                >{{user.nickname}}</el-link>
              </div>
              <div class="user-info clearfix">
                <span>发布商品数量：{{user.postsCount}}</span>
              </div>
              <div class="up-goods clearfix">
                <div class="no-goods-item" v-if="!user.posts.length">
                  <div class="desc">这个用户很懒，什么都没发布</div>
                </div>
                <div class="goods-item" v-for="(goods,index) in user.posts" :key="index">
                  <router-link
                    :to="{ name: 'productBrowsing',params: { postId: goods.postId}}"
                    class="goods-pic"
                  >
                    <el-image style="width: 100%; height: 100%" :src="goods.imageUrl" fit="cover"></el-image>
                  </router-link>
                  <div class="goods-info clearfix">
                    <div class="good-title">
                      <el-link
                        :href="'#/productBrowsing/'+goods.postId"
                        :underline="false"
                        target="_blank"
                        style="font-size:15.5px"
                      >{{goods.title}}</el-link>
                    </div>
                    <div class="good-price">
                      价格：
                      <span style="color:red">
                        <i>¥</i>
                        {{goods.price}}
                      </span>
                    </div>
                    <div
                      class="ptime"
                    >{{new Date( `${goods.editTime}Z`).toLocaleString("chinese", { hour12: false })}}</div>
                  </div>
                </div>
              </div>
            </div>
          </li>
        </ul>
        <el-pagination
          style="text-align:center"
          background
          layout="prev, pager, next, jumper"
          :current-page.sync="currentPage"
          :total="total"
          :page-size="pageSize"
          :pager-count="pagerCount"
          @current-change="currentChange"
          hide-on-single-page
        ></el-pagination>
        <div class="flow-loader-state" v-if="total==0">
          <div class="flow-loader-state-nothing">
            <p class="text">这里啥都没有</p>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style  scoped>
div {
  display: block;
}
p {
  margin: 0;
}
a {
  text-decoration: none;
  outline: 0;
  cursor: pointer;
}
li,
ol,
ul {
  margin: 0;
  padding: 0;
  list-style: none;
}
.homeSearchResult {
  text-align: left;
}
.body-contain .flow-loader {
  margin-bottom: 30px;
}
.clearfix:after {
  content: "";
  display: block;
  height: 0;
  clear: both;
  visibility: hidden;
  overflow: hidden;
}
.total-wrap {
  position: relative;
  margin-left: 2px;
  text-align: left;
}
.total-text {
  font-size: 14px;
  display: inline-block;
  color: #99a2aa;
  line-height: 16px;
}
.user-list .user-item {
  position: relative;
  padding: 20px 0 20px 102px;
  border-bottom: 1px solid #e5e9ef;
  z-index: 1;
}
.user-item .user-image {
  position: absolute;
  width: 90px;
  height: 90px;
  left: 0;
  top: 20px;
}
.user-item .user-image .img {
  display: block;
  width: 100%;
  height: 100%;
  border-radius: 50%;
  overflow: hidden;
}
.user-item .headline {
  margin-bottom: 12px;
}
.user-item .title {
  display: inline-block;
  line-height: 21px;
  vertical-align: middle;
  font-size: 16px;
  color: #222;
  margin-right: 14px;
  font-weight: 700;
}
.user-item .user-info {
  margin-bottom: 10px;
}
.user-item .user-info > span {
  float: left;
  margin-right: 15px;
  font-size: 12px;
  line-height: 12px;
  color: #6d757a;
}
.user-item .up-goods {
  position: relative;
}

.user-item .up-goods .goods-item {
  float: left;
  position: relative;
  width: 158px;
  height: 80px;
  padding-left: 112px;
  margin-right: 10px;
}
.user-item .no-goods-item .desc {
  position: relative;
  padding: 0;
  margin-bottom: 14px;
  width: 750px;
  font-size: 15px;
  color: #6d757a;
  line-height: 16px;
}
.user-item .up-goods .goods-item .goods-pic {
  position: absolute;
  top: 0;
  left: 0;
  width: 100px;
  height: 80px;
  border-radius: 4px;
}
.user-item .up-goods .goods-info {
  position: relative;
  height: 100%;
}
.user-item .up-goods .goods-item .good-title {
  float: left;
  height: 20px;
  top: -3px;
  width: 100%;
  line-height: 20px;
  overflow: hidden;
}
.user-item .up-goods .goods-item .good-price {
  width: 100%;
  height: 30px;
  float: left;
  top: -3px;
  font-size: 13.5px;
  line-height: 30px;
}
.user-item .up-goods .ptime {
  position: absolute;
  left: 0;
  bottom: 0;
  font-size: 13px;
  line-height: 10px;
  color: #99a2aa;
}
.flow-loader-state {
  text-align: center;
}
.flow-loader-state-nothing {
  width: 100%;
  margin-top: 20px;
  border: 1px solid #e5e9ef;
  background: url(//s1.hdslb.com/bfs/static/jinkela/search/asserts/no_more.png)
    70% 100% no-repeat;
  background-color: #fff;
  line-height: 440px;
  text-align: center;
  height: inherit;
  color: #777;
  position: relative;
}
.flow-loader-state-nothing .text {
  font-size: 25px;
}
</style>

<script>
export default {
  props: {
    //选择获取发布或者收藏
    Category: {
      type: String,
      default: () => "posts"
    }
  },
  data() {
    return {
      currentPage: 1,
      total: 0,
      pageSize: 10,
      pagerCount: 10,
      type: ["", "success", "warning", "danger", "info"],
      page: [],
      content: this.$route.query.content
    };
  },
  mounted() {
    this.$nextTick(function() {
      // Code that will run only after the
      // entire view has been rendered
      this.LoadPage1(this.Category);
    });
  },
  methods: {
    currentChange(currentPage) {
      this.loadOtherPage(currentPage);
    },
    LoadPage1() {
      var date = new Date().toLocaleString("chinese", { hour12: false });
      this.axios
        .get("search/user/" + this.content, {
          params: {
            skip: 0,
            take: this.pageSize,
            beforeDateTime: date
          }
        })
        .then(response => {
          this.page.push({
            pages: 1, //页数val
            pageSize: response.data.data.length, //条目数
            skip: 0,
            user: response.data.data
          });
          this.total = response.data.count;
          var i = 2;
          var j = this.total / this.pageSize;
          var pages = (0 | j) + 1;
          while (i <= pages) {
            this.page.push({
              pages: i, //页数val
              pageSize: 0, //条目数
              skip: this.page[i - 2].skip + this.pageSize,
              user: []
            });
            i++;
          }
          console.log(response, this.page, this.total, j, pages);
        })
        .catch(error => {
          console.log(error);
          this.page = [];
          this.total = 0;
        });
    },
    loadOtherPage(currentPage) {
      if (this.page[currentPage - 1].pageSize == 0) {
        this.axios
          .get("search/user/" + this.content, {
            params: {
              skip: this.page[currentPage - 1].skip,
              take: this.pageSize
            }
          })
          .then(response => {
            this.page[currentPage - 1].user = response.data.data;
            this.page[currentPage - 1].pageSize = response.data.data.length;
            console.log(response.data, this.page, this.total);
          })
          .catch(error => {
            console.log(error);
          });
      }
    }
  }
};
</script>