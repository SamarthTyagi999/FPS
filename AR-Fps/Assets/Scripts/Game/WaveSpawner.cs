using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState
    {
        SPAWNING,WAITING,COUNTING
    }

   [System.Serializable]
   public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public float timeBetweenWaves = 5f;
    public float waveCountdown = 0f;

    private float searchCountdown = 1f;

    private float xPos, zPos;

    private SpawnState state = SpawnState.COUNTING;

    public bool isTutorial;

    private AudioSource audioSource;

    public List<Transform> _enemies;

    private void Start()
    {
        GameUI.instance.DisplayWaveNumber(nextWave);
        waveCountdown = timeBetweenWaves;
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    private void Update()
    {
        if (state == SpawnState.WAITING)
        {
            //Check if the enemies are still alive
            if (!EnemyIsAlive())
            {
                Debug.Log("All Dead");
                WaveCompleted();
            }
            else
            {
                return;
            }
        }


        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed");


        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if(nextWave+1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("All Waves Completed!...Looping");

            //if its tutorial
            if (isTutorial)
            {
                TutorialManager.instance.spawner.SetActive(false);
                TutorialManager.instance.EndTutoialScreen.SetActive(true);
            }
            
        }
        else
        {
            nextWave++;
        }

        GameUI.instance.DisplayWaveNumber(nextWave);
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1.5f;
            //Debug.Log(GameObject.FindGameObjectWithTag("Target").Length);
            if (GameObject.FindGameObjectWithTag("Target") == null)
            {
                Debug.Log("All Dead");
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        GameUI.instance.SetWaveSlider(_wave.count);

        state = SpawnState.SPAWNING;

        for(int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }


        state = SpawnState.WAITING;
        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        //z=3>8,-3>-8,,x=3>6,-3>-6
        float x1 = Random.Range(3f, 6f);
        float x2 = Random.Range(-3f, -6f);
        float z1 = Random.Range(3f, 8f);
        float z2 = Random.Range(-3f, -8f);

        if (Random.Range(0, 2) == 0)
        {
            xPos = x1;
            zPos = z1;
        }
        else
        {
            xPos = x2;
            zPos = z2;
        }


        //xPos = Random.Range(-6f,6f);
        //zPos = Random.Range(-8f, 8f);
        Transform newEnemy = Instantiate(_enemy, new Vector3(xPos,-1.5f,zPos), Quaternion.identity);
        //EnemyContainer.instance.AddEnemy(newEnemy);
        GameManager.instance.spawner.GetComponent<EnemyContainer>().AddEnemy(newEnemy);

    }
}
