using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Char : MonoBehaviour
{

    NavMeshAgent agent;

    void Start()
    {


        //UpAnim();


    }

    //float Speed = .02f;
    void Update()
    {
        if (agent.isOnOffMeshLink)
        {
            //agent.updateRotation = false;
            //agent.link
        }
        //Debug.Log(agent.desiredVelocity);

        //transform.Translate(Vector3.up * agent.desiredVelocity.y * Speed);
    }






}
