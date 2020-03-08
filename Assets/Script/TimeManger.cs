using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManger : MonoBehaviour
{
    public static TimeManger I;

    [SerializeField] Text CurYearText, CurMonthText, CurDayText;
    public int CurYear, CurMonth, CurDay;
    public event System.Action OnEveryDay, OnEveryMonth, OnEveryYear;
    public SortedList<int, System.Action> DayEvents;

    void Awake()
    {
        I = this;

        DayEvents = new SortedList<int, System.Action>();

        OnEveryDay = new System.Action(() =>
        {
            var roundDay = ++CurDay % 30;
            CurDayText.text = roundDay.ToString();

            if (roundDay == 0) OnEveryMonth();

            if (DayEvents.Keys.Count != 0 && DayEvents.Keys[0] == CurDay)
            {
                DayEvents.Values[0]();
                DayEvents.RemoveAt(0);
            }
        });

        OnEveryMonth = new System.Action(() =>
        {
            var roundMonth = ++CurMonth % 12;
            CurMonthText.text = roundMonth.ToString();

            if (roundMonth == 0) OnEveryYear();
        });

        OnEveryYear = new System.Action(() =>
        {
            ++CurYear;

            CurYearText.text = CurYear.ToString();
        });

    }

    void Start()
    {
        SetDayDuration();
        StartCoroutine(Day());
    }


    //following funs attach actions to certain date
    public void DoAfterDays(int daysToWait, System.Action evnt)
    {
        var day = CurDay + daysToWait;
        if (!DayEvents.ContainsKey(day)) DayEvents.Add(day, new System.Action(evnt));
        else DayEvents[day] += evnt;
    }

    [HideInInspector] public float DayDuration;
    IEnumerator Day()
    {
        while (true)
        {
            OnEveryDay();
            yield return new WaitForSeconds(DayDuration);
        }
    }

    static readonly float DayDuationMin = .005f, DayDurationMax = 300;
    [SerializeField] Slider DayDurationSlider;
    public void SetDayDuration()
    {
        DayDuration = Mathf.Lerp(DayDuationMin, DayDurationMax, DayDurationSlider.value);
    }
}
