using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//defines a list of nodes and provides interface to modify it and get useful info from.
//Treat it more like a data container that other classes can work from.

public class TransportLine : MonoBehaviour {

	enum AddDir { Front, Back};

	public GameObject NodePrefab;
	public GameObject TrainPrefab;

	private List<GameObject> m_nodes;
	private List<GameObject> m_trains;

	private Transform m_nodesContainerTransform;
	private Transform m_trainsContainerTransform;

	//properties of the line
	private bool m_isLoop;
	private AddDir m_addDirection;

    public uint m_lineUid;

	void Awake()
	{
		m_isLoop = false;
		m_addDirection = AddDir.Back;
		m_nodes = new List<GameObject> ();
		m_trains = new List<GameObject> ();
        m_lineUid = UidGenerator.getNewUid();
    }
	// Use this for initialization
	void Start () {
		m_nodesContainerTransform = gameObject.transform.Find ("Nodes");
		m_trainsContainerTransform = gameObject.transform.Find ("Trains");
	}
	
	// Update is called once per frame
	void Update () {
		updateVisualLines ();
	}

	void updateVisualLines()
	{
		//update linerender for child nodes.
		LineRenderer ren;
		for (int i = 0; i < m_nodes.Count-1; i++) {
			ren = m_nodes [i].GetComponent<LineRenderer> ();
			ren.SetPosition (0, m_nodes [i].transform.position);
			ren.SetPosition (1, m_nodes [i+1].transform.position);
			ren.enabled = true;

		}
		if (m_isLoop) {
			ren = m_nodes [m_nodes.Count-1].GetComponent<LineRenderer> ();
			ren.SetPosition (0, m_nodes [m_nodes.Count - 1].transform.position);
			ren.SetPosition (1, m_nodes [0].transform.position);
			ren.enabled = true;
		}

	}

	//Return the next node gameobject and the direction the next node is in.
	public Unitilities.Tuples.Tuple<GameObject, bool> getNextNode(GameObject currentNode, bool currentDirection)
	{
		Unitilities.Tuples.Tuple<GameObject, bool> rv = new Unitilities.Tuples.Tuple<GameObject, bool> (null, false);
		rv.second = currentDirection;
		int currentIndex = gameObjectToNodeIndex (currentNode);
		int newIndex = currentIndex;
		if (currentDirection == false) {
			if (currentIndex + 1 >= m_nodes.Count) {
				if (m_isLoop) {
					newIndex = 0;
				} else {
					
					newIndex = currentIndex - 1;
					rv.second = !currentDirection;
				}
			} else {
				
				newIndex = currentIndex + 1;
			}
		} else {
			if (currentIndex - 1 < 0) {
				if (m_isLoop) {
					newIndex = m_nodes.Count - 1;
				} else {
					
					newIndex = currentIndex + 1;
					rv.second = !currentDirection;

				}
			} else {
				
				newIndex = currentIndex - 1;
			}
		}

		rv.first = m_nodes [newIndex];
		return rv;
	}

	public bool getNodeIsEol(GameObject node)
	{
		bool rv = false;
		if (!m_isLoop && (node == m_nodes [0] || node == m_nodes [m_nodes.Count - 1])) {
			rv = true;
		}
		return rv;
	}

	private int gameObjectToNodeIndex(GameObject obj)
	{
		int rv = -1;
		for (int i = 0; i < m_nodes.Count; i++) {
			if (m_nodes [i] == obj) {
				rv = i;
				break;
			}
		}
		return rv;
	}

    //add a node that is a station or an overlapping node on another line
    public bool addExistingNode(Vector3 position)
    {

        return true;
    }

	public bool addNewNode(Vector3 position)
	{
		bool rv = false;
		//TODO: check for validity here or at a higher layer?
		//for now accept unless we have closed a loop
		if (newNodePositionWillFormLoop (position)) {
			m_isLoop = true;
		} else if(!m_isLoop) {
			if (m_addDirection == AddDir.Back) {
				m_nodes.Add (Instantiate (NodePrefab, position, Quaternion.identity,m_nodesContainerTransform));
			} else {
				m_nodes.Insert (0, (Instantiate (NodePrefab, position, Quaternion.identity,m_nodesContainerTransform)));
			}
			rv = true;
			updateVisualLines ();
		}

		return rv;
	}

	private bool newNodePositionWillFormLoop(Vector3 pos)
	{
		bool rv = false;
		if ((m_nodes.Count > 0) &&
			((m_addDirection == AddDir.Back && pos == m_nodes [0].transform.position) ||
				(m_addDirection == AddDir.Front && pos == m_nodes [m_nodes.Count - 1].transform.position))) {
			rv = true;
		}
		return rv;
	}
	public bool addTrain(GameObject NodeToSpawnAt, bool direction)
	{
		//I wonder if a train factory would make sense, pass it in here.
		//for now we can handle making trains in the transport
		GameObject train = Instantiate (TrainPrefab, NodeToSpawnAt.transform.position, Quaternion.identity,m_trainsContainerTransform);
		TrainController controller = train.GetComponent<TrainController>();
		controller.setDirection(direction);
		controller.setParentLine(this);
		controller.setCurrentNode(NodeToSpawnAt);
		m_trains.Add (train);

		return true;
	}
}
