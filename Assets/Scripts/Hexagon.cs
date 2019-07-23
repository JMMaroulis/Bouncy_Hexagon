using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexagon : MonoBehaviour
{

    public float rotationRate = 5f;
    public float shrinkRate = 0.1f;
    public float startingScale = 8.0f;
    public int rotationDirection = 1;

    // Start is called before the first frame update
    void Start()
    {
        StartingRotation();
        StartingSize();
        //GetComponent<LineRenderer>().widthMultiplier = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        Shrink();
        Rotation();
        KillWhenSmall();
    }

    void Rotation()
    {
        transform.RotateAround(Vector2.zero, new Vector3(0, 0, 1), rotationRate * rotationDirection * Time.deltaTime);
    }

    void Shrink()
    {
        float y = transform.localScale.y - (shrinkRate * Time.deltaTime);
        float z = transform.localScale.z - (shrinkRate * Time.deltaTime);
        float x = transform.localScale.x - (shrinkRate * Time.deltaTime);
        transform.localScale = new Vector3(x, y, z);
    }

    void StartingRotation()
    {
        //randomise starting rotation
        transform.RotateAround(Vector2.zero, new Vector3(0, 0, 1), Random.value * 360);


        //50:50 chance to set rotation positive or negative
        float flip = Random.value;
        if(flip < 0.5)
        {
            rotationDirection = -1;
        }
        else
        {
            rotationDirection = 1;
        }
    }

    void StartingSize()
    {
        transform.localScale = new Vector3(startingScale, startingScale, startingScale);
    }

    void KillWhenSmall()
    {
        if (transform.localScale.x < 0.5 && transform.localScale.y < 0.5)
        {
            Destroy(gameObject);
        }

    }

}
