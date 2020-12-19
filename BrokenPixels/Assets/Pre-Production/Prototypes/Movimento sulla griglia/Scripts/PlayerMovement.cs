using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GridGenerator gridGenerator;
    [Header("Assi")]
    [SerializeField] private int currentAxisX;
    [SerializeField] private int currentAxisZ;
    [SerializeField] private int offsetY = 1;
    [Space]
    [SerializeField] private float velocity = 0.05f;
    private Coroutine nextCellCoroutine;

    private void Awake()
    {
        nextCellCoroutine = null;
        GetComponent<MeshRenderer>().enabled = false; //FIX
    }

    private void Start()
    {
        var script = gridGenerator.AllCubes[new MatrixCube(0, 0)];
        var position = script.gameObject.transform.position;
        transform.position = new Vector3(position.x, position.y + offsetY, position.z);
        currentAxisX = script.indexX;
        currentAxisZ = script.indexZ;
        GetComponent<MeshRenderer>().enabled = true; //FIX
    }

    private void Update()
    {
        if (nextCellCoroutine == null)
        {
            Move();    
        }
    }

    private void Move()
    {
        if (Input.anyKey)
        {
            Debug.Log("Input");
            GameObject nextPosition = null;
            if (Input.GetKeyDown(KeyCode.W))
            {
                nextPosition = FindNextPosition(currentAxisX, currentAxisZ + 1);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                nextPosition = FindNextPosition(currentAxisX, currentAxisZ - 1);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                nextPosition = FindNextPosition(currentAxisX + 1, currentAxisZ);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                nextPosition = FindNextPosition(currentAxisX - 1, currentAxisZ);
            }

            if (nextPosition == null) return;
            
            var position = nextPosition.transform.position;
            var endVector = new Vector3(position.x, position.y + offsetY, position.z);
            nextCellCoroutine = StartCoroutine(LerpToNextCell(transform.position, endVector, velocity));
        }
    }

    private GameObject FindNextPosition(int nextAxisX, int nextAxisZ)
    {
        var matrixCube = new MatrixCube(nextAxisX, nextAxisZ);
        if (gridGenerator.AllCubes.ContainsKey(matrixCube))
        {
            Debug.Log(gridGenerator.AllCubes[matrixCube].ToString());
            currentAxisX = nextAxisX;
            currentAxisZ = nextAxisZ;
        }
        else
        {
            Debug.Log("Matrix : non esiste");
        }
        return gridGenerator.AllCubes.ContainsKey(matrixCube) ? gridGenerator.AllCubes[matrixCube].gameObject : null;
    }

    private IEnumerator LerpToNextCell(Vector3 start, Vector3 end, float vel, float alpha = 0)
    {
        while (alpha < 1)
        {
            alpha += vel;
            transform.position = Vector3.Lerp(start, end, alpha);
            yield return new WaitForEndOfFrame();
        }
        nextCellCoroutine = null;
        Debug.Log("End Coroutine");
    }
}
