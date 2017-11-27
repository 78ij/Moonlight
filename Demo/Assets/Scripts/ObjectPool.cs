using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {
    public List<GameObject> pool = new List<GameObject>();
    public ObjectPool() { }
    private static ObjectPool instance;
    public static ObjectPool GetInstance()
    {
        if(instance == null)
        {
            instance = new GameObject("NotePool").
                AddComponent<ObjectPool>();
        }
        return instance;
    }
    public GameObject Instantiate(Vector3 position)
    {
        if (pool.Count == 0)
        {

        }
        else { }
    }
}


