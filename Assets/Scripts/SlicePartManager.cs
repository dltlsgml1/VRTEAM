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
    private GameObject fishingRod;

    private Fishing fishingSc;

    private GameObject fishObject;

    [SerializeField]
    private GameObject Table;

    // Use this for initialization
    void Start () {
        //スクリプト取得
        fishingSc = fishingRod.GetComponent<Fishing>();
        //プレハブをロード
        fishObject = (GameObject)Resources.Load("Prefabs/Fish");
	}
	
	// Update is called once per frame
	void Update ()
    {
        //魚を出す
        //if(Input.GetKeyDown(KeyCode.A))
        if(Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            //if (fishingSc.GetCoolerBoxInFishNum() > 0)
            //{
                fishingSc.takeoutFish();// 魚の所持数を減らす
                //魚を実体化
                Vector3 pos = Table.gameObject.transform.position;
                pos.x += 0.7f;
                pos.y += 2.5f;
                pos.z += 0.6f;
                GameObject obj= Instantiate(fishObject, pos, Quaternion.identity);
           //  }
        }
	}
}
