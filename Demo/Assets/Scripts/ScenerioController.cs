 using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unnamed.UnnamedGame;
public class ScenerioController : MonoBehaviour
{
    public Animator pagebreak;
    public Animator bgi;
    public SpriteRenderer bgirend;
    AnimatorStateInfo bgiinfo;
    public Text maintext;
    string[] texts;
    TextAsset scenario;
    bool isdisplaying;
    float lasttime;
    public float deltatime;
    string nextbgi;
    int textcount, currenttext,currenttextword,currenttextlength;
    // Use this for initialization
    void Start()
    {
        isdisplaying = true;
        scenario = Resources.Load("Scenario/1", typeof(TextAsset)) as TextAsset;
        string text = scenario.text;
        texts = text.Split('\n');
        textcount = texts.Length - 1;
        currenttext = 0;
        currenttextword = 0;
        currenttextlength = texts[currenttext].Length - 1;
        lasttime = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bgiinfo = bgi.GetCurrentAnimatorStateInfo(0);
        if (isdisplaying == true)
        {
            if (texts[currenttext][0] == '[')
            {
                bgi.SetTrigger("transition");
                nextbgi = texts[currenttext].Substring(1,
                    texts[currenttext].Length - 3);
                Debug.Log(nextbgi);
                Texture2D newbgi = Resources.Load("Images/bgimage/" + nextbgi, typeof(Texture2D)) as Texture2D;
                bgirend.sprite = Sprite.Create(newbgi, new Rect(0, 0, newbgi.width, newbgi.height),
                    new Vector2(0.5f, 0.5f)
                    );
                currenttext++;
                currenttextlength = texts[currenttext].Length - 1;
                
            }
            if(Time.time - lasttime >= deltatime)
            {
                if (currenttextword <= currenttextlength)
                {
                    maintext.text += texts[currenttext][currenttextword];
                    currenttextword++;
                }
                if (currenttextword > currenttextlength)
                {
                    isdisplaying = false;
                    pagebreak.SetTrigger("break");
                }
                lasttime = Time.time;
            }
        }
        if(isdisplaying == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                maintext.text = "";
                isdisplaying = true;
                currenttext++;
                currenttextword = 0;
                currenttextlength = texts[currenttext].Length - 1;
                pagebreak.SetTrigger("idle");

            }
        }
    }
}
