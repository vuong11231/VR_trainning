using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EmptyView : BaseView
{
    [SerializeField] private TextMeshProUGUI txtQuestion;
    [SerializeField] private Image[] arrBoxAnswer = new Image[4];
    [SerializeField] private Button[] arrButtonAnswer = new Button[4];
    [SerializeField] private TextMeshProUGUI[] arrTextAnswer = new TextMeshProUGUI[4];
    [SerializeField] private Color incorrectColor;
    [SerializeField] private Color correctColor;
    private int indexSelected = 0;

    private void Start()
    {
        arrButtonAnswer[0].onClick.AddListener(delegate { OnClickedButtonAnswer("a", 0); });
        arrButtonAnswer[1].onClick.AddListener(delegate { OnClickedButtonAnswer("b", 1); });
        arrButtonAnswer[2].onClick.AddListener(delegate { OnClickedButtonAnswer("c", 2); });
        arrButtonAnswer[3].onClick.AddListener(delegate { OnClickedButtonAnswer("d", 3); });
    }

    public void OnInit(UnityEvent<string> OnClickedButton)
    {
        this.OnClickedAnswer = OnClickedButton;
    }

    public void OnClickedButtonAnswer(string answer, int index)
    {
        indexSelected = index;
        OnClickedAnswer?.Invoke(answer);
    }

    public void CheckTheSelectedResult(bool isCorrect)
    {
        arrButtonAnswer[indexSelected].interactable = false;
        if(isCorrect)
        {
            foreach(Button btn in arrButtonAnswer)
            {
                btn.interactable = false;
            }
        }    
        Color currentColor = isCorrect ? correctColor : incorrectColor;
        Color newColor = new Color(currentColor.r, currentColor.g, currentColor.b, 1.0f);
        arrBoxAnswer[indexSelected].color = newColor;
    }

    public override void Setup(ViewParam param)
    {
        for(int i = 0; i < arrBoxAnswer.Length; i++)
        {
            arrBoxAnswer[i].color = Color.white;
            arrButtonAnswer[i].interactable = true;
        }    
        txtQuestion.text = param.question.ToString();
        for (int i = 0; i < param.answer.Length; i++)
        {
            arrTextAnswer[i].text = param.answer[i].ToString();
        }
    }

    public override void OnHideView()
    {

    }
}
