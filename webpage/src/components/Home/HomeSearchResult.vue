<template>
  <div class="homeSearchResult">
    <div class="body-contain">
      <div class="flow-loader" id="goods-list">
        <div>
          <div class="total-wrap">
            <p class="total-text">共{{total}}条数据</p>
          </div>
        </div>
        <ul class="goods-list clearfix">
          <li
            class="goods-item"
            v-for="(product,index) in page[currentPage-1].product"
            :key="index"
          >
            <router-link
              :to="{ name: 'productBrowsing',params: { postId: product.postId}}"
              target="_blank"
            >
              <div class="img">
                <el-image style="width: 100%; height: 100%" :src="product.imageUrl" fit="contain "></el-image>
              </div>
            </router-link>
            <div class="info">
              <el-link
                :href="'#/productBrowsing/'+product.postId"
                :underline="false"
                class="title"
                target="_blank"
              >{{product.title}}</el-link>
            </div>
            <div class="tags" v-if="product.tags">
              <el-tag
                v-for="(tags,index) in product.tags.split(',')"
                :key="index"
                :type="type[index]"
                effect="dark"
                size="mini"
                style="margin-left:5px"
              >#{{tags}}</el-tag>
            </div>
            <div class="productPrice">
              <span>价格：</span>
              <span style="color:red;">{{product.price}}</span>
            </div>
          </li>
        </ul>
        <el-pagination
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
.goods-list .goods-item {
  float: left;
  margin-right: 32px;
  margin-top: 20px;
}
.goods-list .goods-item.avclass:nth-child(5n + 1),
.goods-list .goods-item:nth-child(5n) {
  margin-right: 0;
}
.goods-item {
  position: relative;
  height: 238px;
  width: 168px;
  border: 1px solid #e5e9ef;
  border-radius: 4px;
}
.goods-item .img {
  height: 130px;
  width: 100%;
  border-radius: 4px;
  position: relative;
  overflow: hidden;
}
.goods-item .info {
  padding: 8px 10px 0;
}
.info .title {
  margin-bottom: 6px;
  height: 30px;
  overflow: hidden;
  font-size: 20px;
  line-height: 20px;
  color: #222;
}

.productPrice {
  height: 22px;
  line-height: 22px;
  font-size: 14px;
  color: #222;
  margin-bottom: 6px;
  overflow: hidden;
  white-space: nowrap;
  text-overflow: ellipsis;
}
.flow-loader-state {
  text-align: center;
}
.flow-loader-state-nothing {
  width: 100%;
  margin-top: 20px;
  border: 1px solid #e5e9ef;
  /* background: url(//s1.hdslb.com/bfs/static/jinkela/search/asserts/no_more.png)
    70% 100% no-repeat; */
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
      pageSize: 20,
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
    LoadPage1(Category) {
      var date = new Date().toLocaleString("chinese", { hour12: false });
      this.axios
        .get("search/"+Category+"/" + this.content, {
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
            product: response.data.data
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
              product: []
            });
            i++;
          }
          console.log(response.data, this.page, this.total, j, pages);
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
          .get("search/posts/" + this.content, {
            params: {
              skip: this.page[currentPage - 1].skip,
              take: this.pageSize
            }
          })
          .then(response => {
            this.page[currentPage - 1].product = response.data.data;
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