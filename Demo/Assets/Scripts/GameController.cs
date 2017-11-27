using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;
using Unnamed.UnnamedGame;

public class GameController : MonoBehaviour {
    public int offset,offset2;
    TextAsset songtiming;
    string[] timingarray;
    public List<PressState>[] tilearray;
    public int[] tilecount = new int[15];
    public Animator[] tiles = new Animator[15];
    public float starttime;
    public Text text;
	// Use this for initialization
	void Start () {
        tilearray = new List<PressState>[15];
        Input.multiTouchEnabled = true;
        songtiming = Resources.Load("Songs/audio", typeof(TextAsset)) as TextAsset;
        string text = songtiming.text;
        timingarray = text.Split('\n');
        for (int i = 0; i < 15; i++)
        {
            tilecount[i] = 0;
            tilearray[i] = new List<PressState>();
        }
        foreach (string timing in timingarray)
        {
            string[] temp = timing.Split(',');
            int position = Convert.ToInt32(temp[0]);
            tilearray[position].Add(new PressState(Convert.ToInt32(temp[2])));
        }
        GetComponent<AudioSource>().Play();
        starttime = Time.time * 1000;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        for (int i = 0; i < 4; i++)
        {
            if (Time.time * 1000 - starttime >= tilearray[i][tilecount[i]].duepressedtime - 250 + offset &&
                tilearray[i][tilecount[i]].isplayed == false)
            {
                tilearray[i][tilecount[i]].isexceeded = false;
                tiles[i].SetTrigger("marker");
                tilearray[i][tilecount[i]].isplayed = true;
            }
            if (Time.time * 1000 - starttime >= tilearray[i][tilecount[i]].duepressedtime + 250 + offset
                 && tilearray[i][tilecount[i]].isexceeded == false)
            {
                tilearray[i][tilecount[i]].isexceeded = true;
                tilecount[i]++;
            }
        }

        //if (Time.time * 1000 - starttime >= tilearray[0][tilecount[0]].duepressedtime - 250 + offset &&
        //    tilearray[0][tilecount[0]].isplayed == false)
        //{
        //    tiles[0].SetTrigger("marker");
        //    tilearray[0][tilecount[0]].isplayed = true;
        //}
        //if (Time.time * 1000 - starttime >= tilearray[0][tilecount[0]].duepressedtime + 200 + offset
        //     && tilearray[0][tilecount[0]].isexceeded == false)
        //{
        //    tilearray[0][tilecount[0]].isexceeded = true;
        //    tilecount[0]++;
        //}

        //if (Input.GetMouseButtonDown(0))
        //{
        //    Collider2D[] cols = Physics2D.OverlapPointAll(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        //    foreach (Collider2D col in cols)
        //    {
        //        string tag = col.gameObject.tag;
        //        Trigger(tag[4] - 49);
        //    }
        //}
        if (Input.touchCount >= 1)
        {
            for(int n = 0; n < Input.touchCount; n++) {
                if(Input.touches[n].phase == TouchPhase.Began)
                {
                    Collider2D col = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.touches[n].position));
                    string tag = col.gameObject.tag;
                    Trigger(tag[4] - 49);
                }

            }
        }
    }
    void Trigger(int i)
    {
        float delta = Math.Abs(Time.time * 1000 + offset2 - starttime
            - tilearray[i][tilecount[i]].duepressedtime);
        bool istouched = tilearray[i][tilecount[i]].istouched;
        if (delta <= 85f && istouched == false)
        {
            tiles[i].SetTrigger("perfect");
            tilearray[i][tilecount[i]].istouched = true;
        }
        if (delta >= 85f && delta <= 250f && istouched == false)
        {
            tiles[i].SetTrigger("cool");
            tilearray[i][tilecount[i]].istouched = true;
        }
    }
}
