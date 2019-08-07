using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelText : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        TextMesh levelText = GetComponent<TextMesh>();
        levelText.text = "Level " + SceneResetDataTransfer.currentLevel.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
