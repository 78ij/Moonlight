using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {
    public Stack<GameObject> pool = new Stack<GameObject>();
    public Stack<Transform> transforms = new Stack<Transform>();
    public Stack<Rigidbody2D> bodies = new Stack<Rigidbody2D>();
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
            return Instantiate(Resources.Load("Prefabs/drop", typeof(GameObject)) as GameObject,
                position,
                Quaternion.Euler(new Vector3(0, 0, 0)));
        }
        else {
            GameObject temp = pool.Pop();
            Transform temp1 = transforms.Pop();
            Rigidbody2D body = bodies.Pop();
            temp.SetActive(true);
            //temp.SendMessage("Revive", null, SendMessageOptions.DontRequireReceiver);
            temp1.position = position;
            body.velocity = new Vector2(0f, -2f);
            return temp;
        } 
    }
    public void Delete(GameObject note)
    {
        //Debug.Log("delete!");
        pool.Push(note);
        transforms.Push(note.GetComponent<Transform>());
        bodies.Push(note.GetComponent<Rigidbody2D>());
        note.SetActive(false);

    }
    void Start()
    {
        for(int i = 0;i < 8; i++)
        {
            GameObject temp = Instantiate(Resources.Load("Prefabs/drop", typeof(GameObject)) as GameObject,
                new Vector3(0,0,0),
                Quaternion.Euler(new Vector3(0, 0, 0)));
            Delete(temp);
        }
    }
}


