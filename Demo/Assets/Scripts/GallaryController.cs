using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GallaryController : MonoBehaviour
{
    public Canvas canvas;
    public Image panel;
    public Scrollbar scrollbar;
    public Dictionary<string, int> isunlocked;
    List<Button> images;
    int allwidth;
    // Use this for initialization
    void Start()
    {
        images = new List<Button>();
        isunlocked = new Dictionary<string, int>();
        isunlocked.Add("bgi01", 1);
        isunlocked.Add("bgi02", 1);
        isunlocked.Add("bgi03", 1);
        isunlocked.Add("bgi04", 1);
        isunlocked.Add("bgi05", 1);
        isunlocked.Add("bgi06", 1);
        isunlocked.Add("bgi07", 1);
        isunlocked.Add("bgi08", 1);
        for (int i = 0; i < isunlocked.Count; i++)
        {
            GameObject image = Instantiate(Resources.Load("Prefabs/image",
                typeof(GameObject)) as GameObject,
                new Vector3(0,0,0),
                Quaternion.Euler(new Vector3(0, 0, 0)),
                panel.rectTransform);
            image.GetComponent<RectTransform>().anchoredPosition = 
                new Vector3((int)(i / 2) * 801.3f / 2, (int)(i % 2) * -423.9f / 2, 0);
            Texture2D newbg = Resources.Load("Images/bgimage/bgi0" + Convert.ToString(i + 1), typeof(Texture2D)) as Texture2D;
            image.GetComponent<Image>().sprite = Sprite.Create(newbg, new Rect(0, 0, newbg.width, newbg.height),
                    new Vector2(0.5f, 0.5f)
                    );
        }
        allwidth = isunlocked.Count / 2 + isunlocked.Count % 2;
        Debug.Log(allwidth);
        panel.rectTransform.sizeDelta =
            new Vector2(allwidth <= 2 ? 801.3f : 801.3f / 2 * allwidth,423.9f);  
    }

    // Update is called once per frame
    void Update()
    {



    }
}
