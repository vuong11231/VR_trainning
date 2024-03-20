using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DefaultConfigRecord
{
    public int id;
    public string question;
    public string answerA;
    public string answerB;
    public string answerC;
    public string answerD;
    public string correct;
}

public class DefaultConfig : BYDataTable<DefaultConfigRecord>
{
    public override void SetCompareObject()
    {
        recoreCompare = new ConfigCompareKey<DefaultConfigRecord>("id");
    }
}
