package com.example.engahmedkandil.passingdata;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.widget.EditText;

public class Received extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_received);

        Intent i = getIntent();
        String str = i.getExtras().getString("Message");
        EditText text = (EditText) findViewById(R.id.txt1);
        text.setText(str);
    }
}
