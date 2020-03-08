using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mat : MonoBehaviour
{
    static readonly int matVarientsCount = 4;
    public enum Kind { Wood, Rock, Metal, Food };


    public static Mat I;
    private void Awake()
    {
        I = this;

        Counts = new Dictionary<Kind, int>();
        for (int i = 0; i < matVarientsCount; i++) Counts.Add((Kind)i, 0);
    }

    Dictionary<Kind, int> Counts;

    public void AddCount(Kind kind, int countAdd)
    {
        Counts[kind] += countAdd;
        CountTexts[(int)kind].text = Counts[kind].ToString();
    }
    public int GetCount(Kind kind)
    {
        return Counts[kind];
    }


    public static void OneTimeIni()
    {

    }

    [System.Serializable]
    public struct Pair
    {
        public Mat.Kind Kind;
        public int Count;

        public Pair(Mat.Kind kind, int count)
        {
            Kind = kind;
            Count = count;
        }
    }

    [SerializeField] Text[] CountTexts;
}
