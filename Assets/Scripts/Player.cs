using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Camera mainCamera;

    private Rigidbody2D rigidBody2D;
    private float forceMagnitude = 500f;

    public AudioClip NoteC;
    public AudioClip NoteA;
    public AudioClip NoteG;
    public AudioClip NoteE;
    public AudioClip NoteD;
    private List<AudioClip> Notes = new List<AudioClip>();

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();

        Notes.Add(NoteC);
        Notes.Add(NoteA);
        Notes.Add(NoteG);
        Notes.Add(NoteE);
        Notes.Add(NoteD);

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            MoveOnclick();

        }
    }

    void MoveOnclick()
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 relativePosition = mousePosition - playerPosition;
        Debug.Log(mousePosition);
        Debug.Log(playerPosition);

        rigidBody2D.AddForce(relativePosition.normalized * forceMagnitude);
    }

    void OnCollisionEnter2D()
    {
        int index = Mathf.RoundToInt(Random.value * 5);
        audioSource.PlayOneShot(Notes[index]);
    }
}
