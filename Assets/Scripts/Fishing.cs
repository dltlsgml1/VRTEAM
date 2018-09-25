using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing : MonoBehaviour
{
    [SerializeField]
    private GameObject CountDown;
    [SerializeField]
    private GameObject[] FishPool = new GameObject[5];
    private Vector3 Pos;
    private List<GameObject> CoolerBox = new List<GameObject>();
    private bool NowFishing = false;
    private bool FishingDone = false;
    private int FishingTimer = 0;
    private int fishCount = 0;

    private SteamVR_TrackedObject m_trackedObj;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)m_trackedObj.index); }
        
    }


    IEnumerator coroutine;
    // Use this for initialization
    void Start()
    {
        m_trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (NowFishing == false)
            {
                StartCoroutine(SetFishingTimer());
                CountDown.SetActive(true);
                NowFishing = true;
            }
        }
        if(FishingDone)
        {
            FishingDone = false;
            NowFishing = false;
            CoolerBox.Add(FishPool[0]);
        }


            if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                if (NowFishing == false)
                {
                    StartCoroutine(SetFishingTimer());
                    CountDown.SetActive(true);
                    NowFishing = true;
                }

            }

    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
    public int GetCoolerBoxInFishNum()
    {
        return CoolerBox.Count;
    }
    //魚を取り出す
    public bool takeoutFish()
    {
        if (GetCoolerBoxInFishNum() == 0)
        {
            return false;
        }

        CoolerBox.RemoveAt(0);
        return true;
    }
    IEnumerator SetFishingTimer()
    {
        while (true)
        {
            if (FishingTimer < 10)
            {
                FishingTimer++;
                yield return new WaitForSeconds(1.0f);
            }
            else
            {
                CountDown.SetActive(false);
                FishingTimer = 0;
                FishingDone = true;
                break;
            }
        }
        yield return null;

    }
}
