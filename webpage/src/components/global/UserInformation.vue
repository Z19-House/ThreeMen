<template>
  <div id="userInformation">
    <div id="register-form">
      <el-form :model="ruleForm" ref="ruleForm" label-width="100px" class="demo-ruleForm">
        <div style="height:124px">
          <div style="width:80%;float:left">
            <el-form-item label="用户名">
              <label >{{ruleForm.username}}</label>
            </el-form-item>
            <el-form-item label="昵称">
              <label v-if="isDisplay==1">{{ruleForm.nickname}}</label>
              <el-input v-else v-model="ruleForm.nickname"></el-input>
            </el-form-item>
          </div>
          <div style="float:right">
            <div style="height:115px;width:90px">
              <el-upload
                :disabled="(isDisplay==1)"
                class="avatar-uploader"
                action="http://118.25.64.161/api/image/upload"
                name="formfile"
                :headers="heads"
                :show-file-list="false"
                :on-success="handleAvatarSuccess"
                :before-upload="beforeAvatarUpload"
              >
                <img v-if="ruleForm.imageUrl" :src="ruleForm.imageUrl" class="avatar" />
                <i v-else class="el-icon-plus avatar-uploader-icon"></i>
              </el-upload>
              <div style="width:100%;text-align: center;">
                <span v-if="!(isDisplay==1)">上传头像</span>
              </div>
            </div>
          </div>
        </div>
        <el-form-item label="性别">
          <label v-if="isDisplay==1">{{ruleForm.sex}}</label>
          <el-radio-group v-else v-model="ruleForm.sex">
            <el-radio label="男"></el-radio>
            <el-radio label="女"></el-radio>
          </el-radio-group>
        </el-form-item>
        <el-form-item label="生日">
          <label v-if="isDisplay==1">{{ruleForm.birthDate}}</label>
          <el-date-picker v-else type="date" placeholder="选择日期" v-model="ruleForm.birthDate"></el-date-picker>
        </el-form-item>
        <el-form-item label="电话号码">
          <label v-if="isDisplay==1">{{ruleForm.phone}}</label>
          <el-input v-else v-model="ruleForm.phone" style="width:220px;float:left"></el-input>
        </el-form-item>
        <el-form-item label="所在地址">
          <label v-if="isDisplay==1">{{ruleForm.address}}</label>
          <el-input v-else type="textarea" v-model="ruleForm.address"></el-input>
        </el-form-item>
        <el-form-item style="text-align: center;">
          <el-button v-if="isDisplay==2" type="primary" @click="submitForm(ruleForm)">完成</el-button>
          <el-button v-else-if="isDisplay==0" type="primary" @click="submit(ruleForm)">提交</el-button>
        </el-form-item>
      </el-form>
    </div>
  </div>
</template>

<style lang="scss" scoped>
#userInformation {
  width: 520px;
  text-align: left;
}
.avatar-uploader .el-upload {
  border: 1px dashed #d9d9d9;
  border-radius: 6px;
  cursor: pointer;
  position: relative;
  overflow: hidden;
  float: left;
}
.avatar-uploader .el-upload:hover {
  border-color: #409eff;
}
.el-form .avatar-uploader-icon {
  font-size: 20px;
  color: #8c939d;
  width: 90px;
  height: 90px;
  line-height: 90px;
  text-align: center;
}
.avatar {
  width: 100px;
  height: 100px;
  display: block;
}
</style>

<script>
export default {
  props: {
    username: {
      type: String,
      default: () => ""
    },
    isDisplay: {
      type: [String],
      default: () => 1
    }
  },
  data() {
    return {
      ruleForm: {
        username: "",
        nickname: "",
        phone: "",
        birthDate: "",
        sex: "男",
        address: "",
        imageUrl: ""
      },
      heads: {
        Authorization: "Bearer " + localStorage.getItem("accessToken")
      }
    };
  },

  mounted() {
    this.LoadUserInformation();
  },
  methods: {
    submitForm(ruleForms) {
      this.axios({
        method: "put",
        url: "user/edit",
        data: JSON.stringify(ruleForms)
      })
        .then(response => {
          console.log(response);
          this.$router.replace({ path: "/" });
        })
        .catch(error => {
          console.log("错误：：：：");
          console.log(error);
        });
    },
    handleAvatarSuccess(response) {
      this.ruleForm.imageUrl = response.resUri;
    },
    LoadUserInformation() {
      this.axios
        .get("user/" + this.username)
        .then(response => {
          this.ruleForm = response.data;
          console.log(this.ruleForm);
          this.ruleForm.imageUrl = this.response.data.imageUrl;
        })
        .catch(error => {
          console.log(error);
        });
    },
    submit(ruleForms) {
      this.axios({
        method: "put",
        url: "user/edit",
        data: JSON.stringify(ruleForms)
      })
        .then(response => {
          console.log(response);
          this.$router.go(0);
        })
        .catch(error => {
          console.log("错误：：：：");
          console.log(error);
        });
    }
  }
};
</script>