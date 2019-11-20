using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public AudioClip goalClip;


    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            var audioSource = GetComponent<AudioSource>();
            //1
            var timer = FindObjectOfType<Timer>();
            //2
            GameManager.instance.SaveTime(timer.time);


            if (audioSource != null && goalClip != null)
            {
                audioSource.PlayOneShot(goalClip); 
            }

            GameManager.instance.RestartLevel(0.5f);
        }
    }
}
