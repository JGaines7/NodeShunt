using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//component that can transfer and store passengers. Obvious uses are for trains and stations.

public class PassengerContainer : MonoBehaviour {
    [SerializeField]
	private List<Passenger> m_passengers;
	public int m_maxCapacity;


    public delegate void PassengerAdded();

    //nonstatic. Maybe have both types?
    public event PassengerAdded OnPassengerAdded;

    public int getNumPassengers() { return m_passengers.Count; }



    private void Awake()
    {
        m_passengers = new List<Passenger>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //transfer passenger from this container to another. return true if successful.
	public bool transferPassenger(Passenger passenger, PassengerContainer newContainer)
	{
		bool rv = false;
        //find passenger in our list;
        int passengerIndex = getIndexOfPassenger(passenger);
        if(passengerIndex != -1)
        {
            if (newContainer.addPassenger(passenger))
            {
                rv = true;
            }
        }

		return rv;

	}


	private int getIndexOfPassenger(Passenger passenger)
	{
        return m_passengers.FindIndex(psngr => psngr.passengerUid == passenger.passengerUid);
	}

	public bool addPassenger(Passenger passenger)
	{
		bool rv = false;
		if (m_passengers.Count < m_maxCapacity) {
			m_passengers.Add (passenger);
            if(OnPassengerAdded != null)
            {
                OnPassengerAdded();
            }
			rv = true;
		}
		return rv;
	}
}
