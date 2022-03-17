using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{
    public Material OutlineMaterial;
    public float OutlineSize = 0.04f;

    void Start()
    {
        OutlineMaterial.SetFloat("_OutlineWidth", 0);
    }

    public void IsInteratible()
    {
        OutlineMaterial.SetFloat("_OutlineWidth", OutlineSize);
    } 
    
    public void IsNotInteratible()
    {
        OutlineMaterial.SetFloat("_OutlineWidth", 0);
    }
}
