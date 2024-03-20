using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Events;
public class HomeView : BaseView
{
    public void ButtonStart()
    {
        OnClickedStart?.Invoke();
    }
        

    public override void Setup(ViewParam param)
    {
        
    }

    public override void OnHideView()
    {

    }
}
