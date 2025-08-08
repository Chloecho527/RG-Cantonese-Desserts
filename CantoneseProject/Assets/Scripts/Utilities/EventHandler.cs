using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class EventHandler
{
    ////// 跳转Canvas的事件
    //public static event Action<>

    // 返回场景家的事件
    public static event Action BackToHomeEvent;
    public static void CallBackToHomeEvent()
    {
        BackToHomeEvent?.Invoke();
    }



}
