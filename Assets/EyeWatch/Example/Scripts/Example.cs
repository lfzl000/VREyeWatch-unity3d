using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour
{

    public GameObject target;
    private Material t_M;

    void Start()
    {
        t_M = target.GetComponent<Renderer>().material;
    }

    void Update()
    {

    }

    public void WatchEnter()
    {
        Debug.Log("视点进入");
        t_M.color = Color.red;
    }

    public void WatchExit()
    {
        Debug.Log("视点移出");
    }

    public void WatchUpdate()
    {
        Debug.Log("视点在物体上");
    }

    public void WatchOn()
    {
        Debug.Log("注视事件触发了");
        t_M.color = Color.blue;
    }
}