using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSlice : MonoBehaviour {

    //切り身
    [SerializeField]
    private GameObject FishFillets;

    //切った回数
    [SerializeField]
    private int sliceCount;


	// Use this for initialization
	void Start () {
       sliceCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter(Collider other)
    {
        //当たったものが包丁か
        if (other.gameObject.tag == "Kitchenknife")
        {
            sliceCount++;
            if (sliceCount >= 5)
            {
                //５回切ったとき魚を非アクティブ状態にする
                gameObject.SetActive(false);
                //切り身をアクティブ
                FishFillets.gameObject.SetActive(true);
            }
        }
    }

    public int GetSliceCount()
    {
        return sliceCount;
    }
}
