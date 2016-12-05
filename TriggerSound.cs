using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriggerSound : MonoBehaviour
{

    [SerializeField]
    private List<AudioClip> audioClips;

    [SerializeField]
    private AudioSource audioSource;

    void OnTriggerEnter (Collider other)
    {
        //move audio source to location of other gameobject
        audioSource.transform.position = other.transform.position;

        //adjust volume realitive to velocity
        audioSource.volume = Mathf.Clamp01(other.GetComponent<Rigidbody>().velocity.magnitude * 0.2f);

        //adjust pitch
        audioSource.pitch = Random.Range(0.98f, 1.02f);

        //Randonly chose clip
        audioSource.clip = audioClips[Random.Range(0, audioClips.Count)];

        //play the clip
        audioSource.Play();

    }


}
