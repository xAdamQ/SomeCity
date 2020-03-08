using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContCube : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    static readonly float Speed = 30;
    void Update()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        var j = Input.GetKeyDown(KeyCode.Space) ? 40 : 0;

        GetComponent<Rigidbody>().AddForce(new Vector3(x, j, y) * Speed);
    }
}
