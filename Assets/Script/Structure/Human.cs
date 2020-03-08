using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class Human : Creature
{
    public int EducationLevel;
    public int Age;
    public string Name;

    public Male Father;
    public Female Mother;

    public static Human Selected;

    public bool IsSingle;
    protected Human partner;
    public virtual Human Partner { set { partner = value; } get { return partner; } }

    //each of sec level list represents people on edu level
    public bool IsFree;
    public static List<List<Human>> Free;

    bool isHomeLess;
    public bool IsHomeless
    {
        get => IsHomeless;
        set
        {
            isHomeLess = value;
            if (value) OnBecomingHomeless();
            else OnFindHome();
        }
    }
    public static Queue<Human> Homeless;
    public Shelter Shelter;

    public static SortedList<string, Human> ByName;

    static float NoHomeMaxLife = 1f / 10f;
    void OnBecomingHomeless()
    {
        DieProb += NoHomeMaxLife; //make his maximum life 10 years, if there's otherthing affecting it's even worse
    }
    void OnFindHome()
    {
        DieProb -= NoHomeMaxLife;
    }

    void OnMouseDown()
    {
        Selected = this;
        CharDataPanal.I.View(this);
    }

    public static void OneTimeIni()
    {
        Homeless = new Queue<Human>();
        ByName = new SortedList<string, Human>();

        Free = new List<List<Human>>();
        for (int i = 0; i < 4; i++) Free.Add(new List<Human>());


        UpHT = new Hashtable();
        UpHT.Add("y", JumpHight);
        UpHT.Add("time", AnimTime);
        UpHT.Add("oncompletetarget", null);
        UpHT.Add("oncomplete", "DownAnim");

        DownHT = new Hashtable();
        DownHT.Add("y", -JumpHight);
        DownHT.Add("time", AnimTime);
        DownHT.Add("oncompletetarget", null);
        DownHT.Add("oncomplete", "UpAnim");


    }
    protected void Start()
    {
        TimeManger.I.OnEveryYear += OnYearHandler;

        LinearActionDates = new List<int>();
        LinearActions = new List<System.Action>();

        OnYear += Die;
        UpAnim();

    }

    protected const int GrowUpDuration = 1;
    public void GrowUp()
    {
        transform.localScale = Vector3.one;
        CurrentFoodUnits = AdultFoodUnits;
        Free[0].Add(this);
        FindPartner();
    }

    protected System.Action OnYear;
    void OnYearHandler()
    {
        OnYear();
    }

    float DieProb = 1f / 100f + NoHomeMaxLife; //default die prob(natural death) + homless prob, detached when find a home
    void Die()
    {
        if (MyLib.SimpleRandom(DieProb))
        {
            Destroy(transform.parent.gameObject);

            TimeManger.I.OnEveryYear -= OnYearHandler;
        }
    }

    const int ChildFoodUnits = 2, AdultFoodUnits = 5;
    int CurrentFoodUnits = ChildFoodUnits;
    void Eat()
    {
        Mat.I.AddCount(Mat.Kind.Food, -CurrentFoodUnits);
    }//every day

    #region Naming
    public static readonly char[]
      Vowels = new char[] { 'a', 'o', 'e', 'u', 'i' },
      Consonants = new char[] { 'b', 'c', 'd', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'y', 'z' };

    public static readonly char[][] Alphabet = new char[][] { Vowels, Consonants };

    public static string UniqueName()
    {
        var name = "";
        var size = Random.Range(3, 6);
        var continousTwo = false;
        var prevKind = 0;
        for (int i = 0; i < size; i++)
        {
            if (continousTwo || MyLib.SimpleRandom(.5f))
            {
                prevKind = prevKind == 0 ? 1 : 0;
                continousTwo = false;
            }//change to the other char kind
            else
            {
                continousTwo = true;
            }//keep the same kind of char

            name += Alphabet[prevKind].GetRandom();
        }
        return name;
    }

    #endregion

    static readonly Vector3 LocateFar = new Vector3(0, 6, -4);
    public void Locate()
    {
        Camera.main.transform.position = transform.position + LocateFar;
        Camera.main.transform.parent.eulerAngles = Vector3.zero;
    }

    Building job;
    public Building Job
    {
        get => job;
        set
        {
            job = value;
            transform.parent.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(job.GetNearbyLocation());
        }
    }

    public void FindShelter()
    {
        while (true)
        {
            if (Shelter.FreeShelters.Count == 0)
            {
                Homeless.Enqueue(this);
                return;
            }
            else if (Shelter.FreeShelters.Peek() == null)
            {
                Shelter.FreeShelters.Pop();
                continue;
            }//fallen shelter
            else
            {
                break;
            }
        }//make sure u have valid shelter

        IsHomeless = false;
        Shelter.FreeShelters.Peek().Residents.Add(this);
        Shelter = Shelter.FreeShelters.Peek();
        //make connections

        if (Shelter.FreeShelters.Peek().IsFull())
            Shelter.FreeShelters.Pop();
    }

    bool AppQuit;
    private void OnApplicationQuit()
    {
        AppQuit = true;
    }
    private void OnDestroy()
    {
        if (AppQuit) return;

        if (IsFree)
        {
            Free[EducationLevel].Remove(this);
        }

        if (IsSingle)
        {
            if (GetComponent<Male>()) Male.Singles.Remove((Male)this);
            else Female.Singles.Remove((Female)this);
        }
        else
        {
            if (partner != null)//2343
                Partner.FindPartner();
        }

        for (int i = 0; i < LinearActionDates.Count; i++)
        {
            var day = LinearActionDates[i];
            if (day < TimeManger.I.CurDay)//2343
                continue;

            TimeManger.I.DayEvents[day] -= LinearActions[i];
            if (TimeManger.I.DayEvents[day] == null) TimeManger.I.DayEvents.Remove(day);
        }

        OnDie?.Invoke();
    }

    protected void AddLinearEvent(int daysToWait, System.Action action)
    {
        TimeManger.I.DoAfterDays(daysToWait, action);
        LinearActionDates.Add(daysToWait + TimeManger.I.CurDay);
        LinearActions.Add(action);
    }
    protected List<int> LinearActionDates;
    protected List<System.Action> LinearActions;

    private void FindPartner()
    {
        if (GetComponent<Male>())
        {
            IsSingle = Female.Singles.Count == 0;
            if (IsSingle)
            {
                Male.Singles.Add((Male)this);
            }
            else
            {
                Partner = Female.Singles[0];
                Partner.Partner = this;
                Female.Singles.RemoveAt(0);
            }
        }
        else
        {
            IsSingle = Male.Singles.Count == 0;
            if (IsSingle)
            {
                Female.Singles.Add((Female)this);
            }
            else
            {
                Partner = Male.Singles[0];
                Partner.Partner = this;
                Male.Singles.RemoveAt(0);
            }
        }
    }

    static Hashtable UpHT, DownHT;
    static float AnimTime = .3f, JumpHight = .5f;
    void UpAnim()
    {
        UpHT["oncompletetarget"] = gameObject;
        iTween.MoveAdd(gameObject, UpHT);
    }
    void DownAnim()
    {
        DownHT["oncompletetarget"] = gameObject;
        iTween.MoveAdd(gameObject, DownHT);
    }

    public System.Action OnDie;

}


