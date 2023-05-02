using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawn : MonoBehaviour
{
    [Header("Enemy Set")]
    [Tooltip("Enemy to Spawn")]
    public GameObject Prefab;
    [Tooltip("Location To Spawn At")]
    public Transform Location;

    [Header("Waves")]
    [Tooltip("Enemies are spawned when game start, will not work if turned off.")]
    public bool StartSpawning = true;
    public int AmountToSpawnPerWave = 3;
    [Min(1)]
    public float timeBetweenSpawns = 2;
    private float timer = 0;
    private bool firstTimeEnabled = false;

    public bool InfiniteWaves = false;
    [Min(1)]
    public int MaxWaves = 1;
    private int CurrentWave = 0;
    private bool wavesEnded = false;

    [Tooltip("Triggers when the last enemy are spawned.")]
    public UnityEvent onSpawningComplete;
    [Tooltip("Triggers when the last enemy are destroyed.")]
    public UnityEvent onAllObjectsDestroyed;


    private List<GameObject> spawnedObjects;
    private int amountOfTotalObjects = 0;
    private int amountOfDestroyedObjects = 0;
    private bool allDeadCalledOnce = false;

    void Start()
    {
        spawnedObjects = new List<GameObject>();
    }

    void Update()
    {
        if (StartSpawning)
        {
            if (!firstTimeEnabled)
            {
                amountOfTotalObjects = MaxWaves * AmountToSpawnPerWave;
                Spawn();

                firstTimeEnabled = true;
            }

            if (CurrentWave < (MaxWaves - 1))
            {
                timer += Time.deltaTime;
                if (timer > timeBetweenSpawns)
                {
                    Spawn();
                    timer = 0;
                    if (!InfiniteWaves)
                        CurrentWave++;
                }
            }
        }

        if (!wavesEnded && CurrentWave == (MaxWaves - 1))
        {
            wavesEnded = true;
            //Debug.Log("waves ended");
            onSpawningComplete.Invoke();
        }

        if (!InfiniteWaves)
        {
            amountOfDestroyedObjects = 0;
            for (int y = 0; y < spawnedObjects.Count; y++)
            {
                if (spawnedObjects[y] == null)
                {
                    amountOfDestroyedObjects++;
                }
            }

            if (amountOfTotalObjects == amountOfDestroyedObjects)
            {
                if (allDeadCalledOnce == false)
                {
                    allDeadCalledOnce = true;
                    onAllObjectsDestroyed.Invoke();
                    //Debug.Log("All Destroyed");
                }
            }
        }
    }

    public void Spawn()
    {
        if (Prefab)
            for (int x = 0; x < AmountToSpawnPerWave; x++)
            {
                spawnedObjects.Add(Instantiate(Prefab, Location.transform.position, Location.transform.rotation));
            }

    }
}
