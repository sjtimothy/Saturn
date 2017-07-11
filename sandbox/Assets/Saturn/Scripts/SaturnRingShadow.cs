using UnityEngine;
using System.Collections;

public class SaturnRingShadow : MonoBehaviour {

	public GameObject lightSource, planet, container, subContainer;
	public float sun_x, sun_y, sun_z, planet_x, planet_y, planet_z, angularDifference, pa1;
	private Projector proj;

	// Use this for initialization
	void Start () {
		//get sun and shadow projector
		lightSource = GameObject.FindGameObjectWithTag("Sun");
		proj = GetComponent<Projector>();
		//proj.fieldOfView = 20f;
	}
	
	// Update is called once per frame
	void Update () {
		//get planet and sun directionnal light coordinates
		sun_x = lightSource.transform.rotation.eulerAngles.x;
		sun_y = lightSource.transform.rotation.eulerAngles.y;
		sun_z = lightSource.transform.rotation.eulerAngles.z;
		planet_x = planet.transform.rotation.eulerAngles.x;
		planet_y = planet.transform.rotation.eulerAngles.y;
		planet_z = planet.transform.rotation.eulerAngles.z;



		angularDifference = Quaternion.Angle(planet.transform.rotation,lightSource.transform.rotation);

		//link the first container to the planet rotation
		container.transform.rotation = Quaternion.Euler(planet_x, planet_y,planet_z);
		//link the 2nd container to the sun Y axis rotation 
		subContainer.transform.eulerAngles = new Vector3(0,sun_y,0);
		//reset the X and Z axis localRotation of the 2nd container to zero after its global rotation (line above), and keep the Y axis localRotation.

		subContainer.transform.localRotation = Quaternion.Euler(0, subContainer.transform.localRotation.eulerAngles.y,0);
		//link the projector rotation to the sun directionnal light
		transform.rotation = Quaternion.Euler(sun_x, sun_y ,sun_z);

		//but keep only the X axis localRotation and reset Y and Z localRotation to keep the projector pattern aligned with the rings
		pa1 = transform.localRotation.eulerAngles.x;

		//Flips the projector Z axis, or not, based on whether the shadow is porjected above or below the rings
		if(pa1>=180){
			transform.localRotation = Quaternion.Euler(pa1, 0 ,180);
		}
		else{
			transform.localRotation = Quaternion.Euler(pa1, 0 ,0);
		}
		//Calculates the FOV of the projector according to its angle difference with the Sun light.
		angularDifference = (pa1>=180)?pa1-360:pa1;
		angularDifference = Mathf.Abs(angularDifference);
		proj.fieldOfView = angularDifference*0.8f;

	}
}
