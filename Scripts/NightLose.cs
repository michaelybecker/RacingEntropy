using UnityEngine;
using System.Collections;


public class NightLose : MonoBehaviour {
	private float currentTime;
	// Use this for initialization
	void Start () {
		GameObject.Find("Weather").GetComponent<InstantGoodDay>().StopTime();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Global.lose) 
		{
			GameObject.Find ("Weather").GetComponent<InstantGoodDay> ().PassTime ();
			currentTime = GameObject.Find ("Weather").GetComponent<InstantGoodDay> ().GetNumericHour();
			Debug.Log (currentTime);
			if (currentTime > 19.5) 
			{
				GameObject.Find ("Weather").GetComponent<InstantGoodDay> ().StopTime ();
			}
			//			GameObject.Find ("Weather").GetComponent<InstantGoodDay> ().SetNumericHour (0000);
			//			Debug.Log (GameObject.Find("Weather").GetComponent<InstantGoodDay>().GetMilitaryHour());
			
		} 
		else 
		{
			GameObject.Find ("Weather").GetComponent<InstantGoodDay> ().SetNumericHour (12);
		}
		
	}
}