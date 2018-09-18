using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGrabObject : MonoBehaviour {
	private SteamVR_TrackedObject	m_trackedObj;
	private SteamVR_Controller.Device Controller {
		get { return SteamVR_Controller.Input((int)m_trackedObj.index); }
	}

	private GameObject m_collidingObject;	// コントローラと当たっているオブジェクトへの参照
	private GameObject m_objectInHand;		// 掴んだオブジェクトへの参照



	// Use this for initialization
	void Start () {
		
	}
	

	// Update is called once per frame
	void Update () {
		// Triggerを押されたとき
		if (Controller.GetHairTriggerDown()) {
			// 手にあたっているオブジェクトがあれば
			if (m_collidingObject) {
				// 掴む
				GrabObject();
			}
		}

		// Triggerをはなした時
		if (Controller.GetHairTriggerUp()) {
			// 手にオブジェクトを掴んでいれば
			if (m_objectInHand) {
				// 離す
				ReleaseObject();
			}
		}
	}


	private void Awake() {
		m_trackedObj = GetComponent<SteamVR_TrackedObject>();
	}


	// 掴むことが可能なオブジェクトかを判断して取得
	private void SetCollidingObject(Collider _col) {
		// 既にプレイヤーが手にものを持っている、
		// または、当たっているオブジェクトが物理オブジェクトでない場合
		if(m_collidingObject ||
			!_col.GetComponent<Rigidbody>()) {
			return;	// なにもしない
		}

		// 掴むことが可能なオブジェクトを取得
		m_collidingObject = _col.gameObject;
	}


	// シャリを生成して持てるオブジェクトに追加
	private void SetSyariObject() {
		// 既にプレイヤーが手にものを持っているとき
		if (m_collidingObject) {
			return;	// 何もしない
		}

		// シャリのプレファブを取得
		GameObject syariPrefab = (GameObject)Resources.Load("Prefabs/syari");

		// シャリを生成
		GameObject syari = Instantiate(syariPrefab, this.transform.position, Quaternion.identity);

		// 掴むことが可能なオブジェクトとしてシャリを登録
		m_collidingObject = syari;
	}


	// オブジェクトを掴む
	private void GrabObject() {
		// 掴むことが可能なオブジェクを手に持っている変数にコピー
		m_objectInHand = m_collidingObject;
		// collidingObjectの参照を消す
		m_collidingObject = null;

		// オブジェクトをコントローラにつけるためのジョイントを作成
		var joint = AddFixedJoint();
		// オブジェクトのRigidbodyをジョイントにコピー
		joint.connectedBody = m_objectInHand.GetComponent<Rigidbody>();
	}


	// ジョイントを作成する
	private FixedJoint AddFixedJoint() {
		FixedJoint fj = gameObject.AddComponent<FixedJoint>();

		// 簡単に外せないように高い値にセット
		fj.breakForce = 20000;
		fj.breakTorque = 20000;
		return fj;
	}


	// 持っているオブジェクトを離す
	private void ReleaseObject() {
		// ジョイントがあることを確認
		if (this.GetComponent<FixedJoint>()) {
			// ジョイントについているrigidbodyを消す
			this.GetComponent<FixedJoint>().connectedBody = null;
			// ジョイントを削除
			Destroy(this.GetComponent<FixedJoint>());

			// コントローラの移動速度と回転速度を
			// オブジェクトにコピー
			m_objectInHand.GetComponent<Rigidbody>().velocity = Controller.velocity;
			m_objectInHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;
		}

		// 手に持っているオブジェクトへの参照を消す
		m_objectInHand = null;
	}


	// コントローラがオブジェクトと当たった瞬間
	public void OnTriggerEnter(Collider _other) {
		// 米置き場にあたったとき
		if (_other.tag == "riceBox") {
			// シャリを生成して持てるオブジェクトに追加
			SetSyariObject();
		}
		else {
			// 掴めるかを判断して取得
			SetCollidingObject(_other);
		}
	}
	// 内容はOnTriggerEnterと同じ、バグ予防
	public void OnTriggerStay(Collider _other) {
		// 掴めるかを判断して取得
		SetCollidingObject(_other);
	}


	// 離れたオブジェクトはつかめなくする
	public void OnTriggerExit(Collider _other) {
		// 掴めるオブジェクトとしてなにも登録されていないとき
		if (!m_collidingObject) {
			return;	// なにもしない
		}

		// 掴めるオブジェクトから登録を消す
		m_collidingObject = null;
	}
}
