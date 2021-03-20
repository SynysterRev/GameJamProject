using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class SpawnerEnemy : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabEnemy = null;
    private Spawn[] sp;
    private int nbCell = 9;
    private int currentWave = 0;
    private int wave = 0;
    private bool canSpawn = true;
    // Start is called before the first frame update
    void Start()
    {
        LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        if(canSpawn)
        {
            TrySpawn();
        }
    }

    private void LoadData()
    {
        TextAsset textFile = Resources.Load<TextAsset>("DataEnemy/Spawner");
        sp = JsonHelper.FromJson<Spawn>(textFile.text);
        Resources.UnloadAsset(textFile);
    }
    private void TrySpawn()
    {
        for (int i = 0; i < 4; ++i)
        {
            int indexWave = (wave + 1) * 4 - 4 + i;
            int indexEnemy = sp[currentWave].waves[indexWave];
            if (indexEnemy != -1)
            {
                GameObject go = Instantiate(prefabEnemy, new Vector2(-1.5f + i, nbCell * Vector2.down.y), Quaternion.identity);
                go.GetComponent<Enemy>().LoadData(indexEnemy);
            }
        }
        wave++;
        canSpawn = false;
    }

    public void StartSpawner()
    {
        canSpawn = true;
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
