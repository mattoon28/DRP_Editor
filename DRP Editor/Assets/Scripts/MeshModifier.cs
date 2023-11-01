using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshModifier : MonoBehaviour
{
    public MeshFilter modelToChange;
    public Mesh[] modelToUse;
    public int currentModel;
    public EditingScript editingScript;


    public int idObstacleSelected;
    public int idObstacle;


    private void Start()
    {
        modelToChange.mesh = modelToUse[currentModel];
    }
    void Update()
    {
  
        idObstacleSelected = editingScript.selectionObject.GetComponent<MeshModifier>().idObstacle;

        if (Input.GetKeyDown(KeyCode.M) && idObstacleSelected == gameObject.GetComponent<MeshModifier>().idObstacle) 
        {
            currentModel++;
            if (currentModel >= modelToUse.Length)
                {
                    currentModel = 0;
                }

            modelToChange.mesh = modelToUse[currentModel];
        }

    }
    public void setId(int x)
    {
        idObstacle = x;
    }
}


