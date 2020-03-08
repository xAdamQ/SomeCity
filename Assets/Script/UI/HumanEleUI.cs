using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanEleUI : MonoBehaviour
{
    static HumanEleUI Sample;
    [SerializeField] Text Name;
    static Transform Parent;
    public Human Human;

    public static void OneTimeIni()
    {
        Sample = Resources.Load<GameObject>("Prefs/UI/humanUI").GetComponent<HumanEleUI>();
        Parent = GameObject.Find("PopParent").transform;
    }

    public static void Create(Human human)
    {
        Sample.Name.text = human.Name;
        var newBtn = Instantiate(Sample, Parent);
        newBtn.Human = human;
        human.OnDie += newBtn.MyDestroy;
    }


    public void OnClick()
    {
        Human.Locate();
    }

    void MyDestroy()
    {
        Destroy(gameObject);
    }


}
