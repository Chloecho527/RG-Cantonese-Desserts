using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class EventHandler
{
    ////// ��תCanvas���¼�
    //public static event Action<>

    // ���س����ҵ��¼�
    public static event Action BackToHomeEvent;
    public static void CallBackToHomeEvent()
    {
        BackToHomeEvent?.Invoke();
    }



}
