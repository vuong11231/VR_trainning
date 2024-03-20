using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject anchorView;
    private List<DefaultConfigRecord> m_ListQuestions = new();
    [SerializeField] private EmptyView emptyView;
    private ViewParam m_ViewParam;
    private int numberQuestion = 0;

    private void Awake()
    {
        ConfigManager.instance.InitConfig(null);
    }

    private void Start()
    {
        m_ListQuestions = ConfigManager.instance.configQuestion.records;
        m_ListQuestions = m_ListQuestions.OrderBy(x => Guid.NewGuid()).ToList();
        PassEvent();
    }

    private void PassEvent()
    {
        //Home
        UnityEvent eStart = new UnityEvent();
        eStart.AddListener(ShowParam);
        ViewManager.instance.GetView(ViewIndex.HomeView).OnClickedStart = eStart;

        //Empty
        UnityEvent<string> eAnswer = new UnityEvent<string>();
        eAnswer.AddListener(CheckCorrectAnswer);
        ViewManager.instance.GetView(ViewIndex.EmptyView).OnClickedAnswer = eAnswer;
        BaseView baseView = ViewManager.instance.GetView(ViewIndex.EmptyView);
        if (baseView is EmptyView)
            emptyView = baseView as EmptyView;

    }

    private void ShowParam()
    {
        if(numberQuestion < m_ListQuestions.Count - 1)
        {
            m_ViewParam = new();
            m_ViewParam.id = m_ListQuestions[numberQuestion].id;
            m_ViewParam.question = m_ListQuestions[numberQuestion].question;
            string[] arrAnswers = { m_ListQuestions[numberQuestion].answerA,
                                    m_ListQuestions[numberQuestion].answerB,
                                    m_ListQuestions[numberQuestion].answerC,
                                    m_ListQuestions[numberQuestion].answerD };
            m_ViewParam.answer = arrAnswers;
            ViewManager.instance.SwitchView(ViewIndex.EmptyView, m_ViewParam, null);
        } else
        {
            Debug.Log("Stop");
        }
    }

    public void CheckCorrectAnswer(string answer)
    {
        if (answer == m_ListQuestions[numberQuestion].correct)
        {
            numberQuestion++;
            emptyView.CheckTheSelectedResult(true);
            Invoke(nameof(ShowParam), .1f);
        }
        else
        {
            emptyView.CheckTheSelectedResult(false);
        }
    }
}
