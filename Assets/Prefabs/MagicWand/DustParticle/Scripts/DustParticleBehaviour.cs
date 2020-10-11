using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DustParticleBehaviour : MonoBehaviour
{

    private MeshRenderer meshRenderer;
    //public Behaviour halo;
    private System.Random rnd = new System.Random();
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        double p = rnd.NextDouble();
        if (p < 0.03)
        {
            float[] rgb = { (float)rnd.NextDouble(), (float)rnd.NextDouble(), (float)rnd.NextDouble() };
            float max = 0;
            int index = -1;
            for(int i=0; i < 3; i++)
            {
                if (rgb[i] > max)
                {
                    max = rgb[i];
                    index = i;
                }
            }
            rgb[index] = 1;
            Color newCol = new Color(rgb[0],rgb[1],rgb[2]);

            meshRenderer.material.SetColor("_Color", newCol);

            SerializedObject halo = new SerializedObject(GetComponent("Halo"));
            halo.FindProperty("m_Color").colorValue = newCol;
            halo.ApplyModifiedProperties();
        }
    }
}
