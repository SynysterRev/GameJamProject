using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class SpawnerEnemy : MonoBehaviour
{
    //Spawn
    [SerializeField]
    private GameObject prefabEnemy = null;
    private Spawn[] sp;
    private int nbCell = 9;
    private int currentWave = 0;
    private int wave = 0;
    private float timerSpawn = 0.0f;
    private bool canSpawn = false;
    private GameManager gm = null;
    //waves
    private bool waitNewWave = false;
    private bool startTimerWave = false;
    private float timerBetweenWave = 5.0f;
    private int enemiesLeft = 0;
    private bool endLevel = false;
    private List<Enemy> enemiesList = new List<Enemy>();


    //infinite spawn
    [Header("Infinite")]
    [SerializeField]
    private bool infiniteSpawn = false;
    [SerializeField]
    private AnimationCurve curveDifficulty = null;
    [SerializeField]
    private AnimationCurve curveTimerSpawn = null;
    private WaveTypeScriptable waveType = null;
    private float timerDifficulty = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        if (infiniteSpawn)
        {
            if (canSpawn)
            {
                timerDifficulty += Time.deltaTime;
                InfiniteSpawn();
            }
        }
        else
        {
            if (canSpawn)
            {
                TrySpawn();
            }
            else if (startTimerWave)
            {
                timerBetweenWave -= Time.deltaTime * gm.CustomDT;
                if (timerBetweenWave <= 0.0f)
                {
                    startTimerWave = false;
                    canSpawn = true;
                    timerSpawn = 0.0f;
                }
            }
        }
    }

    private void LoadData()
    {
        if (infiniteSpawn)
        {
            waveType = Resources.Load<WaveTypeScriptable>("DataEnemy/Wave");
        }
        else
        {

            TextAsset textFile = Resources.Load<TextAsset>("DataEnemy/Level" + LevelManager.Instance.IndexLevel + "/Spawner");
            sp = JsonHelper.FromJson<Spawn>(textFile.text);
            Resources.UnloadAsset(textFile);
        }
    }
    private void TrySpawn()
    {
        if (timerSpawn <= 0.0f)
        {
            //spawn one line at the time
            timerSpawn = sp[currentWave].timerBetweenEachSpawn;
            for (int i = 0; i < 4; ++i)
            {
                int indexWave = (wave + 1) * 4 - 4 + i;
                int indexEnemy = sp[currentWave].waves[indexWave];
                if (indexEnemy != -1)
                {
                    enemiesLeft++;
                    GameObject go = Instantiate(prefabEnemy, new Vector2(-1.5f + i, nbCell * Vector2.down.y), Quaternion.identity);
                    Enemy enemy = go.GetComponent<Enemy>();
                    enemy.LoadData(indexEnemy);
                    enemy.OnDeath += UpdateEnemiesLeft;
                    enemiesList.Add(enemy);
                }
            }
            //next line
            wave++;
            //last line spawn then next wave
            if (wave == sp[currentWave].sizeWave)
            {
                currentWave++;
                wave = 0;
                canSpawn = false;
                //wave over then level completed
                if (currentWave == sp.Length)
                {
                    endLevel = true;
                    //end level
                }
                else if (enemiesLeft == 0)
                {
                    timerBetweenWave = 5.0f;
                    startTimerWave = true;
                }
                else
                {
                    waitNewWave = true;
                }
            }
        }
        else
        {
            timerSpawn -= Time.deltaTime;
        }
    }

    private void InfiniteSpawn()
    {
        if (timerSpawn <= 0.0f)
        {
            timerSpawn = curveTimerSpawn.Evaluate(timerDifficulty);
            int index;
            if (curveDifficulty.Evaluate(timerDifficulty) > 0.66f)
            {
                index = UnityEngine.Random.Range(0, waveType.listTypeWave.Count);
            }
            else if (curveDifficulty.Evaluate(timerDifficulty) > 0.33f)
            {
                index = UnityEngine.Random.Range(0, 2);
            }
            else
            {
                index = 0;
            }
            int indexWave = UnityEngine.Random.Range(0, waveType.listTypeWave[index].listWaveDifficulty.Count);
            for (int i = 0; i < 4; ++i)
            {
                int indexEnemy = waveType.listTypeWave[index].listWaveDifficulty[indexWave].indexPositionEnemy[i];
                if (indexEnemy != -1)
                {
                    GameObject go = Instantiate(prefabEnemy, new Vector2(-1.5f + i, nbCell * Vector2.down.y), Quaternion.identity);
                    Enemy enemy = go.GetComponent<Enemy>();
                    enemy.LoadData(indexEnemy);
                    enemy.OnDeath += UpdateEnemiesLeft;
                    enemiesList.Add(enemy);
                }
            }
        }
        else
        {
            timerSpawn -= Time.deltaTime;
        }
    }

    private void UpdateEnemiesLeft(Enemy enemy)
    {
        if (enemiesList.Contains(enemy))
            enemiesList.Remove(enemy);
        enemiesLeft--;
        if (enemiesLeft == 0 & waitNewWave)
        {
            waitNewWave = false;
            timerBetweenWave = 5.0f;
            startTimerWave = true;
        }
        else if (enemiesLeft == 0 && endLevel)
        {
            LevelManager.Instance.ShowEndLevel();
        }
    }

    public void StartSpawner()
    {
        canSpawn = true;
    }

    public void StopSpawner(bool destroyAllEnemy)
    {
        canSpawn = false;
        if (destroyAllEnemy)
        {
            for (int i = 0; i < enemiesList.Count; ++i)
            {
                if (enemiesList[i] != null)
                {
                    enemiesList[i].AutoDestruction();
                }
            }
        }
    }

    [Serializable]
    public class Spawn
    {
        public int idWave;
        public int sizeWave;
        public float timerBetweenEachSpawn;
        public int[] waves;
    }
}

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}
