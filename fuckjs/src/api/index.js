import axios from 'axios'

axios.defaults.baseURL = 'https://api.z19.site/';
if (localStorage.getItem("AccessToken") !== null) {
    axios.defaults.headers.common['Authorization'] = "bearer " + localStorage.getItem("AccessToken");
}
axios.defaults.headers.post['Content-Type'] = 'application/json';

class api {
    static getCancelToken() {
        let CancelToken = axios.CancelToken;
        let source = CancelToken.source();
        return source;
    }

    static setUserInfo(data) {
        localStorage.setItem("AccessToken", data.accessToken);
        localStorage.setItem("RefreshToken", data.refreshToken);
        axios.defaults.headers.common['Authorization'] = "bearer " + localStorage.getItem("AccessToken");
    }

    static async signIn(username, password) {
        let response = await axios
            .post("/auth/signin", {
                username,
                password
            });
        this.setUserInfo(response.data);
        return response;
    }

    static signOut() {
        localStorage.removeItem("AccessToken");
        localStorage.removeItem("RefreshToken");
        axios.defaults.headers.common['Authorization'] = "";
    }

    static signUp(username, password) {
        return axios
            .post("/auth/signup", {
                username,
                password
            });
    }

    static getUserInfo(id) {
        return axios.get("/user/" + id);
    }

    static editUserInfo(nickname, phone, birthDate, sex, address, imageUrl) {
        return axios.put("/user/edit", {
            nickname,
            phone,
            birthDate,
            sex,
            address,
            imageUrl
        })
    }

    static changePassword(userId, oldPassword, newPassword) {
        return axios.post("/auth/change-password", {
            username: userId,
            oldPassword,
            newPassword
        })
    }

    static deleteUser(username) {
        return axios.delete("/user/" + username);
    }

    static getQrCode() {
        return axios.get("/qrcode/code");
    }

    static getQrCodeStatus(id,cancelToken) {
        return axios.post("/qrcode/status", { id }, { cancelToken });
    }

    static uploadImage(data) {
        let formData = new FormData();
        formData.append("formFile", data);
        return axios.post("/image/upload", formData, {
            headers: { "Content-Type": "multipart/form-data" }
        })
    }

    static getPosts(url, beforeDateTime, skip, take) {
        return axios
            .get(url, {
                params: {
                    beforeDateTime,
                    skip,
                    take
                }
            })
    }

    static getPostDetail(id) {
        return axios.get("/post/" + id);
    }

    static getCollectionStatus(id) {
        return axios.get(`/collection/${id}`);
    }

    static updateCollectionStatus(id, privacy) {
        return axios.post(`/collection/${id}?privacy=${privacy}`)
    }

    static deleteCollection(id) {
        return axios.delete(`/collection/${id}`);
    }

    static newPost(title, content, tags, status, price, address, medias) {
        return axios.post("/post/new", {
            title,
            content,
            tags,
            status,
            price,
            address,
            medias
        })
    }

    static editPost(id, title, content, tags, status, price, address, medias) {
        return axios.put("/post/" + id, {
            title,
            content,
            tags,
            status,
            price,
            address,
            medias
        })
    }

    static deletePost(id) {
        return axios.delete("/post/" + id);
    }

    static newComment(id, content) {
        return axios.post("/post/" + id + "/comment/new", JSON.stringify(content));
    }

    static deleteComment(id, commentId) {
        return axios.delete("/post/" + id + "/comment/" + commentId);
    }

    static searchPostByTitle(keyword, beforeDateTime, skip, take) {
        return axios.get("/search/posts" + keyword, {
            params: {
                beforeDateTime,
                skip,
                take
            }
        });
    }

    static searchPostByTag(keyword, beforeDateTime, skip, take) {
        return axios.get("/search/tags" + keyword, {
            params: {
                beforeDateTime,
                skip,
                take
            }
        });
    }

    static searchUser(keyword, skip, take) {
        return axios.get("/search/user" + keyword, {
            params: {
                skip,
                take
            }
        });
    }


}

export default api
