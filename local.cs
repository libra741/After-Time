using UnityEngine;
using System.Collections;

public class local : MonoBehaviour {
	[SerializeField]
	GameObject player,objetos;
	[SerializeField]
	GameObject[] posicao;
	float relogio;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (transform.position.x, -0.02f, player.transform.position.z+118);
		relogio += Time.deltaTime;
	//	transform.position = new Vector3 (transform.position.x, transform.position.y, player.transform.position.z+106.5f);

		if (relogio >= 3) {
			Instantiate (objetos, posicao [Random.Range (0, 3)].transform.position,Quaternion.identity);
			relogio = 0;
		}
	}
}

