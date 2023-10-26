using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureScript : MonoBehaviour
{
    public EditingScript editingScript;
    public Material outlineShader;

    public GameObject selectionObject;
    private MeshRenderer meshRendererObject;

    void Start()
    {
        meshRendererObject = gameObject.GetComponentInChildren<MeshRenderer>();
        editingScript = GameObject.Find("EDITOR MANAGER").GetComponent<EditingScript>();
    }

    void Update()
    { 
        selectionObject = editingScript.selectionObject;

        Material[] mats = meshRendererObject.materials;

        if (gameObject == selectionObject && editingScript.toolsType == "Selection")
        {
            mats[1] = outlineShader;
            Debug.Log(gameObject.name + " is selectioned");
        }

        else if (gameObject != selectionObject && editingScript.toolsType == "Selection")
        {
            mats[1] = null;
            Debug.Log(gameObject.name + " is not selectioned");
        }

        if (editingScript.toolsType != "Selection")
        {
            mats[1] = null;
            selectionObject = null;
        }

        meshRendererObject.materials = mats;

    }
}
