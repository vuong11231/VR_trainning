using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PCCCRecord
{
    public int id;
    public string question;
    public string answerA;
    public string answerB;
    public string answerC;
    public string answerD;
    public string correct;
    public string typeSigns;
}

public class PCCC : BYDataTable<PCCCRecord>
{
    public override void SetCompareObject()
    {
        recoreCompare = new ConfigCompareKey<PCCCRecord>("id");
    }
}
