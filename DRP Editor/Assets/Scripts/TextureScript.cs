using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureScript : MonoBehaviour
{
    public EditingScript editingScript;

    public Material selectionMaterial;
    public Material nullMaterial;

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
            mats[0] = selectionMaterial;
            Debug.Log(gameObject.name + " is selectioned");
        }

        else if (gameObject != selectionObject && editingScript.toolsType == "Selection")
        {
            mats[0] = nullMaterial;
            Debug.Log(gameObject.name + " is not selectioned");
        }

        if (editingScript.toolsType != "Selection")
        {
            mats[0] = nullMaterial;
            selectionObject = null;
        }

        meshRendererObject.materials = mats;

    }
}
