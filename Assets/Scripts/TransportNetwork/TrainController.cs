using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//top level controller for a train. Manages travelling to destinations and broadcasting event information (arrival, departure, etc)
public class TrainController : PausableMonoBehavior {

	private TrainDefs.TrainState m_state;

	private TransportLine m_transportLine;

	[SerializeField]
	private bool m_direction;
	private GameObject m_currentNode;
	private GameObject m_targetNode;


	private float m_speed = 0.08f;



	public void setDirection(bool direction)
	{
		m_direction = direction;
	}
	public void setCurrentNode(GameObject node)
	{
		m_currentNode = node;
		transform.position = node.transform.position;
	}
	public void setParentLine (TransportLine line)
	{
		m_transportLine = line;
	}

	// Use this for initialization
	void Start () {
		m_state = TrainDefs.TrainState.ReadyToDepart;
	}
	
	// Update is called once per frame
	public override void PausableUpdate () {
		if (m_state == TrainDefs.TrainState.ReadyToDepart) {
			depart ();
			
		} else if (m_state == TrainDefs.TrainState.Travelling) {
			travel ();
		} else if (m_state == TrainDefs.TrainState.Arriving) {
			arriveAtNode ();
		}
	}
		


	//basic travel to next node. stop at all nodes. //TODO this will be upgraded to travelling 'routes'
	void travel()
	{
  

		transform.LookAt (m_targetNode.transform.position);
		Vector3 dir = m_targetNode.transform.position - transform.position;
		if (dir.magnitude < m_speed)
        {
			transform.position = m_targetNode.transform.position;
            //If at end of the line, arrive
			m_state = TrainDefs.TrainState.Arriving;
        }
        else
        {
            dir.Normalize();
            dir *= m_speed;
            transform.position += dir;
        }
        
    }

	void depart()
	{
		Unitilities.Tuples.Tuple<GameObject, bool> val = m_transportLine.getNextNode (m_currentNode, m_direction);
		m_targetNode = val.first;
		m_direction = val.second;
		m_state = TrainDefs.TrainState.Travelling;
	}

	void arriveAtNode()
	{
		Debug.Log ("We have arrived! Do some cleanup maybe");
		m_currentNode = m_targetNode;
		m_targetNode = null;
		m_state = TrainDefs.TrainState.ReadyToDepart;

	}


    //emit arrived at station. Turn if 

}
