using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {
    ObjectPool pool;
	// Use this for initialization
	void Start () {
        pool = ObjectPool.GetInstance();

	}
	void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("aaa");
        if (other.gameObject.tag == "drop")
        {
            pool.Delete(other.gameObject);
        }
    }
	// Update is called once per frame

    void Update () {
		
	}
}
