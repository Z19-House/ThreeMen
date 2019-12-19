<template>
  <div class="productBrowsing">
    <el-row :gutter="40">
      <el-col :span="12">
        <el-carousel indicator-position="outside">
          <el-carousel-item v-for="(url,index) in details.medias" :key="index">
            <el-image style="width: 100%; height: 100%" :src="url.resUri" fit="contain"></el-image>
          </el-carousel-item>
        </el-carousel>
      </el-col>
      <el-col :span="12">
          <div>
            <p>{{details.content}}</p>
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
          <p>
            <label>发布者：</label>
            {{details.user.username}}
          </p>
          <div></div>
          <div>
            <p>
              <label>发布时间：</label>
              {{new Date(details.upTime).toLocaleString()}}
            </p>
            <p>
              <label>修改于：</label>
              {{new Date(details.editTime).toLocaleString()}}
            </p>
          </div>
      </el-col>
    </el-row>
  </div>
</template>

<style>
.productBrowsing {
  margin-top: 20px;
  text-align: left;
}

.el-carousel__item {
  background-color: #99a9bf;
}
</style>

<script>
export default {
  data() {
    return {
      details: {},
      type: ["", "success", "warning", "danger", "info"]
    };
  },
  mounted() {
    this.LoadMerchandise();
  },
  methods: {
    LoadMerchandise() {
      this.axios({
        method: "get",
        url: "http://118.25.64.161/api/post/" + this.$route.params.postId,
        headers: {
          "Content-Type": "application/json"
        }
      })
        .then(response => {
          console.log(response);
          this.details = response.data;
        })
        .catch(error => {
          console.log(error);
        });
    }
  }
};
</script>