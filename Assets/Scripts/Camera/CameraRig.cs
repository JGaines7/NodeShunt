using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRig : MonoBehaviour {

	// Use this for initialization
	//private Camera m_camera;

	private Quaternion m_desiredRotation;
	void Start () {
		//m_camera = transform.GetComponentInChildren<Camera> ();
		m_desiredRotation = transform.rotation;
	}
		
	void Update () {
		//TODO route thru input manager
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			m_desiredRotation = m_desiredRotation * Quaternion.Euler (0, 90, 0);
		}
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			m_desiredRotation = m_desiredRotation * Quaternion.Euler (0, -90, 0);
		}

		transform.rotation = Quaternion.Slerp (transform.rotation, m_desiredRotation, Time.deltaTime * 8);
	}
}
