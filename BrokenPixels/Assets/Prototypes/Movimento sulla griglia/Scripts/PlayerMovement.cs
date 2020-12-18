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
        if (Input.anyKeyDown)
        {
            GameObject nextPosition = this.gameObject;
            if (Input.GetKeyUp(KeyCode.W))
            {
                nextPosition = FindNextPosition(currentAxisX, currentAxisZ + 1);
            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                nextPosition = FindNextPosition(currentAxisX, currentAxisZ - 1);
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                nextPosition = FindNextPosition(currentAxisX + 1, currentAxisZ);
            }
            else if (Input.GetKeyUp(KeyCode.A))
            {
                nextPosition = FindNextPosition(currentAxisX - 1, currentAxisZ);
            }
            
            Debug.Log(nextPosition.gameObject.name);
            var position = nextPosition.transform.position;
            var endVector = new Vector3(position.x, position.y + offsetY, position.z);
            nextCellCoroutine = StartCoroutine(LerpToNextCell(transform.position, endVector, velocity));
            Debug.Log("End : " + endVector);
        }
    }

    private GameObject FindNextPosition(int nextAxisX, int nextAxisZ)
    {
        var matrixCube = new MatrixCube(nextAxisX, nextAxisZ);
        if (!gridGenerator.AllCubes.ContainsKey(matrixCube))
        {
            Debug.Log("Matrix : non esiste");
        }
        return gridGenerator.AllCubes.ContainsKey(matrixCube) ? gridGenerator.AllCubes[matrixCube].gameObject : this.gameObject;
    }

    private IEnumerator LerpToNextCell(Vector3 start, Vector3 end, float vel, float alpha = 0)
    {
        Debug.Log("Start Coroutine");
        while (alpha < 1)
        {
            alpha += vel;
            Vector3.Lerp(start, end, alpha);
            yield return new WaitForEndOfFrame();
            Debug.Log("Alpha : " + alpha);
        }
        yield return new WaitForEndOfFrame();
        nextCellCoroutine = null;
    }
}
