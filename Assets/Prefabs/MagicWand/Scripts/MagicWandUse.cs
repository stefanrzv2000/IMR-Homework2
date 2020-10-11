using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;



public class MagicWandUse : MonoBehaviour
{

    public VRTK_InteractableObject linkedObject;
    public Transform tip;
    public GameObject dust;
    public float growthRate = 1.01f;

    System.Random rnd = new System.Random();

    private bool beingUsed = false; 

    private void OnEnable()
    {
        linkedObject = (linkedObject == null ? GetComponent<VRTK_InteractableObject>() : linkedObject);

        if (linkedObject != null)
        {
            linkedObject.InteractableObjectUsed += MagicWandUsed;
            linkedObject.InteractableObjectUnused += MagicWandUnused;
        }
    }

    private void OnDisable()
    {
        if (linkedObject != null)
        {
            linkedObject.InteractableObjectUsed -= MagicWandUsed;
            linkedObject.InteractableObjectUnused -= MagicWandUnused;
        }
    }

    private void MagicWandUnused(object sender, InteractableObjectEventArgs e)
    {
        beingUsed = false;
    }

    private void MagicWandUsed(object sender, InteractableObjectEventArgs e)
    {
        ChangeCollor();
        beingUsed = true;
    }

    private void ChangeCollor()
    {
        //Color newCol = new Color(1,0,0);
        
        linkedObject.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color((float)rnd.NextDouble(), (float)rnd.NextDouble(), (float)rnd.NextDouble()));
        
    }

    private void FixedUpdate()
    {
        if (beingUsed)
        {
            double p = rnd.NextDouble();
            if (p < 0.3)
            {
                GenerateDust();
            }
        }
    }

    private void GenerateDust()
    {
        float x = ((float)rnd.NextDouble() - 0.5f) * 0.1f;
        float y = ((float)rnd.NextDouble() - 0.5f) * 0.1f;
        float z = ((float)rnd.NextDouble() - 0.5f) * 0.1f;
        
        Instantiate(dust, tip.position + new Vector3(x, y, z), Quaternion.identity);
        Debug.Log(tip.position);
    }
}
