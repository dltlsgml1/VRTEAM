using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViveControllerInputTest : MonoBehaviour {
	// コントローラやカメラを参照して操作できるオブジェクト
	private SteamVR_TrackedObject	m_trackedObj;
	// コントローラの入力情報を取得する関数
	private SteamVR_Controller.Device Controller {
		get { return SteamVR_Controller.Input((int)m_trackedObj.index); }
	}



	// Use this for initialization
	void Start () {
		
	}
	

	// Update is called once per frame
	void Update () {
		
	}


	private void Awake() {
		m_trackedObj = GetComponent<SteamVR_TrackedObject>();
	}
}
