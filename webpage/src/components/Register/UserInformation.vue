<template>
  <div id="userInformation">
    <div id="register-form">
      <el-form :model="ruleForm" ref="ruleForm" label-width="100px" class="demo-ruleForm">
        <div style="height:124px">
          <div style="width:80%;float:left">
            <el-form-item label="用户名">
              <el-input v-model="ruleForm.username" :disabled="true"></el-input>
            </el-form-item>
            <el-form-item label="昵称">
              <el-input v-model="ruleForm.nickname"></el-input>
            </el-form-item>
          </div>
          <div style="float:right">
            <div style="height:115px;width:90px">
              <el-upload
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
                <span>上传头像</span>
              </div>
            </div>
          </div>
        </div>
        <el-form-item label="性别">
          <el-radio-group v-model="ruleForm.sex" style="float:left" >
            <el-radio label="男"></el-radio>
            <el-radio label="女"></el-radio>
          </el-radio-group>
        </el-form-item>
        <el-form-item label="生日" >
          <el-date-picker
            type="date"
            placeholder="选择日期"
            v-model="ruleForm.birthDate"
            style="float:left"
          ></el-date-picker>
        </el-form-item>
        <el-form-item label="电话号码">
          <el-input v-model="ruleForm.phone" style="width:220px;float:left"></el-input>
        </el-form-item>
        <el-form-item label="所在地址">
          <el-input type="textarea" v-model="ruleForm.address"></el-input>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="submitForm(ruleForm)">立即创建</el-button>
        </el-form-item>
      </el-form>
    </div>
  </div>
</template>

<style>
#userInformation {
  width: 520px;
  margin: 100px auto 0;
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
  data() {
    return {
      ruleForm: {
        username: localStorage.getItem("username"),
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
  methods: {
    submitForm(ruleForms) {
      this.axios({
        method: "put",
        url: "http://118.25.64.161/api/user/edit",

        data: JSON.stringify(
          ruleForms
        ),
        headers: {
          "Content-Type": "application/json",
          "Authorization": "Bearer " + localStorage.getItem("accessToken")
        }
      })
        .then(response => {
          console.log(response);
          this.$router.push({ path: "/home" });
        })
        .catch(error => {
          console.log("错误：：：：");
          console.log(error);
          
        });
    },
    handleAvatarSuccess(response) {
      this.ruleForm.imageUrl = response.resUri;
      this.axios({
        method: "put",
        url: "http://118.25.64.161/api/image/"+response.resUri,
        headers: {
          "Content-Type": "application/json",
          "Authorization": "Bearer " + localStorage.getItem("accessToken")
        }
      })
      .catch(error => {
          console.log(error);
          
      });
    }
  }
};
</script>