using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ResourceBuilding : Building
{
    [SerializeField] int AmountPerDay;

    [SerializeField] protected Mat.Kind Kind;

    private void OnDestroy()
    {
        TimeManger.I.OnEveryDay -= Add;
    }

    protected override void FinishBuild()
    {
        base.FinishBuild();
        TimeManger.I.OnEveryDay += Add;
    }

    void Add()
    {
        Mat.I.AddCount(Kind, AmountPerDay);
    }//daily
}
