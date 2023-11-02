using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class EditingScript : MonoBehaviour
{
    public Vector3 screenPosition;
    public Vector3 targetPoint;
    public Vector3 addTargetPoint;
    public LayerMask terrainLayer;
    public LayerMask objectsLayer;

    public GameObject addObject;
    public GameObject visualiseObject;
    public GameObject targetObject;
    public GameObject selectionObject;

    public Transform objectsGroup;
    public Transform threeDimensionCursor;

    public float objectYRotation;

    public float rotationSensibility;

    public string toolsType;

    public RaycastHit hitData;
    public RaycastHit selectionHitData;
    public Ray mouseRay;



    float mouseX;
    float mouseY;

    public bool objectSelected;

    void Start()
    {
        toolsType = "Select";
    }

    void Update()
    {

        GetInput();

        if (toolsType == "Select")
        {
            SelectionTool();
        }

        else if (toolsType == "Add")
        {
            AddTool();
        }

        else if (toolsType == "Delete")
        {
            DeleteTool();
        }
    }

    private void GetInput()
    {
        mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime;
        mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime;

        screenPosition = Input.mousePosition;

        mouseRay = Camera.main.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(mouseRay, out hitData, 8192f, terrainLayer | objectsLayer))
        {
            targetPoint = hitData.point;
            targetObject = hitData.transform.gameObject;
        }

       

        threeDimensionCursor.position = targetPoint;
    }

    private void SelectionTool()

    {
        visualiseObject.SetActive(false);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(mouseRay, out selectionHitData, 8192f, objectsLayer))
            {
                selectionObject = selectionHitData.transform.gameObject;
                objectSelected = true;
            }
            else if (!Physics.Raycast(mouseRay, out hitData, 8192f, objectsLayer))
            {
                selectionObject = null;
                objectSelected = false;
            }
        }

        if(Input.GetKeyDown(KeyCode.Delete) && selectionObject != null)
        {
            Destroy(selectionObject);
            objectSelected = false;
        }
    }

    public int idObstacle;
    
    private void AddTool()
    {
        visualiseObject.SetActive(true);

        if (Input.GetMouseButton(1))
        {
            objectYRotation -= mouseX * rotationSensibility;
            visualiseObject.transform.rotation = Quaternion.Euler(visualiseObject.transform.rotation.x, objectYRotation, visualiseObject.transform.rotation.z);
        }

        if (!Input.GetMouseButton(2) && !Input.GetMouseButton(1))
        {
            visualiseObject.transform.position = addTargetPoint;
        }

        if (Physics.Raycast(mouseRay, out RaycastHit addHitData, 8192f, terrainLayer))
        {
            addTargetPoint = addHitData.point;

            if (Input.GetMouseButtonDown(0))
            {
                GameObject NewObject;
                NewObject = Instantiate(addObject, addTargetPoint, visualiseObject.transform.rotation, objectsGroup);
                NewObject.GetComponent<Obstacle>().setId(idObstacle++);

                NewObject.SetActive(true);
            }
        }
    }

    private void DeleteTool()
    {
        visualiseObject.SetActive(false);

        if (Input.GetMouseButtonDown(0) && targetObject.layer == 7)
        {
            Destroy(targetObject);
            objectSelected = false;
            Debug.Log("Deleted " + targetObject.name);           
        }
    }

    public void SelectionToolButton()
    {
        toolsType = "Select";
    }

    public void AddToolButton()
    {
        toolsType = "Add";
    }

    public void DeleteToolButton()
    {
        toolsType = "Delete";
    }
}
