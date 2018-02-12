using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasCameraAligner : MonoBehaviour {

    public Canvas m_canvas;
	// Use this for initialization
	void Start () {
        m_canvas = transform.GetComponent<Canvas>();
	}
	
	// Update is called once per frame
	void Update () {
        m_canvas.transform.LookAt(Camera.main.transform.position + Camera.main.transform.forward * 100000f);
	}
}
