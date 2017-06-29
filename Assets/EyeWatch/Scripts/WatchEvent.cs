using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WatchEvent : MonoBehaviour
{
    public UnityEvent onWatchEnter = new UnityEvent();
    public UnityEvent onWatchExit = new UnityEvent();
    public UnityEvent onWatchUpdate = new UnityEvent();
    public UnityEvent onWatch = new UnityEvent();
}