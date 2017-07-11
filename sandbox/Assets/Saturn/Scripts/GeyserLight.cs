using UnityEngine;
using System.Collections;

public class GeyserLight : MonoBehaviour {
	public GameObject geyser;
	private Vector3 sunRotation, geyserRotation;
	private float angularDifference,colorValue,geyserAngleDif;
	public GameObject sun;
	private Transform mCam;
	private bool disableBillboard;
	// Use this for initialization
	void Start () {
		sun = GameObject.FindGameObjectWithTag("Sun");
		mCam  = Camera.main.transform;

	}
	
	// Update is called once per frame
	void Update () {


		sunRotation = sun.transform.forward;
		geyserRotation = geyser.transform.forward;
		angularDifference = (90 - Vector3.Angle(sunRotation,geyserRotation))*-0.011f;
		if(angularDifference>0f){
			angularDifference *=4f;
		}
		if(angularDifference<0f){
			angularDifference *=-0.4f;
		}
		colorValue = 1f - angularDifference;
		geyser.GetComponent<Renderer>().material.SetColor("_EmisColor",new Color(colorValue,colorValue,colorValue));
		if(!disableBillboard){
			geyser.transform.LookAt(mCam);
			geyser.transform.localRotation = Quaternion.Euler(90, geyser.transform.localRotation.eulerAngles.y,0);
		}
	}

	public void setBillboard(bool value){
		this.disableBillboard = value;
	}
}
