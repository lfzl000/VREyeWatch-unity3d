using UnityEngine;

public delegate void CompleteEvent();
public delegate void UpdateEvent(float t);

/// <summary>
/// 计时器工具类
/// </summary>
public class TimerTool : MonoBehaviour
{
    bool isLog = true;

    UpdateEvent updateEvent;

    CompleteEvent onEndCompleted;
    CompleteEvent onStartCompleted;

    float timeTarget;   // 计时时间/  
    float timeStart;    // 开始计时时间/  
    float timeNow;     // 现在时间/  
    float offsetTime;   // 计时偏差/  
    bool isTimer;       // 是否开始计时/  
    bool isDestory = true;     // 计时结束后是否销毁/  
    bool isEnd;         // 计时是否结束/  
    bool isIgnoreTimeScale = true;  // 是否忽略时间速率  
    bool isRepeate;

    float Time_
    {
        get { return isIgnoreTimeScale ? Time.realtimeSinceStartup : Time.time; }
    }
    float now;

    void Update()
    {
        if (isTimer)
        {
            timeNow = Time_ - offsetTime;
            now = timeNow - timeStart;
            if (updateEvent != null)
                updateEvent(Mathf.Clamp01(now / timeTarget));
            if (now > timeTarget)
            {
                if (onEndCompleted != null)
                    onEndCompleted();
                if (!isRepeate)
                    Destory();
                else
                    ReStartTimer();
            }
        }
    }

    /// <summary>
    /// 获取剩余时间
    /// </summary>
    /// <returns>剩余时间</returns>
    public float GetLeftTime()
    {
        return Mathf.Clamp(timeTarget - now, 0, timeTarget);
    }

    void OnApplicationPause(bool isPause_)
    {
        if (isPause_)
        {
            PauseTimer();
        }
        else
        {
            ConnitueTimer();
        }
    }

    /// <summary>  
    /// 计时结束  
    /// </summary>  
    public void Destory()
    {
        isTimer = false;
        isEnd = true;
        if (isDestory)
            Destroy(gameObject);
    }

    float _pauseTime;
    /// <summary>  
    /// 暂停计时  
    /// </summary>  
    public void PauseTimer()
    {
        if (isEnd)
        {
            if (isLog) Debug.LogWarning("计时已经结束！");
        }
        else
        {
            if (isTimer)
            {
                isTimer = false;
                _pauseTime = Time_;
                //Debug.Log("暂停计时！");
            }
        }
    }

    /// <summary>  
    /// 继续计时  
    /// </summary>  
    public void ConnitueTimer()
    {
        if (isEnd)
        {
            if (isLog) Debug.LogWarning("计时已经结束！请从新计时！");
        }
        else
        {
            if (!isTimer)
            {
                offsetTime += (Time_ - _pauseTime);
                isTimer = true;
                Debug.Log("继续计时！");
            }
        }
    }

    /// <summary>
    /// 重新开始当前计时
    /// </summary>
    public void ReStartTimer()
    {
        timeStart = Time_;
        offsetTime = 0;
    }

    /// <summary>
    /// 改变计时目标时间
    /// </summary>
    /// <param name="time_">目标时间</param>
    public void ChangeTargetTime(float time_)
    {
        timeTarget += time_;
    }

    /// <summary>
    /// 开始计时
    /// </summary>
    /// <param name="time_">计时时长</param>
    /// <param name="onStartCompleted_">开始计时调用方法</param>
    /// <param name="onEndCompleted_">结束计时调用方法</param>
    /// <param name="update">计时线程方法</param>
    /// <param name="isIgnoreTimeScale_">是否忽略时间速率</param>
    /// <param name="isRepeate_"></param>
    /// <param name="isDestory_"></param>
    public void StartTiming(float time_, CompleteEvent onStartCompleted_, CompleteEvent onEndCompleted_, UpdateEvent update = null, bool isIgnoreTimeScale_ = true, bool isRepeate_ = false, bool isDestory_ = true)
    {
        timeTarget = time_;
        if (onEndCompleted_ != null)
            onEndCompleted = onEndCompleted_;
        if (onStartCompleted_ != null)
            onStartCompleted = onStartCompleted_;
        if (update != null)
            updateEvent = update;
        isDestory = isDestory_;
        isIgnoreTimeScale = isIgnoreTimeScale_;
        isRepeate = isRepeate_;

        if (onStartCompleted != null)
            onStartCompleted();
        timeStart = Time_;
        offsetTime = 0;
        isEnd = false;
        isTimer = true;

    }

    /// <summary>
    /// 创建计时器
    /// </summary>
    /// <param name="gobjName">计时器名称</param>
    /// <returns>Timer</returns>
    public static TimerTool CreateTimer(string gobjName = "Timer")
    {
        GameObject g = new GameObject(gobjName);
        TimerTool timer = g.AddComponent<TimerTool>();
        return timer;
    }

    /// <summary>
    /// 获取时间格式字符串，显示mm:ss
    /// </summary>
    /// <returns>The minute time.</returns>
    /// <param name="time">Time.</param>
    public static string GetMinuteTime(float time)
    {
        int mm, ss;
        string stime = "0:00";
        if (time <= 0) return stime;
        mm = (int)time / 60;
        ss = (int)time % 60;
        if (mm > 60)
            stime = "59:59";
        else if (mm < 10 && ss >= 10)
            stime = "0" + mm + ":" + ss;
        else if (mm < 10 && ss < 10)
            stime = "0" + mm + ":0" + ss;
        else if (mm >= 10 && ss < 10)
            stime = mm + ":0" + ss;
        else
            stime = mm + ":" + ss;
        return stime;
    }

    /// <summary>
    /// 获取当前时间
    /// </summary>
    /// <returns></returns>
    public static string GetNowDateTime()
    {
        string nowTime;
        System.DateTime now = System.DateTime.Now;
        nowTime = now.ToString().Replace('/', '-').Replace(':', '-');
        return nowTime;
    }
}