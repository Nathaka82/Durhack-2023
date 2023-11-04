using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenTransition : MonoBehaviour
{
    private Animator transition;

    public float transitionTime = 1.1f;

    // Start is called before the first frame update
    void Start()
    {
        transition = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex != 0)
        {
            LoadLevel(0);
        }
    }

    public void LoadLevel(int LevelIndex)
    {
        StartCoroutine(_LoadLevel(LevelIndex));
    }

    IEnumerator _LoadLevel(int LevelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(LevelIndex);
    }
}
