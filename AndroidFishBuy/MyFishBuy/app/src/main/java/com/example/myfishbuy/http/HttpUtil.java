package com.example.myfishbuy.http;

import okhttp3.FormBody;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.RequestBody;

public class HttpUtil {
    //----------------------------------get请求-------------------------------------------
    public static void get_sendOkHttpRequest(String address,okhttp3.Callback callback){
        OkHttpClient client=new OkHttpClient();
        Request request=new Request.Builder().url(address).build();
        client.newCall(request).enqueue(callback);//enqueue会在内部开出一个子线程，在子线程当中执行Http请求，将请求结果回调到okhttp3.Callback当中
    }
    //----------------------------------post请求-------------------------------------------
    //用url发送数据
    public static void post_sendOkHttpRequest(String address,okhttp3.Callback callback){
        OkHttpClient client=new OkHttpClient();
        //创建一个requestBody,不添加任何数据。
        RequestBody requestBody=new FormBody.Builder().build();
        Request request=new Request.Builder().url(address).post(requestBody).build();
        client.newCall(request).enqueue(callback);//enqueue会在内部开出一个子线程，在子线程当中执行Http请求，将请求结果回调到okhttp3.Callback当中
    }
    //用requestBody发送数据
    public static void post_sendOkHttpRequest(String address, RequestBody requestBody,okhttp3.Callback callback){
        OkHttpClient client=new OkHttpClient();
        Request request=new Request.Builder().url(address).post(requestBody).build();
        client.newCall(request).enqueue(callback);//enqueue会在内部开出一个子线程，在子线程当中执行Http请求，将请求结果回调到okhttp3.Callback当中
    }
}
