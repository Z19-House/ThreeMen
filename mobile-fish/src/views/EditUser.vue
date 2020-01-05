<template>
  <div class="row justify-center">
    <div class="col-xs-12 col-sm-8 col-md-6 col-lg-4 q-pa-md q-gutter-md column items-stretch">
      <el-upload
        class="avatar-uploader self-center"
        action="null"
        :http-request="uploadImage"
        :show-file-list="false"
        :before-upload="beforeImageUpload"
        accept=".jpg, .jpeg, .gif, .png, .bmp"
      >
        <q-avatar size="100px">
          <q-img
            v-if="imageUrl"
            :src="imageUrl"
            placeholder-src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAJYAAACWBAMAAADOL2zRAAAAG1BMVEXMzMyWlpaqqqq3t7fFxcW+vr6xsbGjo6OcnJyLKnDGAAAACXBIWXMAAA7EAAAOxAGVKw4bAAABAElEQVRoge3SMW+DMBiE4YsxJqMJtHOTITPeOsLQnaodGImEUMZEkZhRUqn92f0MaTubtfeMh/QGHANEREREREREREREtIJJ0xbH299kp8l8FaGtLdTQ19HjofxZlJ0m1+eBKZcikd9PWtXC5DoDotRO04B9YOvFIXmXLy2jEbiqE6Df7DTleA5socLqvEFVxtJyrpZFWz/pHM2CVte0lS8g2eDe6prOyqPglhzROL+Xye4tmT4WvRcQ2/m81p+/rdguOi8Hc5L/8Qk4vhZzy08DduGt9eVQyP2qoTM1zi0/uf4hvBWf5c77e69Gf798y08L7j0RERERERERERH9P99ZpSVRivB/rgAAAABJRU5ErkJggg=="
          />
          <i v-else class="el-icon-plus" />
        </q-avatar>
      </el-upload>

      <q-input
        outlined
        v-model="nickname"
        ref="nickname"
        label="昵称 *"
        lazy-rules
        :rules="[val => val && val.length >= 2 && val.length <= 32 || '昵称在2-32位之间']"
      />
      <q-input outlined v-model="phone" label="手机" mask="############" />
      <q-input outlined v-model="birthDate" label="生日" mask="date">
        <template v-slot:append>
          <q-icon name="event" class="cursor-pointer">
            <q-popup-proxy ref="qDateProxy" transition-show="scale" transition-hide="scale">
              <q-date v-model="birthDate" @input="() => $refs.qDateProxy.hide()" />
            </q-popup-proxy>
          </q-icon>
        </template>
      </q-input>
      <q-select outlined v-model="sex" label="性别" :options="[
          '保密', '男', '女'
        ]" />
      <q-input outlined v-model="address" label="地址" />

      <q-btn color="primary" label="保存用户信息" @click="editUserInfo" />
    </div>
  </div>
</template>


<script>
// @ is an alias to /src
import api from "@/api";

export default {
  name: "edit-user",
  components: {},
  data() {
    return {
      firstSign: this.$route.query.firstSign,
      nickname: "",
      phone: "",
      birthDate: "",
      sex: "",
      address: "",
      imageUrl: ""
    };
  },
  methods: {
    async uploadImage(item) {
      let response = await api.uploadImage(item.file);
      this.imageUrl = response.data.resUri;
    },
    async getUserInfo() {
      let response = await api.getUserInfo(this.$store.state.userId);
      this.nickname = response.data.nickname;
      this.phone = response.data.phone;
      this.birthDate = response.data.birthDate;
      this.sex = response.data.sex;
      this.address = response.data.address;
      this.imageUrl = response.data.imageUrl;
    },
    async editUserInfo() {
      this.$refs.nickname.validate();
      if (this.$refs.nickname.hasError) {
        this.formHasError = true;
      } else {
        try {
          let user = (await api.editUserInfo(
            this.nickname,
            this.phone,
            this.birthDate,
            this.sex,
            this.address,
            this.imageUrl
          )).data;
          this.$store.commit("setUser", {
            userId: user.userId,
            username: user.username,
            userImage: user.imageUrl
          });
          this.$q.notify({
            icon: "done",
            color: "positive",
            message: "保存用户信息成功！"
          });
          if (this.firstSign) {
            this.$router.replace("/");
          } else {
            this.$router.go(-1);
          }
        } catch (error) {
          console.log(error);
        }
      }
    },
    uploadedImage(info) {
      this.imageUrl = JSON.parse(info.xhr.response).resUri;
    },
    removedImage() {
      this.imageUrl = "";
    },
    beforeImageUpload(file) {
      let isLt2M = file.size / 1024 / 1024 < 2;

      if (!isLt2M) {
        this.$q.notify({
          color: "negative",
          message: "上传头像图片大小不能超过 2MB!"
        });
      }
      return isLt2M;
    }
  },
  created() {
    this.getUserInfo();
  }
};
</script>

<style >
.avatar-uploader .el-upload {
  border: 1px dashed #d9d9d9;
  border-radius: 999px;
  cursor: pointer;
  position: relative;
  overflow: hidden;
}
.avatar-uploader .el-upload:hover {
  border-color: #409eff;
}
</style>