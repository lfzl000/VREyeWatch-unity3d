using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchGameobject : MonoBehaviour
{
    private TimerTool t;
    private WatchEvent w;
    private WatchController wc;

    private void Awake()
    {
        w = GetComponent<WatchEvent>();
        wc = FindObjectOfType<WatchController>();
    }

    private void Update()
    {
        if (w)
            w.onWatchUpdate.Invoke();
    }

    private void OnEnable()
    {
        t = TimerTool.CreateTimer();
        t.StartTiming(wc.watchTime, StartTimer, EndTimer, MarkCircle);
    }

    public void StartTimer()
    {
        if (w)
            w.onWatchEnter.Invoke();
    }

    public void EndTimer()
    {
        if (w)
            w.onWatch.Invoke();
    }

    private void OnDisable()
    {
        if (w)
            w.onWatchExit.Invoke();
        if (t)
        {
            t.PauseTimer();
            t.Destory();
        }
        if (wc.markCircle)
            wc.markCircle.fillAmount = 0;
    }

    private void MarkCircle(float t)
    {
        if (wc.markCircle)
            wc.markCircle.fillAmount = t;
    }
}