<template>
  <div id="homeCommodity">
    <div>
      <h1 style="font-size:30px;">
        <i class="el-icon-goods" />精选好货
      </h1>
    </div>
    <div>
      <ul class="shopDataList">
          <router-link
            v-for="(shopData,index) in objectArray"
            :key="index"
            :to="{ name: 'productBrowsing',params: { postId: shopData.postId}}"
            tag="li"
          >
            <el-card class="list-item" shadow="hover">
              <el-image
                style="width: 150px; height: 150px ;"
                :src="shopData.imageUrl"
                fit="cover"
              />
              <div style="line-height: 30px; padding: 0 20px; height: 30px;">
                <p style="font-size: 14px;line-height: 20px;">{{shopData.title}}</p>
              </div>
              <div
                style=" font-size: 16px;line-height: 30px;height: 30px;color: #e1251b;"
                v-if="shopData.tags"
              >
                <el-tag
                  v-for="(tags,index) in shopData.tags.split(',')"
                  :key="index"
                  :type="type[index]"
                  effect="dark"
                  size="mini"
                  style="margin-right:3px"
                >#{{tags}}</el-tag>
              </div>
              <div style="margin-top: 10px;">
                <div style=" font-size: 16px;line-height: 18px;height: 18px;color: #e1251b;">
                  <i>¥</i>
                  <span style="font-size: 20px; font-weight: 700;">{{shopData.price}}</span>
                </div>
              </div>
            </el-card>
          </router-link>
      </ul>
    </div>
  </div>
</template>
<script>
import {
  getScrollHeight,
  getScrollTop,
  getWindowHeight
} from "../utils/screen";

var data = 0;
export default {
  name: "homeCommodity",
  data() {
    return {
      objectArray: [],
      type: ["", "success", "warning", "danger", "info"]
    };
  },
  mounted() {
    window.addEventListener("scroll", this.load);
    this.LoadMerchandise();
  },
  destroyed() {
    window.removeEventListener("scroll", this.load, false);
  },

  methods: {
    load() {
      if (getScrollTop() + getWindowHeight() >= getScrollHeight()) {
        //拉取接口数据
        var date = new Date().toLocaleString("chinese", { hour12: false });
        this.axios({
          method: "get",
          url:
            "post?beforeDateTime=" +
            date +
            "&skip=" +
            data +
            "&take=20",

        })
          .then(response => {
            console.log(response);
            Array.from(response.data.data).forEach(it =>
              this.objectArray.push(it)
            );
            console.log(this.objectArray);
            data += 20;
          })
          .catch(error => {
            console.log(error);
          });
      }
    },
    LoadMerchandise() {
      this.axios.get("post").then(response => {
        console.log(response.data);
      });
      var date = new Date().toLocaleString("chinese", { hour12: false });
      console.log(date);
      this.axios({
        method: "get",
        url:
          "post?beforeDateTime=" +
          date +
          "&skip=" +
          data +
          "&take=20",

      })
        .then(response => {
          Array.from(response.data.data).forEach(it =>
            this.objectArray.push(it)
          );
          console.log(response);
          console.log(this.objectArray);
          data += 20;
        })
        .catch(error => {
          console.log(error);
        });
    }
  }
};
</script>

<style>


#homeCommodity {
    position: relative;
    margin: 20px 16% 0;
  
}
.list-item {
  float: left;
  width: 230px;
  height: 322px;
  margin: 0  0 20px 20px;
}
li{
list-style: none;
}
.shopDataList::after {
  content: ".";
  display: block;
  height: 0;
  clear: both;
  visibility: hidden;
}
</style>