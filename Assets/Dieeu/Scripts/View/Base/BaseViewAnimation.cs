using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class BaseViewAnimation : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private void Awake()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
    }
    // Start is called before the first frame update
    public virtual void OnShowView(Action callBack)
    {
        canvasGroup.DOFade(1, 1.5f).OnComplete(() =>
        {

            callBack?.Invoke();
        });
        
    }
    public virtual void OnHideView(Action callBack)
    {
        canvasGroup.DOFade(0, 0.5f).OnComplete(() =>
        {

            callBack?.Invoke();
        });
      
    }
}
