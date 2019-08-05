using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonSpawner : MonoBehaviour
{

    public float spawnTimePeriod = 2.0f;
    public float spawnRotationRate = 30f;
    public float spawnShrinkRate = 0.1f;
    public float spawnStartingScale = 8.0f;


    public GameObject hexagonPrefab;

    //private Hexagon hexagon;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneResetDataTransfer.HexagonSpawntimePeriod != 0)
        {
            spawnTimePeriod = SceneResetDataTransfer.HexagonSpawntimePeriod;
        }
        if (SceneResetDataTransfer.HexagonShrinkRate != 0)
        {
            spawnShrinkRate = SceneResetDataTransfer.HexagonShrinkRate;
        }
        SpawnStartingHexagons();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if( timer >= spawnTimePeriod)
        {
            timer = 0f;
            CreateHexagon(spawnStartingScale, spawnShrinkRate, spawnRotationRate);
        }

    }

    void CreateHexagon(float scale, float shrinkRate, float rotationRate)
    {
        GameObject newHexagon = Instantiate(hexagonPrefab);
        Hexagon newHexagonScript = newHexagon.GetComponent<Hexagon>();
        newHexagonScript.startingScale = scale;
        newHexagonScript.shrinkRate = shrinkRate;
        newHexagonScript.rotationRate = rotationRate;
    }

    void SpawnStartingHexagons()
    {
        CreateHexagon(4f, spawnShrinkRate, spawnRotationRate);
        CreateHexagon(6f, spawnShrinkRate, spawnRotationRate);
        CreateHexagon(8f, spawnShrinkRate, spawnRotationRate);
    }
}
