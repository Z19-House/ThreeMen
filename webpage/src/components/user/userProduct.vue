<template>
  <div class="products">
    <h2>{{title}}</h2>
    <ul class="Productlist">
      <li v-for="(product,index) in page[currentPage-1].product" :key="index" class="productItem">
        <router-link
          :to="{ name: 'productBrowsing',params: { postId: product.postId}}"
          class="productImage"
        >
          <el-image style="width: 100%; height: 100%" :src="product.imageUrl" fit="cover"></el-image>
        </router-link>
        <router-link
          :to="{ name: 'productBrowsing',params: { postId: product.postId}}"
          class="productInfo"
        >
          <h4 class="productTitle">{{product.title}}</h4>
          <div class="productDesc">{{product.content}}</div>
          <div class="productTags" v-if="product.tags">
            <el-tag
              effect="dark"
              size="mini"
              v-for="(tags,index) in product.tags.split(',')"
              :key="index"
              :type="type[index]"
              style="margin-right:3px"
            >{{tags}}</el-tag>
          </div>
          <div class="productPrice">
            <span>价格：</span>
            <span style="color:red;">{{product.price}}</span>
          </div>
        </router-link>
        <div class="productOptions"></div>
      </li>
      <li class="follow-empty" v-if="total==0">
        <p>这里啥都没有</p>
      </li>
    </ul>
    <div class="clearfloat"></div>
    <div class="pagelistbox">
      <el-pagination
        background
        layout="total,prev, pager, next, jumper"
        :current-page.sync="currentPage"
        :total="total"
        :page-size="pageSize"
        :pager-count="pagerCount"
        @current-change="currentChange"
        hide-on-single-page
      ></el-pagination>
    </div>
  </div>
</template>

<style scoped>
a{
  color: inherit;
    text-decoration: none;
    transition: color .2s ease,background-color .2s ease;
}
.products {
  width: 100%;
  padding:20px;
  background-color: #fff;
}
.products .follow-empty {
  display: block;
  padding: 344px 0 80px;
  width: 100%;
  border-radius: 4px;
  color: #6d757a;
  overflow: hidden;
  text-align: center;
  background-size: 338px 225px;
  background-image: url("../../assets/timg .jpg");
  background-position: center 120px;
  background-repeat: no-repeat;
}
.products .Productlist {
  position: relative;
  min-height: 140px;
  margin: 0;
  padding: 0;
  list-style: none;
  padding-bottom: 10px;
  text-align: left;
}
.Productlist::after {
  content: ".";
  display: block;
  height: 0;
  clear: both;
  visibility: hidden;
}
.products .productItem {
  display: block;
  position: relative;
  width: 33.33%;
  height: 140px;
  float: left;
  margin-bottom: 30px;
  padding-right: 20px;
  box-sizing: border-box;
}
.productItem a.productImage {
  position: relative;
  display: block;
  float: left;
  width: 110px;
  height: 140px;
  overflow: hidden;
  text-decoration: none;
  border-radius: 4px;
  box-shadow: 0 0 2px 0 rgba(0, 0, 0, 0.1);
}
.productItem .productInfo {
  display: block;
  position: relative;
  padding-left: 130px;
  height: 140px;
  text-decoration: none;
}
.productItem .productOptions {
  position: absolute;
  right: 20px;
  bottom: 6px;
  font-size: 14px;
}
.productItem .productInfo .productTitle {
  font-weight: 400;
  line-height: 24px;
  font-size: 18px;
  padding: 0 10px 0 0;
  overflow: hidden;
  white-space: nowrap;
  text-overflow: ellipsis;
  margin: 0 0 8px 0;
}
.productItem .productInfo .productDesc {
  display: -webkit-box;
  font-weight: 400;
  color: #222;
  line-height: 20px;
  max-height: 40px;
  font-size: 12px;
  overflow: hidden;
  white-space: nowrap;
  text-overflow: ellipsis;
  margin: 0 0 8px 0;
  -webkit-line-clamp: 2;
  -webkit-box-orient: vertical;
  white-space: normal;
}
.productItem .productInfo .productTags {
  height: 22px;
  line-height: 22px;
  margin-bottom: 6px;
  overflow: hidden;
  white-space: nowrap;
  text-overflow: ellipsis;
}
.productItem .productInfo .productPrice {
  height: 22px;
  line-height: 22px;
  font-size: 14px;
  color: #222;
  margin-bottom: 6px;
  overflow: hidden;
  white-space: nowrap;
  text-overflow: ellipsis;
}
.products .pagelistbox {
  display: block;
  height: 40px;
  white-space: nowrap;
  padding-right: 20px;
  text-align: center;
}
.clearfix {
  display: block;
}
</style>

<script>
export default {
  props: {
    //设置标题
    title: {
      type: String,
      default: () => "我的发布"
    },
    //设置每页显示的条目数
    pageSize: {
      type: [Number, String],
      default: () => 18
    },
    //设置页码按钮的数量，当总页数超过该值时会折叠
    pagerCount: {
      type: [Number, String],
      default: () => 5
    },
    username: {
      type: String,
      default: () => "1"
    },
    //选择获取发布或者收藏
    Category:{
      type:String,
      default:()=>"posts"
    }
  },
  data() {
    return {
      currentPage: 1,
      total: 0,
      type: ["", "success", "warning", "danger", "info"],
      page: [
        {
          pages: 1, //页数val
          pageSize: 18, //条目数
          skip: 0,
          product: []
        }
      ]
    };
  },
  mounted() {
    this.$nextTick(function() {
      // Code that will run only after the
      // entire view has been rendered
      this.LoadPage1();
    });
  },
  methods: {
    currentChange(val) {
      if (this.page[val - 1].pageSize == this.pageSize) {
        console.log("加载第" + val + "页");
      } else {
        console.log("已达最大值");
      }
    },
    LoadPage1() {
      this.axios
        .get("user/" + this.username + "/"+this.Category, {
          params: {
            skip: 0,
            take: 12
          }
        })
        .then(response => {
          this.page[0].product = response.data.data;
          this.page[0].pageSize = response.data.data.length;
          this.total = response.data.count;
          console.log(response.data, this.page[0], this.total);
        })
        .catch(error => {
          console.log(error);
        });
    }
  }
};
</script>
