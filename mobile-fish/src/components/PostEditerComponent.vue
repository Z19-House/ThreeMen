﻿<template>
  <div class="row justify-center">
    <div class="col-xs-12 col-sm-8 col-md-6 col-lg-4 q-pa-md q-gutter-md column items-stretch">
      <q-input
        ref="title"
        v-model="post.title"
        label="标题 *"
        lazy-rules
        :rules="[val => !!val || '标题不能为空']"
      />
      <q-input
        ref="content"
        v-model="post.content"
        outlined
        label="正文 *"
        type="textarea"
        lazy-rules
        :rules="[val => !!val || '正文不能为空']"
      />
      <div class="q-gutter-xs">
        <q-chip
          v-for="(tag, index) in post.tags"
          :key="index"
          removable
          @remove="removeTag(index)"
          color="primary"
          text-color="white"
          icon="cake"
        >{{ tag }}</q-chip>
        <q-chip
          clickable
          @click="inputTag = true"
          color="red"
          text-color="white"
          icon="add"
          style="width:28px;padding-left:8px"
        ></q-chip>
      </div>

      <q-dialog v-model="inputTag" persistent>
        <q-card style="min-width: 300px">
          <q-card-section>
            <div class="text-h6">标签</div>
          </q-card-section>

          <q-card-section>
            <q-input dense v-model="tagString" autofocus @keyup.enter="addTag" />
          </q-card-section>

          <q-card-actions align="right" class="text-primary">
            <q-btn flat label="取消" v-close-popup />
            <q-btn flat label="添加标签" @click="addTag" v-close-popup />
          </q-card-actions>
        </q-card>
      </q-dialog>

      <q-input
        outlined
        ref="price"
        v-model="post.price"
        label="商品价格 *"
        mask="#.##"
        fill-mask="0"
        reverse-fill-mask
        input-class="text-right"
        lazy-rules
        :rules="[val => val != 0 || '商品价格不能为零']"
      />
      <q-input
        outlined
        ref="address"
        v-model="post.address"
        label="地址 *"
        lazy-rules
        :rules="[val => !!val || '地址不能为空']"
      />

      <el-upload
        ref="imageUploader"
        action="null"
        list-type="picture-card"
        :before-upload="beforeImageUpload"
        :http-request="uploadImage"
        :on-preview="handlePictureCardPreview"
        :on-remove="handleRemove"
        :limit="9"
        accept=".jpg, .jpeg, .gif, .png, .bmp"
      >
        <i class="el-icon-plus"></i>
      </el-upload>
      <el-dialog width="100%" :visible.sync="dialogVisible">
        <img width="100%" :src="dialogImageUrl" alt />
      </el-dialog>

      <q-btn color="primary" :disable="isImageUploading" label="发布" @click="savePost" />
    </div>
  </div>
</template>


<script>
// @ is an alias to /src
import api from "@/api";

export default {
  name: "PostEditerComponent",
  components: {},
  props: {
    post: Object
  },
  data() {
    return {
      tagString: "",
      inputTag: false,
      dialogImageUrl: "",
      dialogVisible: false,
      isImageUploading: false
    };
  },
  watch: {
    post() {
      // watch监听props里status的变化，然后执行操作
      //console.log(newV);
      if (this.post.medias) {
        this.post.medias.forEach((media, index) => {
          console.log("watch:", media);
          console.log(index, this.$refs.imageUploader.uploadFiles);
          media.uid = index;
          this.$refs.imageUploader.uploadFiles.push({
            uid: index,
            url: media.resUri
          });
        });
      }
    }
  },
  methods: {
    removeTag(index) {
      this.post.tags.splice(index, 1);
    },
    addTag() {
      this.inputTag = false;
      if (this.tagString) {
        this.post.tags.push(this.tagString);
        this.tagString = "";
        console.log("Add tag " + this.tagString);
      }
    },
    async uploadImage(item) {
      this.isImageUploading = true;
      try {
        let response = await api.uploadImage(item.file);
        console.log(response.data);
        this.post.medias.push({
          resType: response.data.resType,
          resUri: response.data.resUri,
          uid: item.file.uid
        });
      } catch (error) {
        console.log(error.response.data);
        item.abort(item.file);
      }
      this.isImageUploading = false;
    },
    handleRemove(file, files) {
      console.log(file, files);
      let removed = this.post.medias.findIndex(it => it.uid == file.uid);
      if (removed >= 0) {
        this.post.medias.splice(removed, 1);
      }
    },
    handlePictureCardPreview(file) {
      console.log(file);
      this.dialogImageUrl = file.url;
      this.dialogVisible = true;
    },
    beforeImageUpload(file) {
      let isLt2M = file.size / 1024 / 1024 < 2;

      if (!isLt2M) {
        this.$q.notify({
          color: "negative",
          message: "上传图片大小不能超过 2MB!"
        });
      }
      return isLt2M;
    },
    async savePost() {
      //   console.log(this.post);
      this.$refs.title.validate();
      this.$refs.content.validate();
      this.$refs.price.validate();
      this.$refs.address.validate();

      if (
        this.$refs.title.hasError ||
        this.$refs.content.hasError ||
        this.$refs.price.hasError ||
        this.$refs.address.hasError
      ) {
        this.formHasError = true;
      } else {
        this.$emit("savePost", this.post);
      }
    }
  },
  created() {
    if (this.post.medias) {
      this.post.medias.forEach(media => {
        console.log(`Created: ${media.resUri}`);
      });
    }
  }
};
</script>
