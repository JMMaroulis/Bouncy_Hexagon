﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetBoundary : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            //data transfer to next iteration
            float spawnTimePeriod = GameObject.Find("HexagonSpawner").GetComponent<HexagonSpawner>().spawnTimePeriod;
            float spawnShrinkRate = GameObject.Find("HexagonSpawner").GetComponent<HexagonSpawner>().spawnShrinkRate;

            SceneResetDataTransfer.HexagonSpawntimePeriod = (spawnTimePeriod -= 0.1f);
            SceneResetDataTransfer.HexagonShrinkRate = (spawnShrinkRate += 0.025f);

            SceneManager.LoadScene("HexagonScene");
        }
    }
}
