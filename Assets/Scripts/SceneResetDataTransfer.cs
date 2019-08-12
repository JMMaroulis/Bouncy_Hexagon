using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;


public static class SceneResetDataTransfer
{
    public static float HexagonSpawntimePeriod = 0f;
    public static float HexagonShrinkRate = 0f;
    public static int currentLevel = 1;
    public static int SecondsRemaining = 10;

    public static void ResetToLevelOne()
    {
        HexagonSpawntimePeriod = 0f;
        HexagonShrinkRate = 0f;
        currentLevel = 1;
        SecondsRemaining = 10;
        SceneManager.LoadScene("HexagonScene");
    }

}