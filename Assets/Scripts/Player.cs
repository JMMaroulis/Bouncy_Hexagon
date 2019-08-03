using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour
{
    public Camera mainCamera;

    private Rigidbody2D rigidBody2D;
    private float forceMagnitude = 500f;
    public Material material;

    public AudioClip NoteC;
    public AudioClip NoteA;
    public AudioClip NoteG;
    public AudioClip NoteE;
    public AudioClip NoteD;
    private List<AudioClip> Notes = new List<AudioClip>();

    private AudioSource audioSource;
    private float pointExtender = 1.1f;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.queriesStartInColliders = false;
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
        if (Input.GetMouseButtonUp(0))
        {
            MoveOnclick();
        }
        RaycastTestThing();


    }

    void MoveOnclick()
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 relativePosition = mousePosition - playerPosition;

        rigidBody2D.AddForce(relativePosition.normalized * forceMagnitude);
        //rigidBody2D.velocity = (relativePosition.normalized * 5f);
    }

    void OnCollisionEnter2D()
    {
        int index = Mathf.RoundToInt(Random.value * 4);
        audioSource.PlayOneShot(Notes[index]);
    }

    void RaycastTestThing()
    {
        //rotation quaternian for getting past points
        //Quaternion extraRotation = new Quaternion(0, 0, degreeRotationExtra, 0);

        //get all hexagon vertices
        //LineRenderer[] allHexagons = Object.FindObjectsOfType<LineRenderer>();
        EdgeCollider2D[] allHexagons = Object.FindObjectsOfType<EdgeCollider2D>();
        Vector2[][] allPoints = new Vector2[allHexagons.Length][];

        for (int i = 0; i < allHexagons.Length; i++)
        {
            allPoints[i] = new Vector2[6];
            //allHexagons[i].GetPositions(allPoints[i]);
            allPoints[i] = allHexagons[i].points;
        }

        //raycast to all vertices, TODO: and also +- 0.02 radians or so
        List<RaycastHit2D> hitList = new List<RaycastHit2D>();

        List<Quaternion> deviations = new List<Quaternion>();
        for (float i = -2f; i <= 2f; i+= 0.1f)
        //for (float i = -0.0006f; i <= 0.0006f; i += 0.0006f)

        {
                deviations.Add(Quaternion.AngleAxis(i, new Vector3(0,0,1)));
        }

        for (int i = 0; i < allPoints.Length; i++)
        {
            //get hex scale, hex rotation
            Vector3 hexScale = allHexagons[i].transform.localScale;
            Quaternion hexRotation = allHexagons[i].transform.rotation;

            for (int j = 0; j < allPoints[i].Length; j++)
            {

                foreach (Quaternion deviation in deviations)
                {
                    //rotate and scale to get from point definition to world space
                    Vector3 hexPointWorld = hexRotation * allPoints[i][j];
                    hexPointWorld = new Vector3((hexPointWorld.x * hexScale.x), (hexPointWorld.y * hexScale.y), hexPointWorld.z * hexScale.z);

                    //relative position to player, raycast, add to results
                    Vector3 direction = hexPointWorld - transform.position;
                    direction = deviation * direction;
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, direction);
                    hitList.Add(hit);
                }

            }
        }

        //sort raycast hit position by angle from Y direction,
        //then convert into vector3 array, prepend player position
        List<RaycastHit2D> hitListSorted = hitList.OrderBy(vector => Vector3.SignedAngle(vector.point - (Vector2)transform.position, new Vector3(0, 1, 0), new Vector3(0, 0, 1))).ToList();
        Vector3[] hitVectorsSorted = new Vector3[hitListSorted.Count + 1];
        hitVectorsSorted[0] = transform.position;

        for (int i = 0; i < hitVectorsSorted.Length-1; i++)
        {
            hitVectorsSorted[i+1] = hitListSorted[i].point;
        }

        //construct one biiiiig polygon out of the resulting raycast collision points
        Mesh shadowOverlay = new Mesh();
        shadowOverlay.vertices = hitVectorsSorted;
        shadowOverlay.triangles = PolynomialTriangles(hitVectorsSorted.Length);
        Debug.Log("Got There!");

        //material.SetColor("_Color", new Color(0f, 0f, 0f, 0f));
        //material.color = new Color(0f, 0f, 0f, 1f);
        //material.shader = shader;


        Graphics.DrawMesh(shadowOverlay, Vector3.zero, Quaternion.identity, material, 20);
        //DrawTestMesh();

    }

    //assumes that centre of polynomial is at index [0] of your vertex list
    int[] PolynomialTriangles(int numVertices)
    {
        int[] triangles = new int[(numVertices - 1) * 3];
        for (int i = 0, j = 0; j < triangles.Length-5; i+=1, j+=3)
        {
            triangles[j] = 0;
            triangles[j + 1] = i + 1;
            triangles[j + 2] = i + 2;
        }
        
        triangles[triangles.Length - 3] = 0;
        triangles[triangles.Length - 2] = numVertices - 1;
        triangles[triangles.Length - 1] = 1;

        return triangles;
    }

    void DrawTestMesh()
    {
        //raycast to 4 corners (this should not be this much fucking work, it really shouldn't)
        GameObject boundingBox = GameObject.FindGameObjectWithTag("GameBoundary");
        EdgeCollider2D boundingBoxcollider = boundingBox.GetComponent<EdgeCollider2D>();
        var bounds = boundingBoxcollider.bounds;
        Vector2 centre = bounds.center;
        float width = bounds.size.x;
        float height = bounds.size.y;

        Vector3 a = new Vector3(centre.x + (0.5f * width), centre.y + (0.5f * height), 0);
        Vector3 b = new Vector3(centre.x + (0.5f * width), centre.y - (0.5f * height), 0);
        Vector3 c = new Vector3(centre.x - (0.5f * width), centre.y - (0.5f * height), 0);
        Vector3 d = new Vector3(centre.x - (0.5f * width), centre.y + (0.5f * height), 0);

        List<RaycastHit2D> hitList = new List<RaycastHit2D>();
        hitList.Add(Physics2D.Raycast(transform.position, a - transform.position));
        hitList.Add(Physics2D.Raycast(transform.position, b - transform.position));
        hitList.Add(Physics2D.Raycast(transform.position, c - transform.position));
        hitList.Add(Physics2D.Raycast(transform.position, d - transform.position));

        List<RaycastHit2D> hitListSorted = hitList.OrderBy(vector => Vector3.SignedAngle(vector.point - (Vector2)transform.position, new Vector3(0, 1, 0), new Vector3(0, 0, 1))).ToList();
        Vector3[] hitVectorsSorted = new Vector3[hitListSorted.Count + 1];
        hitVectorsSorted[0] = transform.position;

        for (int i = 0; i < hitVectorsSorted.Length - 1; i++)
        {
            hitVectorsSorted[i + 1] = hitListSorted[i].point;
        }

        Mesh testMesh = new Mesh();
        //testMesh.vertices = new Vector3[] { transform.position, a, b, c, d };
        testMesh.vertices = hitVectorsSorted;


        testMesh.triangles = new int[] { 0,1,2, 0,2,3, 0,3,4 };
        Graphics.DrawMesh(testMesh, Vector3.zero, Quaternion.identity, material, 30);
    }


}
