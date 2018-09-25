using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFireWorks : MonoBehaviour {

    private GameObject go;
    private GameObject go2;
	// Use this for initialization
	void Start () {
        go = Resources.Load<GameObject>("Prefabs/Seed");
	}
	
	// Update is called once per frame
	void Update () {
        //デリート
        Destroy(go2, 4);

	}

	private void OnTriggerEnter(Collider other) {
		if(other.tag == "sushi") {
			go2 = Instantiate(go, go.transform.position, Quaternion.Euler(270,0,0));
		}
	}

}
