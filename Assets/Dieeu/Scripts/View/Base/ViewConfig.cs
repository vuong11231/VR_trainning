using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 0-256
public enum ViewIndex
{
    QuestionsView,
    HomeView,
    CongratulationsView,
}
public class ViewParam
{
    public int id = 0;
    public string question = string.Empty;
    public string[] answer = new string[4];
}
public class ViewConfig 
{
    public static ViewIndex[] viewIndices = {
        ViewIndex.QuestionsView,
        ViewIndex.HomeView,
        ViewIndex.CongratulationsView,
    };
}
