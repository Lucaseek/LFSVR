using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class DisplayInfoBox : MonoBehaviour
{
    public GameObject infoCanvas; //get canvas
    private Observable obsScript; //observable script
    public Transform boxInfoGO; //box info canvas position

    //Box references
    public Image boxImage;
    public Text boxName;
    public Text boxDesc;

    void Start()
    {
        infoCanvas.SetActive(false);
        obsScript = GetComponent<Observable>();
    }

    public void DisplayBox(string image, string name, string description, Vector3 position)
    {
        infoCanvas.SetActive(true);
        boxImage.sprite = (Sprite)UnityEditor.AssetDatabase.LoadAssetAtPath(image, typeof(Sprite)); //Set image
        boxName.text = name; //set name
        boxDesc.text = description; //set description
        boxInfoGO.transform.position = position;
    }

    public void DeactivateBox()
    {
        if(infoCanvas.activeSelf)
        {
            infoCanvas.SetActive(false);
        }
        else
        {
            infoCanvas.SetActive(true);
        }
    }
}
