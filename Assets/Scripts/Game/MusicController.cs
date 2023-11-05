using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MusicController : MonoBehaviour
{
    private AudioSource musicSource;
    public AudioClip[] Soundtracks;
    // Start is called before the first frame update
    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        musicSource.clip = Soundtracks[Random.Range(0, Soundtracks.Length)];
        musicSource.Play();
    }

    public void Switch()
    {
        musicSource.clip = Soundtracks[Random.Range(0, Soundtracks.Length)];
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
