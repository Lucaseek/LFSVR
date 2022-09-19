using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObservables : MonoBehaviour
{
    public List<GameObject> observables; //list of observable objects
    public GameObject user; //user gameobject
    public GameObject videoPlayer;
    private VideoProgress vprogress; //Get video progress
    private bool movingObj;

    void Start()
    {
        vprogress = videoPlayer.GetComponent<VideoProgress>();
        movingObj = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(vprogress.GetVideoTotalFrames() > 0)
        {
            //Debug.Log(vprogress.GetVideoFrame() + "/" + vprogress.GetVideoTotalFrames());
            EnableObservable(); //Call observable at the correct time/position
        }
    }

    //Enable the observable GO and disable it at the correct frame
    void EnableObservable()
    {
        if(vprogress.GetVideoFrame() > 100 && vprogress.GetVideoFrame() < 600) //Sheep
        {
            observables[0].gameObject.SetActive(true); //enable GO
            //observables[0].gameObject.transform.position = new Vector3(0, 0.8f, -13); //Move GO to position
            MoveObservable(observables[0], new Vector3(0, 0.8f, -13), new Vector3(-28, 0.8f, -13), 'x', -0.03f); //Follow Sheeps on the scene
        }
        else //disable observables
        {
            for (int i = 0; i < observables.Count; i++)
            {
                observables[i].gameObject.SetActive(false); //enable GO
            }
        }
    }

    //Move observable during walk - Dynamic movement
    void MoveObservable(GameObject obs, Vector3 initialPos, Vector3 finalPos, char direction, float frameSpeed)
    {
        float changingXpos = obs.gameObject.transform.position.x;
        if (movingObj == false)
        {
            movingObj = true;
            obs.gameObject.transform.position = initialPos; //Move GO to position
        }

        if(direction.Equals('x')) //If desired motion is X coordinates
        {
            obs.gameObject.transform.position = initialPos; //Move GO to position
            if(changingXpos != finalPos.x) //while position is not the same as finalPosition
            {
                //move observable
                obs.gameObject.transform.position = new Vector3(changingXpos + frameSpeed, obs.gameObject.transform.position.y, obs.gameObject.transform.position.z);

                if (changingXpos < finalPos.x) //if X is smaller than max value
                {
                    obs.gameObject.transform.position = finalPos; //set position to final
                }
            }
            else
            {
                movingObj = false;
            }

        }
        else if (direction.Equals('y')) //If desired motion is Y coordinates
        {

        }
        else if (direction.Equals('z')) //If desired motion is Z coordinates
        {

        }
    }
}
