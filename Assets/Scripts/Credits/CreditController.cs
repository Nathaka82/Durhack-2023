using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditController : MonoBehaviour
{
    private AudioSource AS;
    private Animator animator;

    private float initialVolume;
    private float endTime;
    private float fadeTime = 5;

    private bool endCredits = false;
    // Start is called before the first frame update
    void Start()
    {
        AS = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        initialVolume = AS.volume;
    }

    // Update is called once per frame
    void Update()
    {
        if (endCredits)
        {
            AS.volume = Mathf.Lerp(initialVolume, 0, (Time.time - endTime) / fadeTime);
            if (AS.volume <= 0)
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    public void EndCredits()
    {
        endTime = Time.time;
        endCredits = true;
        Debug.Log("End Credits");
    }
}