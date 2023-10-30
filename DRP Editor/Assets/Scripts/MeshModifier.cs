using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshModifier : MonoBehaviour
{
    public MeshFilter modelToChange;
    public Mesh[] modelToUse;
    public int currentModel;
    public int obstacleId;
    private MeshFilter meshFilter;

    private void Start()
    {
        modelToChange.mesh = modelToUse[currentModel];
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && obstacleId == 2)
        {
            modelToChange.mesh = modelToUse[currentModel];
            currentModel++;
            if (currentModel >= modelToUse.Length)
            {
                currentModel = 0;
            }
        }
    }
}
