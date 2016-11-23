using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetSound : MonoBehaviour {

    [SerializeField]
    private List<AudioClip> audioClips;

    [SerializeField]
    private AudioSource audioSource;

    void OnCollisionEnter(Collision other)
    {
        //move audio source
        audioSource.transform.position = other.contacts[0].point;

        //adjust volume realitive to velocity
        audioSource.volume = Mathf.Clamp01(other.relativeVelocity.magnitude * 0.2f);

        //adjust pitch
        //audioSource.pitch = Random.Range(0.98f, 1.02f);

        //Randonly chose clip
        audioSource.clip = audioClips[Random.Range(0, audioClips.Count)];

        //play the clip
        audioSource.Play();

    }


}
