using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostWwiseEvent : MonoBehaviour
{
    public AK.Wwise.Event MyEvent;
    //Use this for initialization.
    void Start ()
    {
        MyEvent.Post(gameObject);
    }

    //Update is called once per frame.
    void Update()
    {

    }
}