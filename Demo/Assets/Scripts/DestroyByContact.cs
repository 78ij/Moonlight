using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Unnamed.Moonlight;
public class DestroyByContact : MonoBehaviour {
    ObjectPool pool;
    GameController controller;
    public List<PressState>[] droparray;
    public int[] dropcount;
    public int destroynumber;
    public Queue<GameObject>[] drop;
    // Use this for initialization
    void Start () {
        pool = ObjectPool.GetInstance();
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        droparray = controller.droparray;
        dropcount = controller.dropcount;
        destroynumber = Convert.ToInt32(tag.Substring(4));
    }
	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "drop")
        {
            //droparray[destroynumber - 16][dropcount[destroynumber - 16]].istouched = true;
            pool.Delete(other.gameObject);
            drop[destroynumber - 16].Dequeue();
        }
    }
	// Update is called once per frame

    void Update () {
        if(droparray == null)
            droparray = controller.droparray;
        if (drop == null)
            drop = controller.drop;
    }
}
