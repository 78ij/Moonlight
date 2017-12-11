using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadingController : MonoBehaviour {
    public int nextScene;
    AsyncOperation async;
    // Use this for initialization
    void Start () {
        nextScene = PlayerPrefs.GetInt("NextScene");
        StartCoroutine(NextScene());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(2.0f);
        async = SceneManager.LoadSceneAsync(nextScene);
        yield return null;
    }
}
