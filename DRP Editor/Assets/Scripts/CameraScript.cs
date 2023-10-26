using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public EditingScript editingScript;

    public Transform cameraPos;
    public Transform aimingPoint;
    public Transform zoomPointTrans;

    public Vector3 zoomPoint;

    public float sensibility = 30f;

    float yRotation;
    float xRotation;

    float cameraDistance = -5f;

    public float holdSensibility;

    float mouseX;
    float mouseY;

    public int clickTimes;
    public float resetTimer;

    void Start()
    {

    }

    void Update()
    {
        GetInput();
        Rotate();
        Move();
        Zoom();
    }

    private void GetInput()
    {
        mouseX = Input.GetAxisRaw("Mouse X") * sensibility * Time.deltaTime;
        mouseY = Input.GetAxisRaw("Mouse Y") * sensibility * Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine("ResetClickTimes");
            clickTimes++;
        }

        if (clickTimes >= 2)
        {
            clickTimes = 0;
            if (editingScript.selectionObject != null && editingScript.toolsType == "Selection")
            {
                aimingPoint.position = editingScript.selectionObject.transform.position;
                cameraDistance = -7f;
            }
        }
    }

    IEnumerator ResetClickTimes()
    {
        yield return new WaitForSeconds(resetTimer);
        clickTimes = 0;
    }

    private void Rotate()
    {
        if (Input.GetMouseButton(2) && !Input.GetKey("left shift"))
        {
            yRotation += mouseX;
            xRotation -= mouseY;

            aimingPoint.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        }
    }

    private void Move()
    {
        if (Input.GetMouseButton(2) && Input.GetKey("left shift"))
        {
            aimingPoint.position += (transform.right * mouseX / sensibility * cameraDistance * holdSensibility) + (transform.up * mouseY / sensibility * cameraDistance * holdSensibility);
        }
    }

    private void Zoom()
    {
        cameraDistance -= Input.mouseScrollDelta.y * cameraDistance * 0.1f;
        cameraPos.localPosition = new Vector3(0f, 0f, cameraDistance);
        //aimingPoint.position = Vector3.MoveTowards(aimingPoint.position, zoomPointTrans.position, cameraDistance * Input.mouseScrollDelta.y * -0.1f);
    }
}