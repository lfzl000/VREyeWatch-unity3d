using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HighlightingSystem;
using UnityEngine.UI;

public class WatchController : MonoBehaviour
{
    public Camera eye;      //眼睛   VR摄像机
    public GameObject point;    //实例化的点
    public Transform pointDefault;  //默认点的位置
    public Color highLightColor;    //选中的物体高光的颜色
    private float distance;          //眼睛看的距离
    public LayerMask layerMask;     //屏蔽的层
    public float watchTime = 2;     //注视时间
    [HideInInspector]
    public Image markCircle;    //提示圈

    private void Start()
    {
        p = Instantiate(point, pointDefault.position, Quaternion.identity, pointDefault);
        distance = eye.farClipPlane;
        try
        {
            markCircle = p.transform.FindChild("Canvas/markCircle").GetComponent<Image>();
        }
        catch (System.Exception)
        {
        }
    }

    void Update()
    {
        EyeRay();
    }

    private RaycastHit hit;
    private GameObject p;
    private GameObject lastHit;
    private WatchGameobject lg;
    private void EyeRay()
    {
        if (Physics.Raycast(eye.transform.position, eye.transform.forward, out hit, distance, layerMask))
        {
            //Debug.DrawLine(eye.transform.position, hit.point, Color.red);
            if (p == null)
                p = Instantiate(point);
            p.SetActive(true);
            p.transform.parent = null;
            p.transform.localScale = Vector3.one;
            p.transform.LookAt(hit.point + hit.normal);
            p.transform.position = hit.point;
            Highlighter h = hit.transform.GetComponent<Highlighter>();
            if (h == null)
                h = hit.transform.gameObject.AddComponent<Highlighter>();
            h.On(highLightColor);
            lg = hit.transform.GetComponent<WatchGameobject>();
            if (hit.transform.gameObject == lastHit)
            {
                if (lg == null)
                    lg = hit.transform.gameObject.AddComponent<WatchGameobject>();
                lg.enabled = true;
            }
            else
            {
                if (lastHit)
                {
                    var lLG = lastHit.GetComponent<WatchGameobject>();
                    if (lLG != null)
                        lLG.enabled = false;
                }
            }
            lastHit = hit.transform.gameObject;
        }
        else
        {
            p.transform.parent = pointDefault;
            p.transform.localPosition = Vector3.zero;
            p.transform.localRotation = Quaternion.Euler(Vector3.zero);
            p.transform.localScale = Vector3.one;
            lastHit = null;
            if (lg)
                lg.enabled = false;
        }
    }

}