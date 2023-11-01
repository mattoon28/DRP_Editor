using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    public int type;
    public int id;

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
        
    }

    void Update()
    {
        
    }
}
