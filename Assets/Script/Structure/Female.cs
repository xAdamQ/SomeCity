using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Female : Human
{

    public override Human Partner
    {
        get => base.Partner; //it will use base getter
        set
        {
            if (value == null) OnYear -= GiveBirth;
            else if (partner == null) OnYear += GiveBirth; //make sure it's not just change on parent

            partner = value;
        }
    }
    public static List<Female> Singles;

    static GameObject MalePref, FemalePref;
    static void CreateStartPop()
    {
        var startMale = Instantiate(MalePref).transform.GetChild(0).GetComponent<Male>();
        var startFemale = Instantiate(FemalePref).transform.GetChild(0).GetComponent<Female>();

        Free[0].Add(startMale);
        Free[0].Add(startFemale);

        Homeless.Enqueue(startMale);
        Homeless.Enqueue(startFemale);

        startMale.Partner = startFemale;
        startFemale.Partner = startMale;


        RaycastHit hit;

        var ranPoz = new Vector3(Random.Range(-World.Size / 2f, World.Size / 2f), (World.Size / 2f) + 7f, Random.Range(-World.Size / 2f, World.Size / 2f));
        Physics.Raycast(ranPoz, Vector3.down, out hit);
        startMale.transform.parent.position = hit.point + Vector3.up;

        ranPoz = new Vector3(Random.Range(-World.Size / 2f, World.Size / 2f), (World.Size / 2f) + 7f, Random.Range(-World.Size / 2f, World.Size / 2f));
        Physics.Raycast(ranPoz, Vector3.down, out hit);
        startFemale.transform.parent.position = hit.point + Vector3.up;

        startMale.Name = UniqueName();
        startFemale.Name = UniqueName();

        ByName.Add(startMale.Name, startMale);
        ByName.Add(startFemale.Name, startFemale);

        HumanEleUI.Create(startMale);
        HumanEleUI.Create(startFemale);

    }

    public new static void OneTimeIni()
    {
        MalePref = Resources.Load<GameObject>("Prefs/Male");
        FemalePref = Resources.Load<GameObject>("Prefs/Female");

        Singles = new List<Female>();

    }

    public static void EveryWorldIni()
    {
        CreateStartPop();
    }

    new void Start()
    {
        base.Start();
    }


    static readonly float BearProb = 1f / 4f;
    void GiveBirth()
    {
        if (!MyLib.SimpleRandom(BearProb)) return;

        var childPref = MyLib.BoolRand() ? MalePref : FemalePref;
        var child = Instantiate(childPref).transform.GetChild(0).GetComponent<Human>();

        TimeManger.I.DoAfterDays(GrowUpDuration * 30 * 12, child.GrowUp);

        child.transform.localScale = Vector3.one * .5f;
        child.transform.parent.position = transform.position;
        child.transform.localPosition = Vector3.zero;

        child.Father = (Male)Partner;
        child.Mother = this;

        child.FindShelter();

        child.Name = UniqueName() + " " + Partner.Name;

        HumanEleUI.Create(child);

    }
}
