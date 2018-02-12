using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class defines how and when to spawn new passengers.
//Distributes them across the stations in the network. (Randomly for now)
public class PassengerSpawnManager : PausableMonoBehavior {

    public int m_passengersPerWave = 3;
    public float m_spawnInterval = 0.5f;


    private float m_secondsTillSpawn;

	// Use this for initialization
	void Start () {
        m_secondsTillSpawn = m_spawnInterval;


    }
	
	// Update is called once per frame
	public override void PausableUpdate () {
        m_secondsTillSpawn -= Time.deltaTime;
        if(m_secondsTillSpawn <= 0)
        {
            m_secondsTillSpawn = m_spawnInterval;
            spawnRandomly();
        }

    }

    void spawnRandomly()
    {
        PassengerContainer[] containers = getStationContainers();

        if (containers.Length > 0)
        {
            int containerMaxIndex = containers.Length - 1;
            int randomStationIndex;
            int randomStationDestIndex;
            for (int i = 0; i < m_passengersPerWave; i++)
            {
                randomStationIndex = Random.Range(0, containerMaxIndex + 1);
                randomStationDestIndex = getRandomDestinationStationIndex(randomStationIndex, containerMaxIndex);
                Passenger newPass = new Passenger(randomStationDestIndex);
                containers[randomStationIndex].addPassenger(newPass);
            }
        }
    }

    private int getRandomDestinationStationIndex(int startingIndex, int maxIndex)
    {
        if(maxIndex <= 0)
        {
            return -1;
        }

        int destinationIndex;
        do
        {
            destinationIndex = Random.Range(0, maxIndex + 1);
        } while (destinationIndex == startingIndex);

        return destinationIndex;

    }

    private PassengerContainer[] getStationContainers()
    {
        PassengerContainer[] containers = transform.GetComponentsInChildren<PassengerContainer>();
        return containers;
    }
}
