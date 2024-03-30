using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class ViewManager : BYSingletonMono<ViewManager>
{
    [SerializeField] Transform anchorView;
    private Dictionary<ViewIndex, BaseView> dicView = new Dictionary<ViewIndex, BaseView>();
    [NonSerialized]
    public BaseView currentView = null;

    private List<PCCCRecord> m_ListQuestions = new();
    private ViewParam m_ViewParam;
    private int numberQuestion = 0;

    void Awake()
    {
        ConfigManager.instance.InitConfig(null);
        //Shuffle list questions
        m_ListQuestions = ConfigManager.instance.configQuestion.records;
        m_ListQuestions = m_ListQuestions.OrderBy(x => Guid.NewGuid()).ToList();
        LoadView();
    }

    private void LoadView()
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
    public void SwitchView(ViewIndex viewIndex, ViewParam viewParam = null, Action callback = null)
    {
        if (currentView != null)
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

    public void NextQuestion()
    {
        if (numberQuestion < m_ListQuestions.Count - 1)
        {
            m_ViewParam = new();
            m_ViewParam.id = m_ListQuestions[numberQuestion].id;
            m_ViewParam.number = numberQuestion + 1;
            m_ViewParam.totalQuestion = m_ListQuestions.Count;
            m_ViewParam.question = m_ListQuestions[numberQuestion].question;
            string[] arrAnswers = { m_ListQuestions[numberQuestion].answerA,
                                    m_ListQuestions[numberQuestion].answerB,
                                    m_ListQuestions[numberQuestion].answerC,
                                    m_ListQuestions[numberQuestion].answerD };
            m_ViewParam.answer = arrAnswers;
            //Image signs
            m_ViewParam.typeSigns = ShowImageSigns();

            SwitchView(ViewIndex.QuestionsView, m_ViewParam, null);
        }
        else
        {
            Debug.Log("Stop");
        }
    }

    private TypeSigns[] ShowImageSigns()
    {
        string signsString = m_ListQuestions[numberQuestion].typeSigns;
        string[] arrSigns = signsString != "null" ? signsString.Split(',') : null;
        if (arrSigns != null)
        {
            TypeSigns[] typeSigns = new TypeSigns[4];
            TypeSigns typeSign;
            for (int i = 0; i < arrSigns.Length; i++)
            {
                TypeSigns.TryParse(arrSigns[i], out typeSign);
                typeSigns[i] = typeSign;
            }
            return typeSigns;
        }
        else
            return null;
    }

    public bool CheckCorrectAnswer(string answer)
    {
        if (answer == m_ListQuestions[numberQuestion].correct)
        {
            numberQuestion++;
            return true;
        }
        else
            return false;
    }
}

public class ViewCallBack
{
    public Action callBack;
}