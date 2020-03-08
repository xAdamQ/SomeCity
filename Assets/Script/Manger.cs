using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Plane.OneTimeIni();
        World.OneTimeIni();
        Human.OneTimeIni();
        Building.OneTimeIni();
        Mat.OneTimeIni();
        Shelter.OneTimeIni();
        Female.OneTimeIni();
        Male.OneTimeIni();
        Cam.OneTimeIni();
        FoodSourceBuilding.OneTimeIni();
        Mine.OneTimeIni();


        HumanEleUI.OneTimeIni();



    }



    public static bool Quit;



    private void OnApplicationQuit()
    {
        Quit = true;
    }
}
