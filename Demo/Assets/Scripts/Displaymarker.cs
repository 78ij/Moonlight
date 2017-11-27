using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unnamed.UnnamedGame;
public class Displaymarker : MonoBehaviour {
    public GameObject gamecontrol;
    GameController gamecontroller;
    public List<PressState> tilestate;
    int[] tilecount;
    public int tilenumber;
    float starttime;
    int offset;
    Animator animator;
    // Use this for initialization
    void Start () {
        gamecontroller = gamecontrol.GetComponent<GameController>();
        tilecount = gamecontroller.tilecount;
        tilestate = gamecontroller.tilearray[tilenumber];
        starttime = gamecontroller.starttime;
        offset = gamecontroller.offset;
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        int i = tilenumber;
        if (Time.time * 1000 - starttime >= tilestate[tilecount[i]].duepressedtime - 250 + offset &&
            tilestate[tilecount[i]].isplayed == false)
        {
            tilestate[tilecount[i]].isexceeded = false;
            animator.SetTrigger("marker");
            tilestate[tilecount[i]].isplayed = true;
            Debug.Log(tilenumber);
        }
        if (Time.time * 1000 - starttime >= tilestate[tilecount[i]].duepressedtime + 300 + offset
             && tilestate[tilecount[i]].isexceeded == false)
        {
            tilestate[tilecount[i]].isexceeded = true;
            tilecount[i]++;
        }
    }
}
