using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passenger {

	public float m_timeCreated;
    public int m_destinationNodeId;
    public string name;
    public uint passengerUid;


	public Passenger (int destinationNodeId)
	{
		m_timeCreated = Time.realtimeSinceStartup;
        m_destinationNodeId = destinationNodeId;
        name = "herroh";
        passengerUid = UidGenerator.getNewUid();
	}
}
