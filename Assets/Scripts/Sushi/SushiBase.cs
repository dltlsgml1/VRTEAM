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

		// ネタの位置を調整
		_neta.transform.localPosition = new Vector3(0, 0, 0.005f);
		// ネタの回転を調整
		_neta.transform.localRotation = Quaternion.identity;

		// rigidbodyを再設定
		Rigidbody rb = _neta.transform.GetComponent<Rigidbody>();
		rb.isKinematic = true;
		rb.useGravity = false;

		// コライダを無効化
		BoxCollider bc = _neta.transform.GetComponent<BoxCollider>();
		bc.enabled = false;

		// 自分を寿司に設定
		this.transform.tag = "sushi";
	}
}
