using UnityEngine;
using System.Collections;

public class ControllerManager : MonoBehaviour {

    public GameObject controllerPivot;
    private GameObject player;

    void Awake()
    {
        //player = GameObject.Find("Player");
        //controllerPivot.transform.localRotation = player.transform.rotation;
    }

    void Update()
    {
        UpdatePointer();
    }

    private void UpdatePointer()
    {
        
        controllerPivot.SetActive(true);
        controllerPivot.transform.rotation = GvrController.Orientation;
        
    }
}
