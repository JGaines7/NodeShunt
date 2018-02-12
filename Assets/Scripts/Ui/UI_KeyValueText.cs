using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_KeyValueText : MonoBehaviour {


	private Text m_keyTextObj;
	private Text m_valueTextObj;

	[SerializeField]
	private string m_key;
	[SerializeField]
	private string m_value;
	[SerializeField]
	private Color m_color;


	public void setColor(Color color)
	{
		m_color = color;
		m_keyTextObj.color = color;
		m_valueTextObj.color = color;
	}

	public void setKey(string key)
	{
		m_keyTextObj.text = key + ":";
	}

	public void setValue(string value)
	{
		m_valueTextObj.text = value;
	}


	// Use this for initialization
	void Start () {
		
		m_keyTextObj = transform.Find ("Key").GetComponent<Text> ();
		m_valueTextObj = transform.Find ("Value").GetComponent<Text> ();

		setKey (m_key);
		setValue (m_value);
		setColor (m_color);
	}

}
