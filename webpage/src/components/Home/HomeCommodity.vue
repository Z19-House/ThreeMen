<template>
  <div id="homeCommodity">
    <div>
      <h1 style="font-size:30px;">
        <i class="el-icon-goods" />精选好货
      </h1>
    </div>
    <div>
      <ul class="list">
        <li v-for="(shopData,index) in objectArray" class="list-item" :key="index" ref="asd">
          <a>
            <el-image
              style="width: 150px; height: 150px ;margin: 30px auto 40px;"
              :src="shopData.imageUrl"
              fit="fill"
              lazy
            ></el-image>
            <div style="clear: both;line-height: 24px; padding: 0 20px; height: 65px;">
              <p
                style="height: 48px;
                                    font-size: 14px;
                                    line-height: 24px;
                                    text-align: left;
                                    color: #666;
                                    -webkit-transition: color .2s ease;
                                    transition: color .2s ease;
                                    word-break: break-all;
                                    overflow: hidden;
                                    text-overflow: ellipsis;
                                    display: -webkit-box;
                                    -webkit-line-clamp: 2;
                                    -webkit-box-orient: vertical;"
              >{{shopData.title}}</p>
              <div style="margin-top: 10px;">
                <div
                  style="display: inline-block;
                                            font-size: 16px;
                                            line-height: 18px;
                                            height: 18px;
                                            color: #e1251b;"
                >
                  <i>¥</i>
                  <span
                    style="font-size: 20px;
                                                    font-weight: 700;
                                                    font-family: arial,sans-serif;"
                  >{{shopData.price}}</span>
                </div>
              </div>
            </div>
          </a>
        </li>
      </ul>
    </div>
  </div>
</template>
<script>
import {
  getScrollHeight,
  getScrollTop,
  getWindowHeight
} from "../../utils/screen";

var data = 0;
export default {
  data() {
    return {
      objectArray: []
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
            "http://118.25.64.161/api/post?beforeDateTime=" +
            date +
            "&skip=" +
            data +
            "&take=20",
          headers: {
            "Content-Type": "application/json"
          }
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
      this.axios.get("http://118.25.64.161/api/post").then(response => {
        console.log(response.data);
      });
      var date = new Date().toLocaleString("chinese", { hour12: false });
      console.log(date);
      this.axios({
        method: "get",
        url:
          "http://118.25.64.161/api/post?beforeDateTime=" +
          date +
          "&skip=" +
          data +
          "&take=20",
        headers: {
          "Content-Type": "application/json"
        }
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
      console.log(this.$ref.asd);
    }
  }
};
</script>

<style>
#homeCommodity {
  margin-top: 12px;
}
.list-item {
  position: relative;
  background-color: burlywood;
  float: left;
  width: 230px;
  height: 322px;
  margin: 0 5px 10px;
  list-style: none;
}
</style>