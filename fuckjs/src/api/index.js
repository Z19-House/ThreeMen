import axios from 'axios'

axios.defaults.baseURL = 'http://118.25.64.161/api';
if (localStorage.getItem("AccessToken") !== null) {
    axios.defaults.headers.common['Authorization'] = "bearer " + localStorage.getItem("AccessToken");
}
axios.defaults.headers.post['Content-Type'] = 'appliction/json';

class api {
    static async signIn(username, password) {
        let response = await axios
            .post("/auth/signin", {
                username: username,
                password: password
            });
        localStorage.setItem("Username", username);
        localStorage.setItem("AccessToken", response.data.accessToken);
        localStorage.setItem("RefreshToken", response.data.refreshToken);
        axios.defaults.headers.common['Authorization'] = "bearer " + localStorage.getItem("AccessToken");
        return response;
    }

    static signOut() {
        localStorage.removeItem("Username");
        localStorage.removeItem("AccessToken");
        localStorage.removeItem("RefreshToken");
        axios.defaults.headers.common['Authorization'] = "";
    }

    static signUp(username, password) {
        return axios
            .post("/auth/signup", {
                username: username,
                password: password
            });
    }

    static getUserInfo(id) {
        if (!id) {
            id = localStorage.getItem("Username");
        }
        return axios.get("/user/" + id);
    }

    static editUserInfo(nickname, phone, birthDate, sex, address, imageUrl) {
        return axios.put("/user/edit", {
            nickname: nickname,
            phone: phone,
            birthDate: birthDate,
            sex: sex,
            address: address,
            imageUrl: imageUrl
        })
    }

    static changePassword(oldPassword, newPassword) {
        return axios.post("/auth/change_password", {
            username: localStorage.getItem("Username"),
            oldPassword: oldPassword,
            newPassword: newPassword
        })
    }

    static deleteUser(username) {
        return axios.delete("/user/" + username);
    }

    static uploadImage(data) {
        let formData = new FormData();
        formData.append("formFile", data);
        return axios.post("/image/upload", formData, {
            headers: { "Content-Type": "multipart/form-data" }
        })
    }

    static getPosts(url, datetime, skip, take) {
        return axios
            .get(url, {
                params: {
                    beforeDateTime: datetime,
                    skip: skip,
                    take: take
                }
            })
    }

    static getPostDetail(id) {
        return axios.get("/post/" + id);
    }

    static newPost(title, content, tags, status, price, address, medias) {
        return axios.post("/post/new", {
            title: title,
            content: content,
            tags: tags,
            status: status,
            price: price,
            address: address,
            medias: medias
        })
    }

    static editPost(id, title, content, tags, status, price, address, medias) {
        return axios.put("/post/" + id, {
            title: title,
            content: content,
            tags: tags,
            status: status,
            price: price,
            address: address,
            medias: medias
        })
    }

    static deletePost(id) {
        return axios.delete("/post/" + id);
    }

    static newComment(id, content) {
        return axios.post("/post/" + id + "/comment/new", content);
    }

    static deleteComment(id, commentId) {
        return axios.delete("/post/" + id + "/comment/" + commentId);
    }

    static searchPostByTitle(keyword, datetime, skip, take) {
        return axios.get("/search/posts" + keyword, {
            params: {
                beforeDateTime: datetime,
                skip: skip,
                take: take
            }
        });
    }

    static searchPostByTag(keyword, datetime, skip, take) {
        return axios.get("/search/tags" + keyword, {
            params: {
                beforeDateTime: datetime,
                skip: skip,
                take: take
            }
        });
    }

    static searchUser(keyword, skip, take) {
        return axios.get("/search/user" + keyword, {
            params: {
                skip: skip,
                take: take
            }
        });
    }


}

export default api
