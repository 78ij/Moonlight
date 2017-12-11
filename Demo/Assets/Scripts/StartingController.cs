using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartingController : MonoBehaviour {
    bool isstarting;
    public Animator transition;
    AnimatorStateInfo transinfo;
	// Use this for initialization
	void Start () {
        isstarting = false;
        transinfo = transition.GetCurrentAnimatorStateInfo(0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnStartButtonClick()
    {
        if (!isstarting)
        {
            isstarting = true;
            StartCoroutine(SwitchScene());
        }
    }
    IEnumerator SwitchScene()
    {
        transition.SetTrigger("transition");
        yield return new WaitForSeconds(2.5f);
        PlayerPrefs.SetInt("NextScene", 3);
        PlayerPrefs.SetInt("NextMusic", 0);
        SceneManager.LoadScene(2);
    }
}
