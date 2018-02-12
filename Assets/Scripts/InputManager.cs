using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Debug class to test line.

public class InputManager : MonoBehaviour {

    public enum InputState {  SELECTING, ADDING, DELETING, MOVING}

    public InputState m_inputState = InputState.SELECTING;

	private TransportLine m_line;
    private LineEditor m_lineEditor;
	public Camera m_camera;
	// Use this for initialization
	void Start () {
		m_line = (TransportLine)FindObjectOfType (typeof(TransportLine));
        m_lineEditor = (LineEditor)FindObjectOfType(typeof(LineEditor));
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			tryAddNode ();
		}
		if (Input.GetMouseButtonDown (1)) {
			tryRemoveNode ();
		}
		//spawn train at highlighted node.
		if (Input.GetKeyDown (KeyCode.T)) {
			GameObject obj = GridUtil.getGameObjectAtCursor (m_camera);
			if (obj != null && obj.layer == LayerMask.NameToLayer("Nodes")) {
				m_lineEditor.addTrainForCurrentLineAtNode(obj, false);
			}
		}
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		}
        if(Input.GetKeyDown(KeyCode.P))
        {
            PausableMonoBehavior.m_paused = !PausableMonoBehavior.m_paused;
        }
        if(Input.GetKeyDown(KeyCode.N))
        {
            m_lineEditor.startNewLine();
        }
	}



	void tryAddNode()
	{
		RaycastHit hit;
		Ray ray = m_camera.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit, 10000, 1 << LayerMask.NameToLayer("Terrain"))) {
			Vector3 point = hit.point;
			point += hit.normal * 0.25f;
			//point.y = 0.5f;
			m_lineEditor.addNodeForCurrentLineAtPosition(GridUtil.posToGridPos(point));
		}
	}

	void tryRemoveNode()
	{

	}

    void selectLineForNode()
    {
        GameObject obj = GridUtil.getGameObjectAtCursor(m_camera);
        //TransportLine = obj.getCop
        //hitcast a node and select the associated route for the node.
        //if node has multiple routes associated, cycle nodes on click.
        //OR popup a UI to select?
    }
}
