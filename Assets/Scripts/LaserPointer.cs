using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour {
	// カメラリグ
	public Transform	m_cameraRigTransform;

	// 的のプレファブ
	public	GameObject	m_teleportReticlePrefab;

	// 的のインスタンス
	private	GameObject	m_reticle;

	// 的のTransform
	private	Transform	m_teleportReticleTransform;

	// プレイヤーの頭
	public	Transform	m_headTransform;

	// 的と床が重ならないにOffsetを設定
	public	Vector3		m_teleportReticleOffset;

	// テレポート可能なエリアを区別するためのマスク
	public	LayerMask	m_teleportMask;

	// テレポート先がテレポート可能かの判断用
	private	bool		m_shouldTeleport;

	// コントローラやカメラを参照して操作できるオブジェクト
	private	SteamVR_TrackedObject	m_trackedObj;
	// コントローラの入力情報を取得する関数
	private	SteamVR_Controller.Device Controller {
		get { return SteamVR_Controller.Input((int)m_trackedObj.index); }
	}

	// レーザのプレファブの参照
	public	GameObject	m_laserPrefab;
	// レーザのインスタンス
	private	GameObject	m_laser;
	// レーザのtransform
	private	Transform	m_laserTransform;
	// レーザがあたる点のベクトル情報
	private	Vector3		m_hitPoint;

	// Use this for initialization
	void Start () {
		// レーザをプレファブから生成
		m_laser = Instantiate(m_laserPrefab);
		// アクセスしやすいようにTransformを取得
		m_laserTransform = m_laser.transform;
		// 的をプレファブから生成
		m_reticle =Instantiate(m_teleportReticlePrefab);
		// 的のTransformを取得
		m_teleportReticleTransform = m_reticle.transform;
	}
	
	// Update is called once per frame
	void Update () {
		// タッチパッドを押されている間
		if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad)) {
			RaycastHit hit;

			// コントローラからレイを飛ばして、100m以内に当たったら
			if(Physics.Raycast(m_trackedObj.transform.position, this.transform.forward, out hit, 100, m_teleportMask)) {
				m_hitPoint = hit.point;
				// レーザを表示
				ShowLaser(hit);
				// 的を表示
				m_reticle.SetActive(true);
				// Offsetを当たっている位置に加える
				m_teleportReticleTransform.position = m_hitPoint + m_teleportReticleOffset;
				// テレポートを可能にする
				m_shouldTeleport = true;
				// レーザーの表示
				m_laser.SetActive(true);
			}
		}
		else {
			// レーザを非表示
			m_laser.SetActive(false);
			// 的を非表示
			m_reticle.SetActive(false);
		}

		// タッチパッドが押されていて、かつ、テレポートが可能な時
		if(Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) &&
			m_shouldTeleport == true) {
			// テレポート
			Teleport();
		}
	}

	private void Awake() {
		m_trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

	//――――――レーザを表示する関数―――――//
	private void ShowLaser(RaycastHit _hit) {
		// レーザと当たっているオブジェクトの位置とコントローラの位置の中心点を求めて、レーザオブジェクトの位置にする
		m_laserTransform.position = Vector3.Lerp(m_trackedObj.transform.position, m_hitPoint, .5f);
		// レーザオブジェクトを当たっているオブジェクトに向かわせる
		m_laserTransform.LookAt(m_hitPoint);
		// レーザのｚ軸の長さを当たった場所まで伸ばす
		m_laserTransform.localScale = new Vector3(m_laserTransform.localScale.x, m_laserTransform.localScale.y, _hit.distance);
	}
	//―――――――――――――――――――――//


	//――――――テレポート関数―――――//
	private void Teleport() {
		// テレポート中に再テレポートできないようにする
		m_shouldTeleport = false;
		// 的を消す
		m_reticle.SetActive(false);
		// カメラリグの位置とプレイヤーの頭の位置の差を求める
		Vector3 difference;
		difference = m_cameraRigTransform.position - m_headTransform.position;
		// 高さの差を消す
		difference.y = 1.5f;
		// テレポート先の位置に差を加える
		m_cameraRigTransform.position = m_hitPoint + difference;
	}
	//――――――――――――――――――//
}
