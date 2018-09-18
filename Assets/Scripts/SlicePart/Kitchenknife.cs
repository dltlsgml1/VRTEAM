using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Kitchenknife : MonoBehaviour
{

    [SerializeField]
    private float SliceLowestDistance = 3.0f;   //最低でも包丁を振らないといけない距離

    private Vector3 OldPos; //前の座標
    FishSlice scFishSlice;

    //初期処理
    void Start()
    {
        //現在の位置を保持
        OldPos = transform.position;

    }

    //更新処理
    void Update()
    {
        /*
        if (IsHit)
        {
            //現在のy座標が１フレーム前よりも下なら
            if (transform.position.y < OldPos.y)
            {
                //包丁を振った距離を計算
                float MoveDistance = OldPos.y - transform.position.y;

                if (MoveDistance >= SliceLowestDistance)
                {
                    //当たったオブジェクトを非アクティブ
                    HitObject.gameObject.SetActive(false);
                    //別のオブジェクトを表示
                }
            }
        }
        */



    }

    //当たり判定
    private void OnTriggerEnter(Collider other)
    {

        //当たったものが包丁か
        if (other.gameObject.tag == "Fish")
        {
            scFishSlice = other.gameObject.GetComponent<FishSlice>();
            
        }
    }



}
