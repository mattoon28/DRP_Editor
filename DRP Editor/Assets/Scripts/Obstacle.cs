using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    public int type;
    public int id;

    public MeshFilter modelToChange;
    public Mesh[] modelToUse;


    public EditingScript editingScript;
    public int idObstacleSelected;

    void Awake()
    {
        SaveSystem.obstacles.Add(this);
    }

    void OnDestroy()
    {
        SaveSystem.obstacles.Remove(this);
    }

    void Start()
    {
        modelToChange.mesh = modelToUse[type];
    }

    void Update()
    {
        if (editingScript.objectSelected)
        {
            idObstacleSelected = editingScript.selectionObject.GetComponent<Obstacle>().id;
        }
        

        if (Input.GetKeyDown(KeyCode.M) && idObstacleSelected == gameObject.GetComponent<Obstacle>().id)
        {
            type++;
            if (type >= modelToUse.Length)
            {
                type = 0;
            }

            modelToChange.mesh = modelToUse[type];
        }

    }
    public void setId(int x)
    {
        id = x;
    }
}
