using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drops : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -2f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Revive()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -2f);
    }
}
