using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyLib
{
    public static void Shuffle<T>(this IList<T> List)
    {
        for (int i = 0; i < List.Count; i++)
        {
            var temp = List[i];
            int randomIndex = Random.Range(i, List.Count);
            List[i] = List[randomIndex];
            List[randomIndex] = temp;
        }
    }

    #region random
    public static int BiasedRandom(int min, int max, float amount, bool towardMax = true)
    {
        max--; min++;

        var rand = towardMax ? 1 - Mathf.Pow(Random.Range(0f, 1f), 1f / amount) : Mathf.Pow(Random.Range(0f, 1f), 1f / amount);

        return (int)Mathf.Floor((rand * (1 + max - min)) + min);
    }
    public static int BiasedRandom(BiasedRandomInRange BRR)
    {
        var rand = BRR.TowardMax ?
            Mathf.Pow(Random.Range(0f, 1f), 1f / BRR.AmountOfBais) :
            1 - Mathf.Pow(Random.Range(0f, 1f), 1f / BRR.AmountOfBais);

        return (int)Mathf.Floor((rand * (1 + BRR.Range.y - BRR.Range.x)) + BRR.Range.x);
    }
    public static float BiasedRandom(BiasedRandomInFloatRange BRR)
    {
        var rand = BRR.TowardMax ?
            Mathf.Pow(Random.Range(0f, 1f), 1f / BRR.AmountOfBais) :
            1 - Mathf.Pow(Random.Range(0f, 1f), 1f / BRR.AmountOfBais);

        return (rand * (BRR.Range.y - BRR.Range.x)) + BRR.Range.x;
    }
    public static int SlicedRandom(float[] poss)
    {
        var rand = Random.Range(0f, 1f);
        var totalPoss = 0f;

        for (int i = 0; i < poss.Length; i++)
        {
            totalPoss += poss[i];
            if (rand < totalPoss)
            {
                return i;
            }
        }

        return -1;
    }
    ///<summary>
    /// takes array of poss, based on it it return random num (poss sum doesn't have to be 1)
    ///</summary>
    public static int SumRandom(IList<float> poss)
    {
        var possSum = 0f;
        for (int i = 0; i < poss.Count; i++)
        {
            possSum += poss[i];
        }

        var rand = Random.Range(0f, possSum);

        var addedPoss = 0f;
        for (int i = 0; i < poss.Count; i++)
        {
            addedPoss += poss[i];
            if (rand < addedPoss)
            {
                return i;
            }
        }

        return -1;
    }
    //my fair random
    public static float PeriodsRandom(List<Period> periods)
    {
        var totalDelta = 0f;
        for (int i = 0; i < periods.Count; i++)
            totalDelta += periods[i].Delta;

        var peroidPoss = new float[periods.Count];
        for (int i = 0; i < periods.Count; i++)
            peroidPoss[i] = periods[i].Delta / totalDelta;

        var choosenPeriod = periods[SlicedRandom(peroidPoss)];

        return Random.Range(choosenPeriod.Start, choosenPeriod.End);
    }
    public static bool BoolRand()
    {
        return Random.Range(0, 2) == 0;
    }

    public static bool SimpleRandom(float prob)
    {
        return Random.Range(0f, 1f) <= prob;
    }

    public static T GetRandom<T>(this IList<T> List)
    {
        return List[Random.Range(0, List.Count)];
    }
    public static T GetRandom<T>(this IList<T> List, out int index)
    {
        index = Random.Range(0, List.Count);
        return List[index];
    }

    #endregion
}

public class Node<T> where T : IComparer
{
    public T Value;
    public Node<T> Right, Left;
    public Node(T value)
    {
        Value = value;
    }

    public bool Compare(T x, T y)
    {
        return EqualityComparer<T>.Default.Equals(x, y);
    }

}

//public class BinarySearchTree<T> where T : IComparer
//{
//    public Node<T> Root;

//    public bool Compare(T x, T y)
//    {
//        return EqualityComparer<T>.Default.Equals(x, y);
//    }

//    public Node<T> Find(T val)
//    {

//        Node<T> tmp = Root;

//        //if (val == val)
//        //{

//        //}

//        //while ((tmp != null) && (tmp.Value < val))
//        //{

//        //}
//        //    if (tmp->value > val)
//        //        tmp = tmp->left;
//        //    else
//        //        tmp = tmp->right;

//        return tmp;
//    }

//    public void Remove(T value)
//    {
//        //if ()
//    }

//    //void BSTree<T>::remove(T val)
//    //{
//    //    assert(contains(val));
//    //    Node<T>* n = findNode(val);
//    //    if ((n->left == NULL) && (n->right == NULL)) //deleting a leaf node
//    //    {
//    //        if (n == root)
//    //            root = NULL;
//    //        else
//    //        {
//    //            Node<T>* parent = findParent(val);
//    //            if (val < parent->value)
//    //                parent->left = NULL;
//    //            else
//    //                parent->right = NULL;
//    //        }
//    //        delete n;
//    //    }
//    //    else if ((n->left == NULL) && (n->right != NULL))
//    //    {
//    //        Node<T>* parent = findParent(val);
//    //        if (n == root)
//    //            root = n->right;
//    //        else
//    //        {
//    //            if (val < parent->value)
//    //                parent->left = n->right;
//    //            else
//    //                parent->right = n->right;
//    //        }
//    //        delete n;
//    //    }
//    //    else if ((n->left != NULL) && (n->right == NULL))
//    //    {
//    //        Node<T>* parent = findParent(val);
//    //        if (n == root)
//    //            root = n->left;
//    //        else
//    //        {
//    //            if (val < parent->value)
//    //                parent->left = n->left;
//    //            else
//    //                parent->right = n->left;
//    //        }
//    //        delete n;
//    //    }
//    //    else
//    //    {
//    //        Node<T>* minNode = findMin(n->right);
//    //        Node<T>* parent = findParent(minNode->value);
//    //        n->value = minNode->value;

//    //        if (parent == n)
//    //            parent->right = minNode->right;
//    //        else
//    //            parent->left = minNode->right;
//    //        delete minNode;
//    //    }

//    //}
//}

#region structs
[System.Serializable]
public struct Period : System.IComparable
{
    public float Start, End, Delta;
    public Period(float start, float end)
    {
        Start = start;
        End = end;
        Delta = End - Start;
    }

    public int CompareTo(object obj)
    {
        var other = (Period)obj;
        return Delta.CompareTo(other.Delta);
    }//no null handling as struct can't be null

    public override string ToString()
    {
        return Start + " to " + End;
    }

    public static Period operator +(Period period, float floatVal)
    {
        return new Period(period.Start + floatVal, period.End + floatVal);
    }
    public static Period operator +(float floatVal, Period period)
    {
        return period + floatVal;
    }
}

[System.Serializable]
public struct BiasedRandomInRange
{
    public Vector2Int Range;
    public float AmountOfBais;
    public bool TowardMax;
}
[System.Serializable]
public struct BiasedRandomInFloatRange
{
    public Vector2 Range;
    public float AmountOfBais;
    public bool TowardMax;
}

#endregion