using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class StartInstructions : MonoBehaviour
{
    void Start()
    {
        GetComponent<CanvasGroup>().DOFade(0, 1).SetDelay(4).OnComplete(() => gameObject.SetActive(false));

        //GetComponent<Graphic>().DOFade(0, 1).SetDelay(4);
        //foreach (Transform item in transform)
        //{
        //    if (item.GetComponent<MaskableGraphic>())
        //        GetComponent<MaskableGraphic>().DOFade(0, 1).SetDelay(1);

        //}
    }
}
