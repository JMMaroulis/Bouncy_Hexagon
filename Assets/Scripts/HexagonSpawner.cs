using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonSpawner : MonoBehaviour
{

    public float spawnTimePeriod = 2.0f;
    public GameObject hexagon;

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
        Instantiate(hexagon);
    }
}
