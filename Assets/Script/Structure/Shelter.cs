using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelter : Building
{
    public int Capacity = 5;
    public List<Human> Residents;

    public static Stack<Shelter> FreeShelters;

    protected override void FinishBuild()
    {
        base.FinishBuild();
        FindResidents();
    }

    public void FindResidents()
    {
        for (int i = 0; i < Capacity && Human.Homeless.Count != 0; i++)
        {
            var homeless = Human.Homeless.Peek();
            if (homeless == null)
            {
                Human.Homeless.Dequeue();
                continue;
            }//dead human

            homeless.Shelter = this;
            homeless.IsHomeless = false;

            Residents.Add(homeless);

            Human.Homeless.Dequeue();

        }

        if (IsFull() == false)
            FreeShelters.Push(this);
    }

    public bool IsFull()
    {
        return Residents.Count == Capacity;
    }

    private void Start()
    {
        Residents = new List<Human>();
    }
    public new static void OneTimeIni()
    {
        FreeShelters = new Stack<Shelter>();
    }

    private void OnDestroy()
    {
        if (Manger.Quit) return;
        foreach (var r in Residents)
        {
            Human.Homeless.Enqueue(r);
            r.IsHomeless = true;
        }
    }
}
