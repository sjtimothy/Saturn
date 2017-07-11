using UnityEngine;
using System.Collections;

public class SaturnShadows : MonoBehaviour {

	public GameObject lightSource;
	public float sun_x, sun_y, sun_z;
	// Use this for initialization
	void Start () {
		lightSource = GameObject.FindGameObjectWithTag("Sun");
	}

	// Update is called once per frame
	void Update () {
		sun_x = lightSource.transform.rotation.eulerAngles.x;
		sun_y = lightSource.transform.rotation.eulerAngles.y;
		sun_z = lightSource.transform.rotation.eulerAngles.z;
		transform.rotation = Quaternion.Euler(sun_x, sun_y,sun_z);
	}
}
