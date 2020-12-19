using System;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [Header("Inizializzazione Griglia")]
    [SerializeField] private int columnAxisX = 1;
    [SerializeField] private int rowAxisZ = 1;
    [Space]
    [SerializeField, Range(1f, 2f)] private float offsetAxisX = 1f;
    [SerializeField, Range(1f, 2f)] private float offsetAxisZ = 1f;
    [Space]
    [SerializeField] private Vector3 startPointGenerator = Vector3.zero;
    [SerializeField] private GameObject sampleCubePerfab = null;
    [Space]
    [Tooltip("Tutti i cubi della Griglia")]
    public List<SimpleCubeScript> Cubes;
    public Dictionary<MatrixCube, SimpleCubeScript> AllCubes;
    
    
    private void Awake()
    {
        AllCubes = new Dictionary<MatrixCube, SimpleCubeScript>();
        Cubes = new List<SimpleCubeScript>();
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        for (int i = 0; i < columnAxisX; i++)
        {
            for (int j = 0; j < rowAxisZ; j++)
            {
                var position = CalculatePosition(i, j);
                SetNewSimpleCubeScript(Instantiate(sampleCubePerfab, position, Quaternion.identity).GetComponent<SimpleCubeScript>(), i, j);
            }
        }
    }

    private Vector3 CalculatePosition(int currentRowAxisX, int currentRowAxisZ)
    {
        float x = (startPointGenerator.x + currentRowAxisX) * offsetAxisX;
        float z = (startPointGenerator.z + currentRowAxisZ) * offsetAxisZ;
        return new Vector3(x, startPointGenerator.y, z);
    }

    private void SetNewSimpleCubeScript(SimpleCubeScript script, int currentRowAxisX, int currentRowAxisZ)
    {
        script.gameObject.name = currentRowAxisX.ToString() + " - " + currentRowAxisZ.ToString();
        script.indexX = currentRowAxisX;
        script.indexZ = currentRowAxisZ;
        script.gameObject.transform.SetParent(transform);
        Cubes.Add(script);
        AllCubes.Add(new MatrixCube(currentRowAxisX, currentRowAxisZ), script);
    }
}

[Serializable]
public struct MatrixCube
{
    public int X, Z;

    public MatrixCube(int indexX, int indexZ)
    {
        X = indexX;
        Z = indexZ;
    }
}
