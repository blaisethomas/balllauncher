using UnityEngine;
using System.Collections;

public class BasketDetector : MonoBehaviour
{

    public GameObject Player;

    public GameObject[] Zones;
    Camera camera;
    int zoneIndex = 0; 

    //public Transform Teleport;
    //public Transform[] teleportZone;
    //var teleportZone : Transform[]


    void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody.velocity.y < 0f)
        {
            //GameManager.score++;
            GameManager.score++;

            zoneIndex ++;
            Player.transform.position = Zones[zoneIndex].transform.position;
            //Player.transform.rotation = Zones[zoneIndex].transform.rotation;

            camera = GetComponentInParent<Camera>();
            GameObject hands = GameObject.Find("Hands");
            //hands.transform.position = camera.transform.rotation.ToEulerAngles();
            //Player.transform.position = Teleport.transform.position;

        }
    }
}
