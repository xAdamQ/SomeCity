using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class Plane : MonoBehaviour
{
    //universal over worlds
    static GameObject PlanePref, LinkPref;
    static Vector3Int[] Dirs = new Vector3Int[] { Vector3Int.right, Vector3Int.left, Vector3Int.up, Vector3Int.down, new Vector3Int(0, 0, 1), new Vector3Int(0, 0, -1) };
    static Vector3Int[] PlaneRotArray = new Vector3Int[] { new Vector3Int(0, 0, -90), new Vector3Int(0, 0, 90), new Vector3Int(0, 0, 0), new Vector3Int(180, 0, 0), new Vector3Int(90, 0, 0), new Vector3Int(-90, 0, 0) };
    static Vector3Int[] LinkRotArray = new Vector3Int[] {
        new Vector3Int(0,0,0), new Vector3Int(0,180,0), new Vector3Int(0,-90,0), new Vector3Int(0,90,0), //up plane
        new Vector3Int(180,0,0), new Vector3Int(180,180,0), new Vector3Int(180,-90,0), new Vector3Int(180,-90,0), //down plane
        new Vector3Int(-90,0,0), new Vector3Int(90,0,0), new Vector3Int(90,0,90), new Vector3Int(90,0,180),//side planes
    };
    static Vector3Int[] LinkPozArray;

    static GameObject Quad, Cube;

    static Hashtable StartAnimHT;

    public static void OneTimeIni()
    {
        PlanePref = Resources.Load<GameObject>("Prefs/Plane");
        LinkPref = Resources.Load<GameObject>("Prefs/Link");
        Quad = Resources.Load<GameObject>("Prefs/Quad");
        Cube = Resources.Load<GameObject>("Prefs/terrain cube");

        StartAnimHT = new Hashtable();
        StartAnimHT.Add("position", Vector3.up * World.Size * 1.5f);
        StartAnimHT.Add("time", 10f);
        StartAnimHT.Add("delay", 0f);
        StartAnimHT.Add("islocal", true);

    }
    public static void EveryWorldIni()
    {
        Planes = new Dictionary<Vector3Int, Plane>();
        LinkPozArray = new Vector3Int[] {
        new Vector3Int(1,1,0), new Vector3Int(-1,1,0), new Vector3Int(0,1,1), new Vector3Int(0,1,-1), //up plane
        new Vector3Int(1,-1,0), new Vector3Int(-1,-1,0), new Vector3Int(0,-1,1), new Vector3Int(0,-1,-1), //up plane
        new Vector3Int(1,0,-1), new Vector3Int(1,0,1), new Vector3Int(-1,0,1), new Vector3Int(-1,0,-1)};

        CreateBox();
    }

    static void CreateBox()
    {
        for (int i = 0; i < Dirs.Length; i++)
        {
            CreatePlane(i);
        }

        CreateLinks();
    }
    static void CreatePlane(int ind)
    {
        var dirParent = new GameObject(ind.ToString());
        dirParent.transform.position = Dirs[ind] * (World.Size / 2);
        dirParent.transform.eulerAngles = PlaneRotArray[ind];

        var newPlane = Instantiate(PlanePref, dirParent.transform);
        newPlane.layer = 9 + ind;
        newPlane.transform.localScale = Vector3.one * (World.Size / 10f);
        newPlane.transform.localPosition = Vector3.zero;
        newPlane.transform.localEulerAngles = Vector3.zero;


        newPlane.GetComponent<Plane>().EarlyStart();

        Planes.Add(Dirs[ind], newPlane.GetComponent<Plane>());
    }
    static void CreateLinks()
    {
        var parent = new GameObject("links");
        for (int i = 0; i < LinkRotArray.Length; i++)
        {
            var newLink = Instantiate(LinkPref, parent.transform);
            newLink.transform.position = LinkPozArray[i] * (World.Size / 2);
            newLink.transform.eulerAngles = LinkRotArray[i];
            newLink.GetComponent<NavMeshLink>().width = World.Size - 1;
            newLink.GetComponent<NavMeshLink>().UpdateLink();
        }
    }

    List<MyRect> FreeRects, TerRects;
    void CreateTerrian()
    {
        FreeRects = new List<MyRect>();
        TerRects = new List<MyRect>();
        var planeRect = new MyRect(Vector2.zero, Vector2.one * World.Size);
        FreeRects.Add(planeRect);

        while (FreeRects.Count > 0)
            CreateTerrainRect(FreeRects[0]);

        foreach (var rect in TerRects)
        {
            var yLev = .5f; //for mountain
            var animDelay = 0f;

            var newMount = Instantiate(Cube, transform.parent);
            newMount.transform.localScale = new Vector3(rect.width - .5f, 1, rect.height - .5f);
            newMount.transform.localPosition = new Vector3(rect.Poz.x, .5f, rect.Poz.y);
            newMount.transform.localEulerAngles = Vector3.zero;
            newMount.layer = transform.gameObject.layer;

            iTween.ColorFrom(newMount, iTween.Hash("color", Color.clear, "time", .4f, "delay", animDelay += .2f));

            int floorCount = Random.Range(0, 4);
            var prevFloor = newMount;
            for (int i = 0; i < floorCount; i++)
            {
                var prevSize = prevFloor.GetComponent<Renderer>().bounds.size;

                //skip
                //when size is low the skip poss increase
                var newFloor = Instantiate(Cube, transform.parent);
                newFloor.layer = transform.gameObject.layer;

                var xCut = prevSize.x <= 6 ? Random.Range(prevSize.x / 10f, prevSize.x / 5f) : Random.Range(3f, prevSize.x / 2f);
                var zCut = prevSize.z <= 6 ? Random.Range(prevSize.z / 10f, prevSize.z / 5f) : Random.Range(3f, prevSize.z / 2f);
                var newSize = new Vector3(prevSize.x - xCut, 1, prevSize.z - zCut);
                newFloor.transform.localScale = newSize;

                var xPozRange = (prevSize.x / 2f) - (newSize.x / 2f);
                var zPozRange = (prevSize.z / 2f) - (newSize.z / 2f);
                newFloor.transform.localPosition = new Vector3(
                    Random.Range(-xPozRange, xPozRange) + prevFloor.transform.localPosition.x,
                    ++yLev,
                    Random.Range(-zPozRange, zPozRange) + prevFloor.transform.localPosition.z
                    );

                iTween.ColorFrom(newFloor, iTween.Hash("color", Color.clear, "time", .7f, "delay", animDelay += .2f));

                prevFloor = newFloor;
            }

        }
    }
    void CreateTerrainRect(MyRect surRect)
    {
        //ter for terrain

        FreeRects.RemoveAt(0);

        //make the new rect
        MyRect makeTerRect()
        {
            //min size for surRect is 2 in both dimensions
            var size = new Vector2(Random.Range(2, surRect.width - .5f), Random.Range(2, surRect.height - .5f));
            var xPozRange = new Vector2(surRect.xMin + (size.x / 2), surRect.xMax - (size.x / 2));
            var yPozRange = new Vector2(surRect.yMin + (size.y / 2), surRect.yMax - (size.y / 2));
            var poz = new Vector2(Random.Range(xPozRange.x, xPozRange.y), Random.Range(yPozRange.x, yPozRange.y));
            return new MyRect(poz, size);
        }
        var terRect = makeTerRect();
        TerRects.Add(terRect);

        var interPoints = new Vector2[4];

        for (int i = 0; i < 4; i++)
        {
            //make random line, get intersections
            interPoints[i] = Random.Range(0, 2) == 0 ?
                new Vector2(terRect.Points[i].x, surRect.Points[i].y) :
                new Vector2(surRect.Points[i].x, terRect.Points[i].y);
        }//pick intersection points, create surRects

        for (int i = 1; i <= 4; i++)
        {
            var curInd = i % 4; var prevInd = i - 1;
            var s = new Vector2(); var e = new Vector2();
            if (interPoints[curInd].x != interPoints[prevInd].x && interPoints[curInd].y != interPoints[prevInd].y)
            {
                s = interPoints[curInd]; e = interPoints[prevInd];
            }//adjacent
            else if (interPoints[curInd].x == interPoints[prevInd].x)
            {
                if (IsClose(Mathf.Abs(interPoints[curInd].y - interPoints[prevInd].y), terRect.height, .1f))
                {
                    s = terRect.Points[prevInd];
                    e = interPoints[curInd];
                }//parallel 
                else
                {
                    s = interPoints[prevInd];
                    e = surRect.Points[curInd]; //corner
                }//same
            }//either inter with same border, or parallel borders
            else if (interPoints[curInd].y == interPoints[prevInd].y)
            {
                if (IsClose(Mathf.Abs(interPoints[curInd].x - interPoints[prevInd].x), terRect.width, .1f))
                {
                    s = terRect.Points[prevInd];
                    e = interPoints[curInd];
                }//parallel 
                else
                {
                    s = interPoints[prevInd];
                    e = surRect.Points[curInd]; //corner
                }//same
            }//either inter with same border, or parallel borders

            var poz = Vector2.Lerp(s, e, .5f);
            var size = new Vector2(Mathf.Abs(s.x - e.x), Mathf.Abs(s.y - e.y));
            var newSurRect = new MyRect(poz, size);

            if (size.y >= 3 && size.x >= 3)
            {
                FreeRects.Add(newSurRect);
            }

        }//get diameter points and more

    }

    void EarlyStart()
    {
        CreateTerrian();
        GetComponent<NavMeshSurface>().BuildNavMesh();
    }

    //when you leave to menu this memory will still be allocated but you are not here to play main menu, mean that next load time will be fast like you havn't left
    //I mean this var should be in World and not static
    static Dictionary<Vector3Int, Plane> Planes;

    bool IsClose(float num1, float num2, float amount)
    {
        return
            Mathf.Abs(num1 - num2) <= amount;
    }

    private void Start()
    {
        StartCoroutine(StartAnim());
    }

    IEnumerator StartAnim()
    {
        yield return new WaitForFixedUpdate();

        //StartAnimHT["position"] = Vector3.up * 15;
        //iTween.MoveFrom(gameObject, StartAnimHT);


        //StartAnimHT["position"] = newMount.transform.position + newMount.transform.up * 15;
        //StartAnimHT["delay"] = animDelay;


        //StartAnimHT["position"] = transform.parent.position + Vector3.up * 15;
        //iTween.MoveFrom(transform.parent.gameObject, StartAnimHT);


    }
}

//points must entered with some order
public struct MyRect
{
    public Vector2[] Points;
    public Vector2 Size;
    public Vector2 Poz;
    public float width, height;
    public float xMin, xMax, yMin, yMax;

    public MyRect(Vector2 a, Vector2 b, Vector2 c, Vector2 d)
    {
        Points = new Vector2[4] { a, b, c, d };

        Size = new Vector2(Mathf.Abs(Points[2].x - Points[0].x), Mathf.Abs(Points[2].y - Points[0].y));
        Poz = Vector2.Lerp(Points[0], Points[2], .5f);

        width = Size.x;
        height = Size.y;

        xMin = Points[0].x; yMin = Points[0].y;
        xMax = Points[2].x; yMax = Points[2].y;
    }
    public MyRect(Vector2[] points)
    {
        Points = points;

        Size = new Vector2(Mathf.Abs(Points[2].x - Points[0].x), Mathf.Abs(Points[2].y - Points[0].y));
        Poz = Vector2.Lerp(Points[0], Points[2], .5f);

        width = Size.x;
        height = Size.y;

        xMin = Points[0].x; yMin = Points[0].y;
        xMax = Points[2].x; yMax = Points[2].y;

    }
    public MyRect(Vector2 poz, Vector2 size)
    {
        Poz = poz;
        Size = size;

        var bounds = size / 2;
        Points = new Vector2[4];
        Points[0] = poz - bounds;
        Points[1] = new Vector2(poz.x - bounds.x, poz.y + bounds.y);
        Points[2] = poz + bounds;
        Points[3] = new Vector2(poz.x + bounds.x, poz.y - bounds.y);

        width = Size.x;
        height = Size.y;

        xMin = Points[0].x; yMin = Points[0].y;
        xMax = Points[2].x; yMax = Points[2].y;

    }


}
