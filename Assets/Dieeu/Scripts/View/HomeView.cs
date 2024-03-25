using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Events;
public class HomeView : BaseView
{
    public void ButtonStart()
    {
        ViewManager.instance.NextQuestion();
    }
        

    public override void Setup(ViewParam param)
    {
        
    }

    public override void OnHideView()
    {

    }
}
