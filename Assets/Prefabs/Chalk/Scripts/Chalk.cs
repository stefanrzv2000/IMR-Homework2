using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Chalk : MonoBehaviour
{
    public VRTK_InteractableObject linkedObject;
    public Transform tip;

    private LineRenderer line;
    private bool beingUsed = false;

    private void OnEnable()
    {
        linkedObject = (linkedObject == null ? GetComponent<VRTK_InteractableObject>() : linkedObject);

        if (linkedObject != null)
        {
            linkedObject.InteractableObjectUsed += ChalkUsed;
            linkedObject.InteractableObjectUnused += ChalkUnused;
        }
    }

    private void ChalkUnused(object sender, InteractableObjectEventArgs e)
    {
        beingUsed = false;
    }

    private void ChalkUsed(object sender, InteractableObjectEventArgs e)
    {
        beingUsed = true;
        line = new GameObject().AddComponent<LineRenderer>();
        line.startWidth = 0.05f;
        line.endWidth = 0.05f;
        line.material = linkedObject.GetComponent<MeshRenderer>().material;
        line.positionCount = 1;
        Vector3[] pos = { tip.position };
        line.SetPositions(pos);
        

    }

    private void FixedUpdate()
    {
        if (beingUsed)
        {
            line.positionCount++;
            line.SetPosition(line.positionCount - 1, tip.position);
        }
    }
}
