package com.example.myfishbuy.ui.splash;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.WindowManager;

import com.example.myfishbuy.MainActivity;
import com.example.myfishbuy.R;
import static java.lang.Thread.sleep;


public class SplashActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        getWindow().addFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN);//隐藏手机状态栏
        getSupportActionBar().hide();//隐藏活动标题栏
        //加载启动界面
        setContentView(R.layout.activity_splash);
        //在新线程中，延迟2秒钟，利用intent启动 MainActivity
        Thread myThread=new Thread(){
            @Override
            public  void run() {
                try {
                    sleep(2000);
                    Intent intent=new Intent(SplashActivity.this, MainActivity.class);
                    startActivity(intent);
                    finish();//结束此活动
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        };
        myThread.start();//启动线程
    }
}//class splashActivity
