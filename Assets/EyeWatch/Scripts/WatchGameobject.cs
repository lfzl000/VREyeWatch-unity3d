using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchGameobject : MonoBehaviour
{
    TimerTool t;

    void OnEnable()
    {
        t = TimerTool.CreateTimer();
        t.StartTiming(2, StartTimer, EndTimer);
    }

    public void StartTimer()
    {
        Debug.Log("Start");
    }

    public void EndTimer()
    {
        Debug.Log("点击" + gameObject.name);
    }

    private void OnDisable()
    {
        if (t)
        {
            t.PauseTimer();
            t.Destory();
        }
    }


}