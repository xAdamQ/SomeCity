using System.Collections.Generic;
using UnityEngine;

public class BakeManger : MonoBehaviour
{
    public GameObject[] Made;
    public Mesh[] Meshes;
    public GameObject prefsContainer;
    public GameObject StandardPrefab;

    private void Start()
    {
        //LightMaps = new Dictionary<Mesh, Vector4>();
        //for (int i = 0; i < LightMapScalesAndParts.Length; i++)
        //{
        //    LightMaps.Add(AllMeshs[i], LightMapScalesAndParts[i]);
        //}

        //MakeObjects();
    }

    public void MakeObjects()
    {
        DeletePrefabs();

        prefsContainer = new GameObject();
        prefsContainer.name = "lit prefabs";

        var pointer = new Vector3(0, 500, 0);

        Made = new GameObject[Meshes.Length];

        for (int i = 0; i < Meshes.Length; i++)
        {
            var go = Instantiate(StandardPrefab);
            go.transform.SetParent(prefsContainer.transform);
            go.transform.position = pointer;
            go.GetComponent<MeshFilter>().mesh = Meshes[i];
            go.name = Meshes[i].name;

            Made[i] = go;

            go.isStatic = true;

            pointer.x += 200f;
        }
    }
    public void DeletePrefabs()
    {
        if (prefsContainer)
            DestroyImmediate(prefsContainer);
    }

    public void SetLightmapPartArrays()
    {
        LightMapScalesAndParts = new Vector4[Meshes.Length];
        AllMeshs = new Mesh[Meshes.Length];

        for (int i = 0; i < Meshes.Length; i++)
        {//mesh
            var renderer = Made[i].GetComponent<MeshRenderer>();
            var filter = Made[i].GetComponent<MeshFilter>();

            LightMapScalesAndParts[i] = renderer.lightmapScaleOffset;
            AllMeshs[i] = filter.sharedMesh;
        }

        Debug.Log("Light Map Set Done!");
    }

    public GameObject CloneWithLightmap(GameObject prefab, Transform parent = null)
    {
        var go = Instantiate(prefab, parent);

        SetLightmap(go);

        return go;
    }

    public static void SetLightmap(GameObject go)
    {
        var temp = new Vector4();
        if (go.GetComponent<MeshFilter>().sharedMesh == null)
        {
            Debug.Log("null mesh");
            return;
        }
        if (LightMaps.TryGetValue(go.GetComponent<MeshFilter>().sharedMesh, out temp))
        {
            go.GetComponent<MeshRenderer>().lightmapScaleOffset = temp;
            go.GetComponent<MeshRenderer>().lightmapIndex = 0;
        }
        else
        {
            Debug.LogWarning(go.GetComponent<MeshFilter>().sharedMesh.name + "  Failed to get LM");
        }


    }

    public static Vector4[] LightMapScalesAndParts { get; internal set; }
    public static Mesh[] AllMeshs { get; internal set; }

    public static Dictionary<Mesh, Vector4> LightMaps;

}