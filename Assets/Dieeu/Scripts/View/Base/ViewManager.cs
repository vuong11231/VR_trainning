using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewManager : BYSingletonMono<ViewManager>
{
    [SerializeField] Transform anchorView;
    private Dictionary<ViewIndex, BaseView> dicView = new Dictionary<ViewIndex, BaseView>();
    [NonSerialized]
    public BaseView currentView = null;
    // Start is called before the first frame update
  

    void Awake()
    {
        foreach (ViewIndex viewIndex in ViewConfig.viewIndices)
        {
            //1. create view
            string nameView = viewIndex.ToString();
            GameObject viewObject = Instantiate(Resources.Load("View/" + nameView, typeof(GameObject))) as GameObject;
            viewObject.transform.SetParent(anchorView, false);
            BaseView baseView = viewObject.GetComponent<BaseView>();
            dicView.Add(viewIndex, baseView);
            viewObject.SetActive(false);
            //// 2. switch empty view
            //SwitchView(ViewIndex.HomeView);
        }
        // 2. switch empty view
        SwitchView(ViewIndex.HomeView);
    }

    public BaseView GetView(ViewIndex viewIndex)
    {
        return dicView[viewIndex];
    }
    public BaseView GetCurrentView()
    {
        return currentView;
    }
    // Update is called once per frame
    public void SwitchView(ViewIndex viewIndex,ViewParam viewParam=null,Action callback=null)
    {
        if(currentView!=null)
        {
            //
            ViewCallBack viewCallBack = new ViewCallBack();
            viewCallBack.callBack = () =>
                                    {
                                        currentView.gameObject.SetActive(false);
                                        OnShowNextView(viewIndex, viewParam, callback);
                                    };

            currentView.SendMessage("HideView", viewCallBack); 
        }
        else
        {
            OnShowNextView(viewIndex, viewParam, callback);
        }
    }
   
    private void OnShowNextView(ViewIndex viewIndex, ViewParam viewParam = null, Action callback = null)
    {
        currentView = dicView[viewIndex];
        currentView.gameObject.SetActive(true);
        currentView.Setup(viewParam);
        ViewCallBack viewCallBack = new ViewCallBack();
        viewCallBack.callBack = callback;
        currentView.SendMessage("ShowView", viewCallBack);
    }
   
}

public class ViewCallBack
{
    public Action callBack;
}