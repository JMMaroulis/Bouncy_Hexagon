using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    public float SecondsRemaining;
    private TextMesh timerText;

    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponent<TextMesh>();
        SecondsRemaining = SceneResetDataTransfer.SecondsRemaining;
    }

    // Update is called once per frame
    void Update()
    {
        //update timer; if time runs out, reset to level 1
        SecondsRemaining -= Time.deltaTime;
        if (SecondsRemaining <= 0.0f)
        {
            SceneResetDataTransfer.ResetToLevelOne();
        }
        
        timerText.text = (SecondsRemaining).ToString("n1");
    }
}
