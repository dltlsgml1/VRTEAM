using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSlice : MonoBehaviour
{

    //切り身
    //[SerializeField]
    //private GameObject neta;

    //切った回数
    [SerializeField]
    private int sliceCount;

    //GameObject Obj;

    // Use this for initialization
    void Start()
    {
        sliceCount = 0;
        // neta = (GameObject)Resources.Load("Prefabs/neta");
        // Vector3 pos = gameObject.transform.position;
        // pos.y += 1;
        // Obj = Instantiate(neta, pos, Quaternion.identity);
        // Obj.gameObject.SetActive(false);
        // Obj.gameObject.transform.Rotate(new Vector3(-90, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            //５回切ったとき魚を非アクティブ状態にする
            Destroy(gameObject);
            //切り身をアクティブ
            // Obj.gameObject.SetActive(true);
        }
    }


    private void OnTriggerEnter(Collider other)
    {


        //当たったものが包丁か
        if (other.gameObject.tag == "knife")
        {
            sliceCount++;
            if (sliceCount >= 5)
            {
                //５回切ったとき魚を非アクティブ状態にする
                Destroy(gameObject);
                //切り身をアクティブ
                // Obj.gameObject.SetActive(true);
            }
        }
    }

    public int GetSliceCount()
    {
        return sliceCount;
    }


}
