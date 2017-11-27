using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {
    public List<GameObject> pool = new List<GameObject>();
    public List<Transform> transforms = new List<Transform>();
    public List<Rigidbody2D> bodies = new List<Rigidbody2D>();
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
            GameObject temp = pool[0];
            Transform temp1 = transforms[0];
            Rigidbody2D body = bodies[0];
            temp.SetActive(true);
            //temp.SendMessage("Revive", null, SendMessageOptions.DontRequireReceiver);
            temp1.position = position;
            body.velocity = new Vector2(0f, -4f);
            pool.Remove(temp);
            transforms.Remove(temp1);
            bodies.Remove(body);

            return temp;
        }
    }
    public void Delete(GameObject note)
    {
        pool.Add(note);
        transforms.Add(note.GetComponent<Transform>());
        bodies.Add(note.GetComponent<Rigidbody2D>());
        note.SetActive(false);

    }
    void Start()
    {
        for(int i = 0;i < 6; i++)
        {
            GameObject temp = Instantiate(Resources.Load("Prefabs/drop", typeof(GameObject)) as GameObject,
                new Vector3(0,0,0),
                Quaternion.Euler(new Vector3(0, 0, 0)));
            Delete(temp);
        }
    }
}


