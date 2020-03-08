using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Building : GroundEle
{
    public Mat.Pair[] RequirdMats;
    public int RequirdHumans = 1;
    public int RequirdMonths = 1;

    [HideInInspector] public int EduLev; //specified in res folder

    #region placing
    [HideInInspector] public int CollidersCount;
    private void OnCollisionEnter(Collision collision)
    {
        CollidersCount++;
    }
    private void OnCollisionExit(Collision collision)
    {
        CollidersCount--;
    }
    //for perfomance I should use this only while placing/moving

    #endregion

    static Mesh ConstructingMesh;
    static Texture ConstructingTexture;

    public bool ConstructIfPossible()
    {

        for (int i = 0; i < RequirdMats.Length; i++)
        {
            if (Mat.I.GetCount(RequirdMats[i].Kind) < RequirdMats[i].Count)
            {
                cantBuild();
                return false;
            }
        }//make sure mats are enough

        var avaiHumans = new List<Human>();
        if (RequirdHumans != 0)
            for (int i = EduLev; ; i++)
            {
                if (i == Human.Free.Count)
                {
                    cantBuild();
                    return false;
                }

                for (int e = 0; e < Human.Free[i].Count; e++)
                {
                    if (Human.Free[i][e] != null)
                        avaiHumans.Add(Human.Free[i][e]);

                    if (avaiHumans.Count == RequirdHumans)
                        break;
                }

                if (avaiHumans.Count == RequirdHumans)
                    break;
            }//make sure humans are enough and if it pick required humans

        void cantBuild()
        {
            Debug.Log("cnt");
            //you can also debug message
            MarketManger.I.DestroyHeld();
        }

        Construct(avaiHumans);
        return true;
    }

    List<Human> Workers;
    protected void Construct(List<Human> avaiHumans)
    {
        for (int i = 0; i < RequirdMats.Length; i++)
        {
            Mat.I.AddCount(RequirdMats[i].Kind, -RequirdMats[i].Count);
        }//reduce materials

        var deletedCount = 0;
        for (int i = EduLev; deletedCount != RequirdHumans; i++)
        {
            var remainingRequired = RequirdHumans - deletedCount;
            if (remainingRequired >= Human.Free[i].Count)
            {
                deletedCount += Human.Free[i].Count;
                Human.Free[i].Clear();
            }
            else
            {
                deletedCount += remainingRequired;
                Human.Free[i].RemoveRange(0, remainingRequired);
            }

        }//remove selected humans from free and add to busy


        Human.Free[EduLev].Shuffle();
        for (int i = 0; i < avaiHumans.Count; i++)
        {
            avaiHumans[i].Job = this;
        }//set jobs for humans

        ActualMesh = GetComponent<MeshFilter>().mesh;
        ActualTexture = GetComponent<MeshRenderer>().material.mainTexture;

        GetComponent<MeshFilter>().mesh = ConstructingMesh;
        GetComponent<MeshRenderer>().material.mainTexture = ConstructingTexture;

        Workers = avaiHumans;

        TimeManger.I.DoAfterDays(RequirdMonths * 30, () => FinishBuild());

        iTween.MoveFrom(gameObject, transform.position + transform.up * 3, 1f);
    }

    protected virtual void FinishBuild()
    {
        GetComponent<MeshFilter>().mesh = ActualMesh;
        GetComponent<MeshRenderer>().material.mainTexture = ActualTexture;

        for (int i = 0; i < Workers.Count; i++)
        {
            Human.Free[Workers[i].EducationLevel].Add(Workers[i]);
        }//set workers back free
        Workers.Clear();
    }

    Mesh ActualMesh;
    Texture ActualTexture;

    public Vector3 GetNearbyLocation()
    {
        return
           transform.position +
           transform.right * (GetComponent<MeshFilter>().mesh.bounds.extents.x + .5f);

    }

    public static void OneTimeIni()
    {
        ConstructingMesh = Resources.Load<Mesh>("Meshs/ConstructingMesh");
        ConstructingTexture = Resources.Load<Texture>("Meshs/ConstructingMesh");
    }



    //public Mat.Pair[] RequirdMats;
    //public int RequirdHumans;
    //public int RequirdMonths;
    //[HideInInspector] public int RequiredEduLev; //specified in res folder


    //static Mesh ConstructingMesh;
    //static Texture ConstructingTexture;

    //Mesh ActualMesh;

    //public bool ConstructIfPossible()
    //{
    //    for (int i = 0; i < RequirdMats.Length; i++)
    //    {
    //        if (Mat.Counts[RequirdMats[i].Kind] < RequirdMats[i].Count)
    //        {
    //            cantBuild();
    //            return false;
    //        }
    //    }//make sure mats are enough

    //    var avaiHumans = new List<Human>();
    //    for (int i = RequiredEduLev; avaiHumans.Count != RequirdHumans; i++)
    //    {
    //        if (i == Human.Free.Count)
    //        {
    //            cantBuild();
    //            return false;
    //        }
    //        var remainingRequired = RequirdHumans - avaiHumans.Count;
    //        if (remainingRequired >= Human.Free[i].Count)
    //            avaiHumans.AddRange(Human.Free[i]);
    //        else
    //            avaiHumans.AddRange(Human.Free[i].GetRange(0, remainingRequired));

    //    }//make sure humans are enough and if it pick required humans

    //    void cantBuild()
    //    {
    //        //you can also debug message
    //        MarketManger.I.DestroyHeld();
    //    }

    //    Construct(avaiHumans);
    //    return true;
    //}

    //void Construct(List<Human> avaiHumans)
    //{
    //    for (int i = 0; i < RequirdMats.Length; i++)
    //    {
    //        Mat.Counts[RequirdMats[i].Kind] -= RequirdMats[i].Count;
    //    }//reduce materials

    //    var deletedCount = 0;
    //    for (int i = RequiredEduLev; deletedCount != RequirdHumans; i++)
    //    {
    //        var remainingRequired = RequirdHumans - deletedCount;
    //        if (remainingRequired >= Human.Free[i].Count)
    //        {
    //            deletedCount += Human.Free[i].Count;
    //            Human.Free[i].Clear();
    //        }
    //        else
    //        {
    //            deletedCount += remainingRequired;
    //            Human.Free[i].RemoveRange(0, remainingRequired);
    //        }

    //    }//remove selected humans from free and add to busy


    //    Human.Free[RequiredEduLev].Shuffle();
    //    for (int i = 0; i < avaiHumans.Count; i++)
    //    {
    //        avaiHumans[i].Job = this;
    //    }//set jobs for humans

    //    //ActualMesh = GetComponent<MeshFilter>().mesh;

    //    GetComponent<MeshFilter>().mesh = ConstructingMesh;
    //    GetComponent<MeshRenderer>().material.mainTexture = ConstructingTexture;

    //    TimeManger.I.DoAfterDays(RequirdMonths * 30, FinishBuild);

    //    Workers = avaiHumans;
    //}

    //List<Human> Workers;
    //public void FinishBuild()
    //{
    //    //GetComponent<MeshFilter>().mesh = ActualMesh;
    //    for (int i = 0; i < Workers.Count; i++)
    //    {
    //        Human.Free[Workers[i].EducationLevel].Add(Workers[i]);
    //    }//set workers back free
    //    Workers.Clear();
    //}


    //#region placing
    //[HideInInspector] public int CollidersCount;
    //private void OnCollisionEnter(Collision collision)
    //{
    //    CollidersCount++;
    //}
    //private void OnCollisionExit(Collision collision)
    //{
    //    CollidersCount--;
    //}

    //#endregion



}
