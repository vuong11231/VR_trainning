using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : BYSingletonMono<ConfigManager>
{
    [HideInInspector]
    public PCCC configQuestion;
    public void InitConfig(Action callback)
    {
        StartCoroutine(LoadConfig(callback));
    }

    IEnumerator LoadConfig(Action callback)
    {
        configQuestion = Resources.Load("DataTable/PCCC", typeof(ScriptableObject)) as PCCC;
        yield return new WaitUntil(() => configQuestion != null);


        callback?.Invoke();

    }
}
