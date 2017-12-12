 using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unnamed.Moonlight;
public class ScenerioController : MonoBehaviour
{
    Dictionary<int, string> mapping;
    public Animator pagebreak;
    public Animator bgi;
    public Animator transition;
    public SpriteRenderer bgirend;
    AnimatorStateInfo bgiinfo;
    public Text maintext;
    string[] texts;
    TextAsset scenario;
    bool isdisplaying;
    float lasttime;
    public float deltatime;
    string nextbgi;
    public int textcount;
    public int currenttext;
    int currenttextword,currenttextlength;
    // Use this for initialization
    void Start()
    {
        transition.updateMode = AnimatorUpdateMode.UnscaledTime;
        mapping = new Dictionary<int, string>();
        StartCoroutine(Fadein());
        mapping.Add(0, "pathetique");
        isdisplaying = true;
        scenario = Resources.Load("Scenario/0", typeof(TextAsset)) as TextAsset;
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
                if (currenttext++ >= textcount - 1 )
                {
                    SceneManager.LoadScene(0);
                }
                else {
                    maintext.text = "";
                    isdisplaying = true;
                    currenttextword = 0;
                    currenttextlength = texts[currenttext].Length - 1;
                    pagebreak.SetTrigger("idle");
                }
            }
        }
    }
    IEnumerator Fadein()
    {
        transition.SetTrigger("fadein");
        float start = Time.realtimeSinceStartup;
        Time.timeScale = 0;
        while (Time.realtimeSinceStartup < start + 1.8f)
        {
            yield return null;
        }
        Time.timeScale = 1;
    }
}
