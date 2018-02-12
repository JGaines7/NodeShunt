using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//provides a way to create, delete, and modify transport lines. 
//Does NOT provide any visual stuff or parse raw user input. though can tell us if a grid aligned position is valid or not.




public class LineEditor : MonoBehaviour {

    private TransportLine m_activeTransportLine; // the line you are currently editing
    public GameObject m_transportLinePrefab;


    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    public void startNewLine()
    {
        GameObject line = Instantiate(m_transportLinePrefab, this.transform);
        line.name = "Line " + line.GetComponent<TransportLine>().m_lineUid;
        m_activeTransportLine = line.GetComponent<TransportLine>();
    }

    public void selectExistingLine(TransportLine line)
    {
        m_activeTransportLine = line;
    }

    public bool addNodeForCurrentLineAtPosition(Vector3 position)
    {
        Debug.Log("AddNode at pos");
        m_activeTransportLine.addNewNode(position);
        /*
        Can lay a node if:
        Grid square is blank
        Grid square intersects node of another line.  Add existing node to list, don't create a new one.
        Grid square intersects other end if our own line
        */
        return true;
    }
    public bool addTrainForCurrentLineAtNode(GameObject NodeToSpawnAt, bool direction)
    {
        return m_activeTransportLine.addTrain(NodeToSpawnAt, direction);
    }

    bool canPlaceNodeAtPosition(Vector3 position)
    {
        return true;
    }

    void setLineAsActive(TransportLine line)
    {
        m_activeTransportLine = line;
    }



}
