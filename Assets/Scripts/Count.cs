using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Count : MonoBehaviour {
    private Text targetText;
    public int fish_count = 0; 
	// Use this for initialization
	void Start () {
		targetText = GetComponentInChildren<Text>();//UIのテキストの取得の仕方
        targetText.text = "0"; //テキストの変更
    }
	
	// Update is called once per frame
	void Update () {
        targetText.text = fish_count.ToString();
	}
}