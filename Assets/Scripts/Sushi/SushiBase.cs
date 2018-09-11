using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//*******************************
// シャリのオブジェクトにつける
//*******************************

public class SushiBase : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	// あたったとき
	private void OnCollisionEnter(Collision collision) {
		// 自身がシャリのとき
		if (this.transform.tag == "syari") {
			// ネタにあたったとき
			if (collision.transform.tag == "neta") {
				// ネタと合わせて寿司にする
				MakeSushiWithNetaAndSyari(collision.gameObject);
			}
		}
		// 自身が寿司のとき
		else if(this.transform.tag == "sushi") {

		}
	}



	// 寿司を作る
	private void MakeSushiWithNetaAndSyari(GameObject _neta) {
		// ネタをシャリの子供に設定
		_neta.transform.parent = this.transform;

		// 
	}
}
