using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseView : MonoBehaviour
{
    public ViewIndex viewIndex;
    private BaseViewAnimation viewAnimation;
    private void Awake()
    {
        viewAnimation = gameObject.GetComponentInChildren<BaseViewAnimation>();
    }
    public virtual void Setup(ViewParam param)
    {

    }
    private void ShowView(ViewCallBack viewCallBack)
    {
        viewAnimation.OnShowView(()=> {
            OnShowView();
            viewCallBack.callBack?.Invoke();
        });
    }
    /// <summary>
    /// override in child
    /// </summary>
    public virtual void OnShowView() { }

    private void HideView(ViewCallBack viewCallBack)
    {
        viewAnimation.OnHideView(() => {
            OnHideView();
            viewCallBack.callBack?.Invoke();
        });
    }
    /// <summary>
    /// override in child
    /// </summary>
    public virtual void OnHideView(){}
}
