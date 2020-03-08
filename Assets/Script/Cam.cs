using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    System.Action RightButtonHolding;
    event System.Action Updating;

    [SerializeField] Transform Center;

    static Hashtable PozHT, RotHT, StartAnimHT;

    private void Start()
    {
        transform.position = new Vector3(0, World.Size, -World.Size / 2);
        transform.LookAt(Vector3.zero);

        iTween.MoveFrom(gameObject, StartAnimHT);

        RightButtonHolding = RotArountPlanet;

        StartCoroutine(CaptureControls());
    }

    const float StartAnimTime = 1.5f;
    public static void OneTimeIni()
    {
        PozHT = new Hashtable();
        PozHT.Add("position", Vector3.zero);
        PozHT.Add("islocal", true);
        PozHT.Add("time", 1f);

        RotHT = new Hashtable();
        RotHT.Add("rotation", Vector3.zero);
        RotHT.Add("islocal", true);
        RotHT.Add("time", 1f);

        StartAnimHT = new Hashtable();
        StartAnimHT.Add("position", new Vector3(0, World.Size * 3, -World.Size * 1.5f));
        StartAnimHT.Add("time", StartAnimTime);
        StartAnimHT.Add("delay", .5f);
    }

    static float RotSpeed = 600, ZoomSpeed = .25f;
    IEnumerator CaptureControls()
    {
        yield return new WaitForSeconds(2);

        while (true)
        {
            if (Input.GetKey(KeyCode.I))
            {
                transform.position += transform.forward * ZoomSpeed;
            }
            else if (Input.GetKey(KeyCode.O))
            {
                transform.position -= transform.forward * ZoomSpeed;
            }

            if (Input.GetMouseButton(1)) RightButtonHolding();

            Updating?.Invoke();

            yield return null;
        }
    }


    void RotArountPlanet()
    {
        var z = -Input.GetAxis("Mouse X");
        var x = -Input.GetAxis("Mouse Y");
        var y = -Input.mouseScrollDelta.y;

        transform.parent.Rotate(new Vector3(x * RotSpeed, -z * RotSpeed, y * 300) * Time.deltaTime, Space.Self);
    }
    void RotAroundSelf()
    {
        //var y = Input.GetAxis("Mouse X");
        //var x = Input.GetAxis("Mouse Y");

        //transform.Rotate(new Vector3(-x, y, 0) * RotSpeed * Time.deltaTime, Space.Self);
    }

    static readonly float MoveSpeed = 2f;
    void MoveCam()
    {
        var z = Input.GetAxis("Vertical");
        var x = Input.GetAxis("Horizontal");
        var y = Input.mouseScrollDelta.y;

        transform.position +=
           (ActivePlane.right * x +
            ActivePlane.up * y +
            ActivePlane.forward * z) *
            MoveSpeed;
    }

    Transform ActivePlane;

    byte ViewType = 0;
    public void ToggleView()
    {
        if (ViewType == 0)
        {
            ViewType = 1;

            RaycastHit info;
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            if (Physics.Raycast(ray, out info, 999, World.TerLayers))
            {
                ActivePlane = info.collider.gameObject.transform;
            }
            //get facing up plane (ActivePlane)

            transform.SetParent(ActivePlane.parent, worldPositionStays: true);
            Center.rotation = new Quaternion();

            PozHT["position"] = new Vector3(0, World.Size * .4f, -World.Size * .5f);
            RotHT["rotation"] = new Vector3(60, 0, 0);

            iTween.MoveTo(gameObject, PozHT);
            iTween.RotateTo(gameObject, RotHT);

            RightButtonHolding = RotAroundSelf;
            Updating += MoveCam;
        }//change to plane view
        else
        {
            ViewType = 0;
            RightButtonHolding = RotArountPlanet;

            transform.LookAt(Vector3.zero);
            Center.rotation = transform.rotation;
            transform.SetParent(Center);
            transform.localRotation = new Quaternion();
            //adapt rotation

            Updating -= MoveCam;
        }//change to planet view
    }

    //public void AdjustCamPoz()
    //{
    //    // view the shape to understand
    //    // FOV = setaBy2

    //    var vFOVRad = GetComponent<Camera>().fieldOfView * Mathf.Deg2Rad;
    //    var CamHeightAt1 = Mathf.Tan(vFOVRad * .5f);
    //    var hFOVRad = Mathf.Atan(CamHeightAt1 * Camera.main.aspect) * 2;
    //    //var hFOV = Mathf.Rad2Deg * hFOVRad;
    //    var hFOV = GetComponent<Camera>().fieldOfView;

    //    var l = World.Size;

    //    var camRot = 60;
    //    var seta = hFOV / 2;
    //    var seta2 = 90 - camRot;
    //    var seta5 = 90 - seta - seta2;

    //    var r = l * Mathf.Sin(Mathf.Deg2Rad * seta5);
    //    var n = r / Mathf.Sin(Mathf.Deg2Rad * hFOV);

    //    var poz = new Vector3(0, n * Mathf.Sin(Mathf.Deg2Rad * (90 - seta2 + seta)), -n * Mathf.Cos(Mathf.Deg2Rad * (90 - seta2 + seta)));

    //    transform.localPosition = poz;
    //}

}
