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

    [Header("Arrows")]
    public string ObjectType;
    public GameObject gateGroup;
    public GameObject ladderGroup;
    public int[] arrowDirection;
    public GameObject[] arrowObjects;

    public GameObject ObstacleObject;

    void OnEnable()
    {
        if (gameObject.GetComponent<Obstacle>().isActiveAndEnabled)
        {
            SaveSystem.obstacles.Add(this);
        }
        
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

        ArrowsObstacleType();
    }

    public void setId(int x)
    {
        id = x;
    }


    void ArrowsObstacleType()
    {
        if(type == 0)
        {
            ObjectType = "Gate";
            gateGroup.SetActive(true);
            ladderGroup.SetActive(false);

            arrowObjects = new GameObject[1];

            ObstacleObject = gateGroup;

            // assigner toute la liste aux flèches
            arrowObjects[0] = ObstacleObject.transform.Find("G1").gameObject;
        }

        if (type == 1)
        {
            ObjectType = "Ladder";
            gateGroup.SetActive(false);
            ladderGroup.SetActive(true);

            arrowObjects = new GameObject[3];

            ObstacleObject = ladderGroup;

            // assigner toute la liste aux flèches
            arrowObjects[0] = ObstacleObject.transform.Find("L1").gameObject;
            arrowObjects[1] = ObstacleObject.transform.Find("L2").gameObject;
            arrowObjects[2] = ObstacleObject.transform.Find("L3").gameObject;
        }
    }
}
