using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausableMonoBehavior : MonoBehaviour {

    public static bool m_paused = false;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		if(!m_paused)
        {
            PausableUpdate();
        }
	}

    virtual public void PausableUpdate()
    {
        Debug.Log("Warning! Pausable game object not implementing pausableUpdate!!!");
    }
}
