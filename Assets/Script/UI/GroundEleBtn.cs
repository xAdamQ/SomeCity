using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEleBtn : MonoBehaviour
{
    [HideInInspector] public GameObject Go;
    public UnityEngine.UI.Text NameText, RequiredMatsText;

    public void OnClick()
    {
        MarketManger.SelectBuilding(Go);
    }




}