<template>
  <div class="releaseProduct">
    <h2>发布商品</h2>
    <div class="body">
      <el-form :model="form" class="a" ref="form">
        <el-form-item prop="title">
          <el-input v-model="form.title" placeholder="商品标题"></el-input>
        </el-form-item>
        <el-form-item prop="content">
          <el-input v-model="form.content" type="textarea" placeholder="商品内容"></el-input>
        </el-form-item>
        <el-form-item prop="address">
          <el-input v-model="form.address" type="textarea" placeholder="所在地址"></el-input>
        </el-form-item>
        <el-form-item label="添加标签" style="  text-align: left;" prop="tags">
          <el-button type="success" icon="el-icon-plus" @click="addTags" circle></el-button>
          <span v-if="form.tags">
            <el-tag
              ref="tags"
              v-for="(tags,index) in form.tags.split(',')"
              :key="index"
              :type="tagYype[index]"
              closable
              v-on:close="close(index)"
              style="margin-right:5px;"
            >{{tags}}</el-tag>
          </span>
        </el-form-item>
        <el-form-item label="商品价格" style="  text-align: left;" prop="price">
          <el-input-number v-model="form.price" :min="0" :precision="2" :step="0.1"></el-input-number>
        </el-form-item>
        <el-form-item label="上传图片" style="text-align: left;" prop="medias">
          <el-upload
            class="upload-demo"
            action="http://118.25.64.161/api/image/upload"
            :on-preview="handlePreview"
            :on-remove="imageRemove"
            list-type="picture"
            :on-success="Success"
            :headers="heads"
            name="formfile"
            :file-list="fileList"
          >
            <el-button size="small" type="primary">点击上传</el-button>
          </el-upload>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="onSubmit(form)">立即创建</el-button>
          <el-button @click="resetForm('form')">重置</el-button>
        </el-form-item>
      </el-form>
    </div>
  </div>
</template>

<style>
.releaseProduct {
}
.releaseProduct .body {
}
.a {
  margin: 0 auto;
  width: 500px;
}
</style>

<script>
export default {
  data() {
    return {
      heads: {
        Authorization: "Bearer " + localStorage.getItem("accessToken")
      },
      form: {
        title: "",
        content: "",
        address: "",
        status: "sale",
        tags: "",
        price: 0,
        medias: []
      },
      fileList: [],
      tag: [],
      tagYype: ["", "success", "info", "warning", "danger"]
    };
  },
  methods: {
    onSubmit(form) {
      this.axios
        .post("post/new", JSON.stringify(form))
        .then(
          this.$message({
            type: "success",
            message: "发布成功"
          }),
          this.$refs["form"].resetFields(),
          this.fileList = []
        )
        .catch(error => {
          this.$message({
            type: "error",
            message: "错误！错误码：" + error.status
          });
        });
    },
    Success(res, file, fileList) {
      this.form.medias.push({
        uid: file.uid,
        resType: file.response.resType,
        resUri: file.response.resUri
      });
      this.fileList = fileList;
      console.log(res, this.form.medias, this.fileList);
    },
    addTags() {
      if (this.tag.length != 5) {
        this.$prompt("", "请输入标签", {
          confirmButtonText: "确定",
          cancelButtonText: "取消",
          inputPattern: /[\u4E00-\u9FA5a-zA-Z0-9]/,
          inputErrorMessage: "标签里不能含有特殊字符"
        })
          .then(({ value }) => {
            this.tag.push(value);
            this.form.tags = this.tag.join(",");
          })
          .catch(() => {
            this.$message({
              type: "info",
              message: "取消输入"
            });
          });
      } else {
        this.$message({
          type: "warning",
          message: "标签只能添加五个"
        });
      }
    },
    close(index) {
      this.tag.splice(index, 1);
      this.form.tags = this.tag.join(",");
      console.log(index);
    },
    imageRemove(file) {
      var index = 0;
      while (this.form.medias[index].uid != file.uid) index++;
      this.form.medias.splice(index, 1);
      console.log(file, this.form.medias);
    },
    resetForm(form) {
      this.$refs[form].resetFields();
      this.fileList = [];
    }
  }
};
</script>