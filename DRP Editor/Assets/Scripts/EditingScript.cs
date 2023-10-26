using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditingScript : MonoBehaviour
{
    public Vector3 screenPosition;
    public Vector3 targetPoint;
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

    void Start()
    {
        toolsType = "Selection";
    }

    void Update()
    {
        GetInput();

        if (toolsType == "Selection")
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
            }
            else if (!Physics.Raycast(mouseRay, out hitData, 8192f, objectsLayer))
            {
                selectionObject = null;
            }
        }
    }

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
            visualiseObject.transform.position = targetPoint;
        }

        if (Input.GetMouseButtonDown(0))
        {
            GameObject NewObject;
            NewObject = Instantiate(addObject, targetPoint, visualiseObject.transform.rotation, objectsGroup);

            NewObject.SetActive(true);
        }
    }

    private void DeleteTool()
    {
        visualiseObject.SetActive(false);

        if (Input.GetMouseButtonDown(0) && targetObject.layer == 7)
        {
            Destroy(targetObject);
            Debug.Log("Deleted " + targetObject.name);           
        }
    }

    public void SelectionToolButton()
    {
        toolsType = "Selection";
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
