using UnityEngine;
using System.Collections;

public class InstantiateGeysers : MonoBehaviour {

	public GameObject[] geysersPrefabs;
	private GameObject[] geysers;
	public bool disableBillboard;

	// Use this for initialization
	void Start () {
		geysers = new GameObject[geysersPrefabs.Length];
		for(int i = 0; i<geysersPrefabs.Length; i++){
			geysers[i] = Instantiate(geysersPrefabs[i],transform.position,geysersPrefabs[i].transform.rotation) as GameObject;
			geysers[i].transform.parent=transform;
			geysers[i].transform.localScale = new Vector3(1,1,1);
			if(disableBillboard){
				geysers[i].GetComponent<GeyserLight>().setBillboard(true);
			}else{
				geysers[i].GetComponent<GeyserLight>().setBillboard(false);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
