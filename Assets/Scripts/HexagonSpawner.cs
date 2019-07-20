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
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if( timer >= spawnTimePeriod)
        {
            timer = 0f;
            CreateHexagon();
        }

    }

    void CreateHexagon()
    {
        GameObject newHexagon = Instantiate(hexagonPrefab);
        Hexagon newHexagonScript = newHexagon.GetComponent<Hexagon>();
        newHexagonScript.startingScale = spawnStartingScale;
        newHexagonScript.shrinkRate = spawnShrinkRate;
        newHexagonScript.rotationRate = spawnRotationRate;
    }
}
