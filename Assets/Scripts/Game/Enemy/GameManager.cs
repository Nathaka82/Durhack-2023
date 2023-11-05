using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.TimeZoneInfo;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int Credits;

    public GameObject Player;

    public GameObject[] Enemies;
    public GameObject[] Spawns;

    public GameObject[] Objects;
    private List<GameObject> ActiveObjects = new List<GameObject>();

    public GameObject[] ObjectSpawns;
    private List<GameObject> AliveEnemies = new List<GameObject>();

    private int Level;

    private bool PlayerAtExit;

    public Animator anim;

    private bool changingLevel;

    public GameObject RedLight;
    public GameObject GreenLight;

    private bool AllEnemiesDead;

    // Start is called before the first frame update
    void Start()
    {
        Level = 1;
        Credits = Level + 1;
        SpawnEnemies();
        SpawnObjects();

        PlayerAtExit = false;

        changingLevel = false;

        RedLight.SetActive(true);
        GreenLight.SetActive(false);
    }

    void SpawnEnemies()
    {
        for (int i = Enemies.Length - 1; i >= 0; i--)
        {
            if (Enemies[i].GetComponent<Enemy>().CreditCost <= Credits)
            {
                AliveEnemies.Add(Instantiate(Enemies[i], Spawns[Random.Range(0, Spawns.Length)].transform.position, Quaternion.identity));
                Credits -= Enemies[i].GetComponent<Enemy>().CreditCost;
            }
        }
    }

    void SpawnObjects()
    {
        for (int i = 0; i < ObjectSpawns.Length; i++)
        {
            int num = Random.Range(0, 11);
            if (num >= 6)
            {
                ActiveObjects.Add(Instantiate(Objects[Random.Range(0, Objects.Length)], ObjectSpawns[i].transform.position, Quaternion.identity));
            }
        }
    }

    void DestoryObjects()
    {
        foreach (var obj in ActiveObjects)
        {
            Destroy(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!AllEnemiesDead)
        {
            foreach (var enemy in AliveEnemies)
            {
                if (enemy != null)
                {
                    return;
                }
            }
            AllEnemiesDead = true;
        }


        if (AllEnemiesDead)
        {
            RedLight.SetActive(false);
            GreenLight.SetActive(true);
        }

        if (PlayerAtExit && !changingLevel)
        {
            foreach (var enemy in AliveEnemies)
            {
                if (enemy != null)
                {
                    return;
                }
            }

            changingLevel = true;
            NextLevel();
        }
    }

    void OnTriggerEnter(Collider Other)
    {
        if (Other.gameObject.name == "Player")
        {
            PlayerAtExit = true;
        }
    }

    void OnTriggerExit()
    {
        PlayerAtExit = false;
    }

    public void NextLevel()
    {
        StartCoroutine(_NextLevel());
    }

    IEnumerator _NextLevel()
    {
        float time = 0.5f;

        anim.Play("LevelEnd");
        yield return new WaitForSeconds(time);
        DestoryObjects();
        Level += 1;
        Credits = 2 * Level - 1;
        AliveEnemies = new List<GameObject>();
        SpawnEnemies();

        PlayerAtExit = false;

        AllEnemiesDead = false;

        Player.transform.position = new Vector3(-10.5f, 2f, 0f);

        RedLight.SetActive(true);
        GreenLight.SetActive(false);

        SpawnObjects();

        anim.Play("LevelStart");
        changingLevel = false;
    }
}
