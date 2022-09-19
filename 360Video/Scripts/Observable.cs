using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Observable : MonoBehaviour
{
    //Raycasting
    private Transform firePoint;
    LineRenderer laser;
    public float laserWidth;
    RaycastHit hitInfo;
    public float distance;

    //Identifier
    private bool isUserLooking = false;
    private float timer;
    //public GameObject sheepText;
    private DisplayInfoBox infoBoxScript;

    void Start()
    {
        laserWidth = 0.5f;
        firePoint = GetComponent<Transform>();
        laser = GetComponent<LineRenderer>();
        laser.startWidth = laserWidth;
        laser.endWidth = laserWidth;
        laser.enabled = false;
        distance = 100;
        timer = 2f;
        infoBoxScript = GetComponent<DisplayInfoBox>();
    }

    // Update is called once per frame
    void Update()
    {
        ObserveRay();
    }

    public void ObserveRay()
    {
        //Debug.DrawRay(firePoint.position, firePoint.forward * distance, Color.red, 2f);
        laser.SetPosition(0, firePoint.position); //laser start at the front of the headset

        if (Physics.Raycast(firePoint.position, firePoint.forward, out hitInfo, distance))//if it hits the observable objet, distance is limited
        {
            laser.SetPosition(1, hitInfo.point);

            if (hitInfo.collider.tag == "Observable")
            {
                isUserLooking = true; //user is looking at observable obj
                Debug.Log("hit " + hitInfo.collider.name);
                //If observe for 2 seconds
                IdentifyTime();
            }
            else { //Debug.Log("Not Observable");
                isUserLooking = false; //user not looking anymore
            }
        }
        else
        {
            laser.SetPosition(1, firePoint.forward * distance); //if it doesnt hit, the laser keeps going until it reaches the maximum distance
            timer = 2f;
        }

        IEnumerator coroutine = ActivateLaser();
        StartCoroutine(coroutine);
    }

    IEnumerator ActivateLaser()
    {
        laser.enabled = true;

        yield return new WaitForSeconds(5f);

        laser.enabled = false;
    }

    private void identifyObj()
    {
        string filePath = "";
        string description = "";
        Vector3 boxPos = new Vector3(0, 0, 0);
        if (hitInfo.collider.name == "SheepID")
        {
            Debug.Log("You are observing a SHEEP");
            //select image and Description
            filePath = "Assets/Materials/SheepImg.jpg";
            description = "Sheeps are ruminant mammals, typically kept as livestock.\n" +
                "One of the earliest animals to be domesticated for agricultural purposes, sheep " +
                "are raised for fleeces, meat (lamb, hogget or mutton) and milk. A sheep's wool is " +
                "the most widely used animal fiber, and is usually harvested by shearing.";
            //info Box position
            boxPos = new Vector3(-1, 0, 0);
            //Edit information to match Sheep (image/info)
            infoBoxScript.DisplayBox(filePath, "Sheep", description, boxPos);

        }
    }

    private void IdentifyTime()
    {
        if(isUserLooking == true)
        {
            timer -= Time.deltaTime; //reduce timer
            Debug.Log(timer);
            if(timer < 0)
            {
                timer = 0;
                identifyObj();
            }
        }
        else { timer = 2; }
        
    }

    // Start the collision timer when user enters
    /*void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Observable")
        {
            Debug.Log("Player Entered");
            isUserLooking = true;
        }

    }
    // Check if the player is still at location, if they are spawn our secret item
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Observable" && isUserLooking == true)
        {
            Debug.Log("Countdown not done yet");
            if (timer <= 0)
            {
                Debug.Log("SPAWN CHEST");
                identifyObj();
            }

        }
    }
    // If the player is not colliding reset our timer
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Observable")
        {
            Debug.Log("Player Exited");
            isUserLooking = false;
        }
    }*/
}
