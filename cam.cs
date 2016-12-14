using UnityEngine;
using System.Collections;

public class cam : MonoBehaviour {
	[SerializeField]
	GameObject player;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (transform.position.x, transform.position.y, player.transform.position.z-12);

	
	}
}
