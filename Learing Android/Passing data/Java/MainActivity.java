package com.example.engahmedkandil.passingdata;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;

public class MainActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        final EditText Etxt = (EditText) findViewById(R.id.txt);
        Button button = (Button) findViewById(R.id.btn);
        button.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent i = new  Intent(MainActivity.this,Received.class);
                String str = Etxt.getText().toString();
                i.putExtra("Message",str);
                startActivity(i);
            }
        });
    }
}
