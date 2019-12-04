package com.example.myfishbuy;

import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;

import com.example.myfishbuy.http.HttpUtil;
import com.google.android.material.bottomnavigation.BottomNavigationView;

import androidx.appcompat.app.AppCompatActivity;
import androidx.navigation.NavController;
import androidx.navigation.Navigation;
import androidx.navigation.ui.AppBarConfiguration;
import androidx.navigation.ui.NavigationUI;

import org.json.JSONObject;

import java.io.IOException;

import okhttp3.Call;
import okhttp3.Response;

public class MainActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        BottomNavigationView navView = findViewById(R.id.nav_view);
        // Passing each menu ID as a set of Ids because each
        // menu should be considered as top level destinations.
        AppBarConfiguration appBarConfiguration = new AppBarConfiguration.Builder(
                R.id.navigation_home, R.id.navigation_dashboard, R.id.navigation_user)
                .build();
        NavController navController = Navigation.findNavController(this, R.id.nav_host_fragment);
        NavigationUI.setupActionBarWithNavController(this, navController, appBarConfiguration);
        NavigationUI.setupWithNavController(navView, navController);
                //RequestBody requestBody=new FormBody.Builder().build();
                HttpUtil.post_sendOkHttpRequest("http://118.25.64.161:5000/api/User/signin?userName=test&password=1",new okhttp3.Callback(){
                    @Override
                    public void onResponse(Call call, Response response)throws IOException {
                        //得到服务器返回的具体内容
                        int responseCode=response.code();
                        String responseData=response.body().string();
                        Log.d("MainActivity", "response_code is "+responseCode);
                        Log.d("MainActivity", "responseData is "+responseData);
                        //parseJSONWithJSONObject(responseData);
                    }
                    @Override
                    public void onFailure(Call call,IOException e){
                        //异常处理
                        Log.d("MainActivity", "onFailure");
                    }
                });


    }

    private void parseJSONWithJSONObject(String responseData) {
        try {
            //JSONArray jsonArray=new JSONArray(responseData);
            //for(int i=0;i<jsonArray.length();i++){
            JSONObject jsonObject=new JSONObject(responseData);
            String userId=jsonObject.getString("userId");
            String userName=jsonObject.getString("userName");
            String nickname=jsonObject.getString("nickname");
            Log.d("MainActivity", "userId is "+userId);
            Log.d("MainActivity", "userName is "+userName);
            Log.d("MainActivity", "nickname is "+nickname);
            //}
        } catch (Exception e) {
            e.printStackTrace();
        }
    }


}
