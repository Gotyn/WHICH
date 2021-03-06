﻿using UnityEngine;
using System.Collections;

public class MithionDropTimer : MonoBehaviour {
    HolderTest bbHolder;
    TextMesh text;
    MeshRenderer meshRenderer;
    void Start() {
        bbHolder = FindObjectOfType<HolderTest>();
        text = GetComponentInParent<TextMesh>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if (bbHolder.holdingPlayer)
        {
            Vector3 dir = Camera.main.transform.position - transform.position;
            dir.x = dir.z = 0.0f;
            transform.LookAt(Camera.main.transform.position - dir);
            transform.rotation = (Camera.main.transform.rotation);
            meshRenderer.enabled = true;
            text.text = Mathf.Round(5 - bbHolder.elapsedTime).ToString();
        }
        else
        {
            meshRenderer.enabled = false;
        }
    }
}
