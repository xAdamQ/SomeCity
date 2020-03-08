using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketManger : MonoBehaviour
{
    public static MarketManger I;

    void Awake()
    {
        I = this;
    }

    [SerializeField] GameObject GroundEleBtn;
    [SerializeField] Transform Content;
    void Start()
    {
        //gen market
        for (int i = 0; i <= World.EduLev; i++)
        {
            var eles = Resources.LoadAll<GameObject>("Prefs/GroundEles/0");
            for (int e = 0; e < eles.Length; e++)
            {
                var newBtn = Instantiate(GroundEleBtn, Content).GetComponent<GroundEleBtn>();
                var building = eles[e].GetComponent<Building>();

                newBtn.Go = eles[e];
                newBtn.NameText.text = eles[e].name;

                for (int t = 0; t < building.RequirdMats.Length; t++)
                {
                    newBtn.RequiredMatsText.text += building.RequirdMats[t].Kind.ToString() + " " + building.RequirdMats[t].Count.ToString() + '\n';
                }
                newBtn.RequiredMatsText.text += "required humans:  " + building.RequirdHumans;

                building.EduLev = 0;
            }
        }
    }

    void OnNewEduLevel()
    {

    }

    static GameObject ChoosenObj, HeldObj; //the pref //the actual instance
    static Coroutine CurrentPutCor;
    public static IEnumerator PutBuilding()
    {
        if (HeldObj != null)
            Destroy(HeldObj);

        var newBuilding = Instantiate(ChoosenObj);
        HeldObj = newBuilding;
        Transform lastSurface = null;

        while (true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); RaycastHit hit;
            if (World.IsMouseNotOverUI() && Physics.Raycast(ray, out hit, 999, World.TerLayers))
            {
                if (hit.transform.parent != lastSurface)
                {
                    lastSurface = hit.transform.parent;
                    newBuilding.transform.SetParent(hit.transform.parent);
                }//if parent changed
                newBuilding.transform.localEulerAngles = Vector3.zero;
                newBuilding.transform.position = hit.point + (hit.transform.up * .05f);
                if (newBuilding.GetComponent<Building>().CollidersCount == 0)
                {

                    var extends = newBuilding.GetComponent<MeshFilter>().mesh.bounds.extents;
                    var poz = newBuilding.transform.position;

                    var buttomPoints = new Vector3[] {
                            (newBuilding.transform.right * -extends.x)+(newBuilding.transform.forward * -extends.z) + poz,
                            (newBuilding.transform.right * -extends.x)+(newBuilding.transform.forward * extends.z) + poz,
                            (newBuilding.transform.right * extends.x)+(newBuilding.transform.forward * -extends.z) + poz,
                            (newBuilding.transform.right * extends.x)+(newBuilding.transform.forward * extends.z) + poz,
                        };
                    var grounded = true;
                    for (int i = 0; i < 4; i++)
                    {
                        if (Physics.Raycast(buttomPoints[i], -newBuilding.transform.up, .1f) == false)
                        {
                            grounded = false;
                            break;
                        }
                    }
                    //check if mesh is fully grounded

                    if (grounded)
                    {
                        newBuilding.GetComponent<Renderer>().material.color = Color.white;
                        if (Input.GetMouseButtonDown(0))
                        {
                            if (newBuilding.GetComponent<Building>().ConstructIfPossible())
                                HeldObj = null;

                            yield break;
                        }
                    }
                    else
                    {
                        newBuilding.GetComponent<Renderer>().material.color = Color.blue;
                    }
                }
                else
                {
                    newBuilding.GetComponent<Renderer>().material.color = Color.blue;
                }
            }

            yield return new WaitForFixedUpdate();
        }
    }

    public static void SelectBuilding(GameObject choosen)
    {
        ChoosenObj = choosen;

        if (CurrentPutCor != null)
            I.StopCoroutine(CurrentPutCor);

        CurrentPutCor = I.StartCoroutine(PutBuilding());
    }

    public void DestroyHeld()
    {
        Destroy(HeldObj);
    }
}
