using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class World : MonoBehaviour
{
    public static World I;

    public static float EduLev;
    public static int Size = 40;

    public static void OneTimeIni()
    {

    }

    [SerializeField] NavMeshAgent Agent;
    private void Start()
    {
        I = this;

        Plane.EveryWorldIni();
        Female.EveryWorldIni();

    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) && hit.collider.tag == "Terrain")
            {
                if (Human.Selected != null)
                    Human.Selected.transform.parent.GetComponent<NavMeshAgent>().SetDestination(hit.point);
            }
        }
    }

    public static int TerLayers = 1 << 9 | 1 << 10 | 1 << 11 | 1 << 12 | 1 << 13 | 1 << 14;

    public static bool IsMouseNotOverUI()
    {
        return !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
    }



}


