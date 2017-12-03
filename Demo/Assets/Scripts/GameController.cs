using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;
using Unnamed.UnnamedGame;

public class GameController : MonoBehaviour {
    public int offset,offset2,offset3;
    TextAsset songtiming;
    string[] timingarray;
    public List<PressState>[] tilearray;
    public List<PressState>[] droparray;
    public int[] tilecount = new int[15];
    public int[] tileall = new int[15];
    public int[] dropcount = new int[4];
    public int[] dropall = new int[4];
    public Queue<GameObject>[] drop = new Queue<GameObject>[4];
    public Animator[] tiles = new Animator[15];
    public Animator[] drops = new Animator[4];
    public float starttime;
    public Text text;
    ObjectPool pool;

    // Use this for initialization
    void Start () {
        pool = ObjectPool.GetInstance();
        tilearray = new List<PressState>[15];
        droparray = new List<PressState>[15];
        Input.multiTouchEnabled = true;
        for (int i = 0; i < 15; i++)
        {
            tilecount[i] = 0;
            tileall[i] = -1;
            tilearray[i] = new List<PressState>();
            if (i <= 3)
            {
                dropcount[i] = 0;
                dropall[i] = -1;
                droparray[i] = new List<PressState>();
                drop[i] = new Queue<GameObject>();
            }
        }


        songtiming = Resources.Load("Songs/pathetique", typeof(TextAsset)) as TextAsset;
        string text = songtiming.text;
        timingarray = text.Split('\n');
        foreach (string timing in timingarray)
        {
            string[] temp = timing.Split(',');
            int position = Convert.ToInt32(temp[0]);
            tilearray[position].Add(new PressState(Convert.ToInt32(temp[2])));
            tileall[position]++;
        }

        songtiming = Resources.Load("Songs/pathetiquedrop", typeof(TextAsset)) as TextAsset;
        text = songtiming.text;
        timingarray = text.Split('\n');
        foreach (string timing in timingarray)
        {
            string[] temp = timing.Split(',');
            int position = Convert.ToInt32(temp[0]);
            droparray[position].Add(new PressState(Convert.ToInt32(temp[2])));
            dropall[position]++;
        }
        GetComponent<AudioSource>().Play();
        starttime = Time.time * 1000;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        for (int i = 0; i < 15; i++)
        {
            PressState currentstate = tilearray[i][tilecount[i]];
            if (Time.time * 1000 - starttime >= currentstate.duepressedtime - 250 + offset &&
                currentstate.isplayed == false)
            {
                currentstate.isexceeded = false;
                tiles[i].SetTrigger("marker");
                tiles[convert(i)].SetTrigger("note");
                currentstate.isplayed = true;
                
            }
            if (Time.time * 1000 - starttime >= currentstate.duepressedtime + 250 + offset
                 && currentstate.isexceeded == false)
            {
                currentstate.isexceeded = true;
                if(tilecount[i] < tileall[i])
                    tilecount[i]++;
            }
            if(i <= 3)
            {
                PressState currentdropstate = droparray[i][dropcount[i]];
                if (Time.time * 1000 - starttime >= currentdropstate.duepressedtime - 700 + offset &&
                currentdropstate.isplayed == false)
                {
                    currentdropstate.isexceeded = false;
                    currentdropstate.isplayed = true;
                    drop[i].Enqueue(pool.Instantiate(new Vector3(-1.06f + 0.710f * i, 1.082f, -1f)));
                }

                if (Time.time * 1000 - starttime >= currentdropstate.duepressedtime + 250 + offset
                        && currentdropstate.isexceeded == false)
                {
                    currentdropstate.isexceeded = true;
                    if(dropcount[i] < dropall[i])
                        dropcount[i]++;
                }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D col = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (col != null)
            {
                string tag = col.gameObject.tag;
                Debug.Log(tag);
                Trigger(Convert.ToInt32(tag.Substring(4)));
            }
        }
        if (Input.touchCount >= 1)
        {
            for (int n = 0; n < Input.touchCount; n++)
            {
                if (Input.touches[n].phase == TouchPhase.Began)
                {

                    Collider2D col = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.touches[n].position));
                    string tag = col.gameObject.tag;
                    if (Convert.ToInt32(tag.Substring(4)) <= 15)
                    {
                        Trigger(Convert.ToInt32(tag.Substring(4)));
                    }

                }
                else if (Input.touches[n].phase == TouchPhase.Moved)
                {
                    Collider2D col = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.touches[n].position));
                    string tag = col.gameObject.tag;
                    if (Convert.ToInt32(tag.Substring(4)) >= 15)
                    {
                        Trigger(Convert.ToInt32(tag.Substring(4)));
                    }
                }

            }
        }
    }
    void Trigger(int i)
    {
        i--;
        if(i <= 14)
        {
            i = convert(i);
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
        else
        {
            float delta = Math.Abs(Time.time * 1000 + offset3 - starttime
            - droparray[i - 15][dropcount[i - 15]].duepressedtime);
            bool istouched = droparray[i - 15][dropcount[i - 15]].istouched;
            if (delta <= 85f && istouched == false)
            {
                drops[i - 15].SetTrigger("perfect");
                droparray[i - 15][dropcount[i - 15]].istouched = true;
                if (drop[i - 15] != null)
                    pool.Delete(drop[i - 15].Dequeue());
                //dropcount[i - 15]++;
            }
            if (delta > 85f && delta <= 150f && istouched == false)
            {
                drops[i - 15].SetTrigger("cool");
                droparray[i - 15][dropcount[i - 15]].istouched = true;
                if (drop[i - 15] != null)
                    pool.Delete(drop[i - 15].Dequeue());
                //dropcount[i - 15]++;
            }

        }
    }
    int convert(int i)
    {
        if (i <= 4)
            i += 5;
        else if (i > 4 && i <= 9)
            i -= 5;
        else if (i == 10 || i == 12)
            i += 1;
        else if (i == 11 || i == 13)
            i -= 1;
        return i;
    }
}