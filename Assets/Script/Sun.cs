using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    //float Spacing = 7;
    //float DayValue = 20f; //SPEED PER SEC

    //Hashtable RotArHT;

    void Start()
    {
        //RotArHT = new Hashtable();

        //RotArHT.Add("from", 0f);
        //RotArHT.Add("to", 360f);
        //RotArHT.Add("time", 30);
        //RotArHT.Add("onupdate", "RotAr");
        //RotArHT.Add("onupdatetarget", gameObject);
        //RotArHT.Add("easetype", iTween.EaseType.linear);


        //StartRot();

        //TimeManger.I.OnEveryDay += StartRot;

        transform.position = Vector3.one * World.Size * .75f;
        TimeManger.I.OnEveryDay += () => { StartCoroutine(DayRoation()); };

    }

    IEnumerator DayRoation()
    {
        var frameCount = TimeManger.I.DayDuration / Time.fixedDeltaTime;
        var DisPerFrame = 360 / frameCount;

        for (int i = 0; i < frameCount; i++)
        {
            transform.RotateAround(Vector3.zero, new Vector3(-1, 0, 1), DisPerFrame);
            yield return new WaitForFixedUpdate();
        }

    }


    //void FixedUpdate()
    //{
    //    //var anglePerUpdate
    //    //transform.RotateAround(Vector3.zero, new Vector3(-1, 0, 1), DayValue * Time.fixedDeltaTime);
    //}


    //void StartRot()
    //{
    //    RotArHT["time"] = TimeManger.I.DayDuration;

    //    iTween.ValueTo(gameObject, RotArHT);
    //}

    //public void RotAr(float angle)
    //{
    //    Debug.Log("oooo:  " + (angle * Mathf.Deg2Rad) + "  " + angle);
    //    transform.RotateAround(Vector3.zero, new Vector3(-1, 0, 1), angle * Mathf.Deg2Rad);
    //}


    /// <summary>
    /// on every day call this
    /// </summary>
    //void Rotate()
    //{
    //    //iTween.RotateAdd(gameObject,)
    //}







}
