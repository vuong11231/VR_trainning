using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum TypeSigns
{
    Red,
    Green,
    Blue,
    Notice,
    Ban,
    NoSmoke,
}

public class QuestionsView : BaseView
{
    [SerializeField] private TextMeshProUGUI txtNumber;
    [SerializeField] private TextMeshProUGUI txtQuestion;
    [SerializeField] private Image[] arrBoxAnswer = new Image[4];
    [SerializeField] private Button[] arrButtonAnswer = new Button[4];
    [SerializeField] private TextMeshProUGUI[] arrTextAnswer = new TextMeshProUGUI[4];
    [SerializeField] private Sprite incorrectColor;
    [SerializeField] private Sprite correctColor;
    [SerializeField] private Sprite normalColor;
    [SerializeField] private Sprite[] spritesSigns = new Sprite[6];
    [SerializeField] private GameObject groupSigns;
    [SerializeField] private Image[] imgSigns = new Image[4];
    private int indexSelected = 0;

    private void Start()
    {
        arrButtonAnswer[0].onClick.AddListener(delegate { OnClickedButtonAnswer("a", 0); });
        arrButtonAnswer[1].onClick.AddListener(delegate { OnClickedButtonAnswer("b", 1); });
        arrButtonAnswer[2].onClick.AddListener(delegate { OnClickedButtonAnswer("c", 2); });
        arrButtonAnswer[3].onClick.AddListener(delegate { OnClickedButtonAnswer("d", 3); });
    }

    public void OnClickedButtonAnswer(string answer, int index)
    {
        indexSelected = index;
        CheckTheSelectedResult(answer);
    }

    public void CheckTheSelectedResult(string answer)
    {
        bool isCorrect = ViewManager.instance.CheckCorrectAnswer(answer);
        arrButtonAnswer[indexSelected].interactable = false;
        //Color currentColor = isCorrect ? correctColor : incorrectColor;
        //Color newColor = new Color(currentColor.r, currentColor.g, currentColor.b, 1.0f);
        arrBoxAnswer[indexSelected].sprite = isCorrect ? correctColor : incorrectColor;
        if (isCorrect)
        {
            foreach (Button btn in arrButtonAnswer)
            {
                btn.interactable = false;
            }
            ViewManager.instance.NextQuestion();
        }
    }

    public override void Setup(ViewParam param)
    {
        txtNumber.text = param.number + "/" + param.totalQuestion;
        for(int i = 0; i < arrBoxAnswer.Length; i++)
        {
            arrBoxAnswer[i].sprite = normalColor;
            arrButtonAnswer[i].interactable = true;
        }    
        txtQuestion.text = param.question.ToString();

        //Signs
        if(param.typeSigns != null)
        {
            groupSigns.SetActive(true);
            for (int i = 0; i < imgSigns.Length; i++)
            {
                imgSigns[i].sprite = spritesSigns[(int)param.typeSigns[i]];
            }
        } else
        {
            groupSigns.SetActive(false);
        }

        for (int i = 0; i < param.answer.Length; i++)
        {
            arrTextAnswer[i].text = param.answer[i].ToString();
        }
    }

    public override void OnHideView()
    {

    }
}
