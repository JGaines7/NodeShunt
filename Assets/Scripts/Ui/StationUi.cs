using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationUi : MonoBehaviour {


    public UI_KeyValueText m_name;
    public UI_KeyValueText m_numPassengers;
    public PassengerContainer m_passengerContainer;


	// Use this for initialization
	void Start () {
        m_passengerContainer.OnPassengerAdded += setUiValues;
        setUiValues();
       

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void setUiValues()
    {
        m_name.setKey("Name");
        m_name.setValue(transform.parent.name);

        m_numPassengers.setKey("Passengers");
        m_numPassengers.setValue(m_passengerContainer.getNumPassengers().ToString());

    }

    void OnDestroy()
    {
        m_passengerContainer.OnPassengerAdded -= setUiValues;
    }
}
