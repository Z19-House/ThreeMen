<template>
  <div class="row justify-center">
    <div class="col-xs-12 col-sm-8 col-md-6 col-lg-4 q-pa-md q-gutter-md column items-stretch">
      <q-input v-model="title" label="标题" />
      <q-input v-model="content" outlined label="正文" type="textarea" />
      <div class="q-gutter-xs">
        <q-chip
          v-for="(tag, index) in tags"
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
        v-model="price"
        label="商品价格"
        mask="#.##"
        fill-mask="0"
        reverse-fill-mask
        input-class="text-right"
      />
      <q-input outlined v-model="address" label="地址" />

      <el-upload
        action="null"
        list-type="picture-card"
        :http-request="uploadImage"
        :on-preview="handlePictureCardPreview"
        :on-remove="handleRemove"
        :limit="9"
        accept=".jpg, .jpeg, .gif, .png, .bmp, .svg"
      >
        <i class="el-icon-plus"></i>
      </el-upload>
      <el-dialog width="100%" :visible.sync="dialogVisible">
        <img width="100%" :src="dialogImageUrl" alt />
      </el-dialog>

      <q-btn color="primary" :disable="isImageUploading" label="发布" @click="newPost" />
    </div>
  </div>
</template>

<script>
// @ is an alias to /src
import api from "@/api";

export default {
  name: "new-post",
  components: {},
  data() {
    return {
      title: "",
      content: "",
      tags: [],
      price: 0,
      address: "",
      medias: [],
      tagString: "",
      inputTag: false,
      dialogImageUrl: "",
      dialogVisible: false,
      isImageUploading: false
    };
  },
  methods: {
    removeTag(index) {
      this.tags.splice(index, 1);
    },
    addTag() {
      this.inputTag = false;
      if (this.tagString) {
        this.tags.push(this.tagString);
        this.tagString = "";
        console.log("Add tag " + this.tagString);
      }
    },
    async uploadImage(item) {
      this.isImageUploading = true;
      try {
        let response = await api.uploadImage(item.file);
        this.medias.push({
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
    handleRemove(file) {
      let removed = this.medias.findIndex(it => it.uid == file.uid);
      if (removed >= 0) {
        this.medias.splice(removed, 1);
      }
    },
    handlePictureCardPreview(file) {
      console.log(file);
      this.dialogImageUrl = file.url;
      this.dialogVisible = true;
    },
    async newPost() {
      try {
        await api.newPost(
          this.title,
          this.content,
          this.tags.join(","),
          "sale",
          parseFloat(this.price),
          this.address,
          this.medias
        );
        this.$router.replace("/");
      } catch (error) {
        console.log(error.response.data);
      }
    }
  },
  created() {
    if (!this.$store.state.username) {
      this.$router.replace("sign-in");
    }
  }
};
</script>
