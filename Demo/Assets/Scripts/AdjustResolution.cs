using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustResolution : MonoBehaviour {
    public Camera maincamera;
	void Start () {
        maincamera.aspect = 1.78f;
	}
}
