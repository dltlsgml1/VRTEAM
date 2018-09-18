using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicePartManager : MonoBehaviour {

    private SteamVR_TrackedObject m_trackedObj;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)m_trackedObj.index); }
    }

    [SerializeField]
    private GameObject countManager;

    private CountManager countManagerSc;

    private GameObject fishObject;

    // Use this for initialization
    void Start () {
        //スクリプト取得
        countManagerSc = countManager.GetComponent<CountManager>();
        //プレハブをロード
        fishObject = (GameObject)Resources.Load("Prefabs/Fish");
	}
	
	// Update is called once per frame
	void Update ()
    {
        //魚を出す
        //if(Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        if(Input.GetKey(KeyCode.A))
        {
            if (countManagerSc.getFishNum() > 0)
            {
                countManagerSc.subFish();// 魚の所持数を減らす
                //魚を実体化
                Vector3 pos = new Vector3(-4.6806f,0.967f,5.0747f);
                Instantiate(fishObject, pos, Quaternion.identity);
            }
        }
	}
}
