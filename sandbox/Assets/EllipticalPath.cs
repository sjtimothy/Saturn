using UnityEngine;
using System.Collections;

public class EllipticalPath : MonoBehaviour {
	
	public Vector3 center;
	public float r1;
	public float r2;
	public float radPerSecond;
	public float alpha;
	public GameObject pathObject;
	public float angle;
	float startAngle;
	public bool loop;
	
	// Use with trail renderer, draw gizmo for debug use only
	void Start () {
		//alpha = Mathf.PI * (1f - .25f * (Mathf.Sqrt (2f * Mathf.Pow (r1 / r2 + 1f, 3f))));
		startAngle = angle;

		
	}
	
	// Update is called once per frame
	void Update () {
		if (Mathf.Abs(angle) <= Mathf.Abs(alpha)) //draw
		{	

			//gameObject.GetComponent<TrailRenderer>().enabled=true;
			angle += radPerSecond * Time.deltaTime;
		
			// Calculates position with parametric form, explanation:
			// http://en.wikipedia.org/wiki/Ellipse#Parametric_form_in_canonical_position
			float x = r1 * Mathf.Cos (angle);
			float y = r2 * Mathf.Sin (angle);
		
			transform.position = center + new Vector3 (x, 0, y); //starting point?

			//OnDrawGizmosSelected();
		}
		else if(Mathf.Abs(angle) > Mathf.Abs(alpha))
		{	

			//gameObject.GetComponent<TrailRenderer>().enabled=false; //disable trail, reset angle
			if (loop == true){
				angle = startAngle;
				drawPath ();
			}
		}
	}

	void drawPath(){
		GameObject path = Instantiate(pathObject, new Vector3(transform.position.x,transform.position.y, transform.position.z) , Quaternion.identity) as GameObject;
		path.transform.parent = gameObject.transform;
	}

	void OnDrawGizmosSelected(){

		Gizmos.DrawLine(Vector3.zero, transform.position);
	}
}