﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing : MonoBehaviour
{
    [SerializeField]
    private GameObject Camerarig;
    private Vector3 Pos;
    private GameObject[] CoolerBox;
    private bool NowFishing = false;
    private bool FishingDone = false;
    private int FishingTimer = 0;


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
            StartCoroutine(SetFishingTimer());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (this.gameObject.tag == "FishingZone")
        {
            if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                StartCoroutine(SetFishingTimer());
            }
        }
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
                FishingTimer = 0;
                FishingDone = true;
                break;
            }
        }
        yield return null;

    }
}
