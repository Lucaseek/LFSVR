using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupObs : MonoBehaviour
{
    public Transform target; //VR headset
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (target != null) //if target exists
        { 
            transform.LookAt(target); //always face the user
            transform.rotation = Quaternion.LookRotation(transform.position - target.position); //rotate to face the user forward
        }
    }
}
