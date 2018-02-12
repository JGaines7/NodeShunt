using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankAccount : MonoBehaviour {

	private float m_balance;
	//private string m_owner;

	public delegate void AccountBalanceChanged (BankAccount balance);
	public static event AccountBalanceChanged OnAccountBalanceChanged;


	// Use this for initialization
	void Start () {
	}

	public void initializeBalance(float value)
	{
		m_balance = value;
		emitBalanceChanged ();
		logBalance ();
	}

	public void adjustBalance(float value)
	{
		m_balance += value;
		emitBalanceChanged ();
		logBalance ();
	}

	public float getBalance()
	{
		return m_balance;
	}

	public bool canAfford(float value)
	{
		return value >= m_balance;
	}

	public bool subtractIfAffordable(float value)
	{
		bool rv = false;
		if (m_balance - value >= 0) {
			m_balance -= value;

			rv = true;
		}
		logBalance ();
		emitBalanceChanged ();
		return rv;
	}

	private void logBalance()
	{
		//Debug.Log ("Account balance: " + m_balance);
	}

	private void emitBalanceChanged()
	{
		if (OnAccountBalanceChanged != null) {
			OnAccountBalanceChanged (this);
		}
	}
}
