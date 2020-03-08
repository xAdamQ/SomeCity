using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Male : Human
{
    public static List<Male> Singles;

    new void Start()
    {
        base.Start();
    }

    void Update()
    {

    }

    public new static void OneTimeIni()
    {
        Singles = new List<Male>();
    }
}
