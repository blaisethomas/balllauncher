using UnityEngine;
using System.Collections;

public class BasketDetectorMain : MonoBehaviour
{

    public GameObject Player;

    public GameObject[] Zones;
    public ScoreManagerMain scoreManager; 
    Camera camera;
    int zoneIndex = 0;
    string varabar = "hello dubdubdubd"; 

    //public Transform Teleport;
    //public Transform[] teleportZone;
    //var teleportZone : Transform[]


    void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody.velocity.y < 0f)
        {
            //GameManager.score++;
            GameManagerMain.score++;

            zoneIndex ++;
            //Player.transform.position = Zones[zoneIndex].transform.position;
            //Player.transform.rotation = Zones[zoneIndex].transform.rotation;

            //camera = GetComponentInParent<Camera>();
            //GameObject hands = GameObject.Find("Hands");
            //hands.transform.position = camera.transform.rotation.ToEulerAngles();
            //Player.transform.position = Teleport.transform.position;

            Debug.Log("detector");

            /*scoreManager = new ScoreManagerMain();
            //scoreManager.Start(varabar);
            scoreManager.MakeCall("Hoop da da da");
            */
            

        }
    }
}
