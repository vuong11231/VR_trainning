using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : BYSingletonMono<ConfigManager>
{
    [HideInInspector]
    public DefaultConfig configQuestion;
    public void InitConfig(Action callback)
    {
        StartCoroutine(LoadConfig(callback));
    }

    IEnumerator LoadConfig(Action callback)
    {
        configQuestion = Resources.Load("DataTable/DefaultConfig", typeof(ScriptableObject)) as DefaultConfig;
        yield return new WaitUntil(() => configQuestion != null);


        callback?.Invoke();

    }
}
