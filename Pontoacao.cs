using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Pontoacao : MonoBehaviour {

	float distancia;
	[SerializeField]
	Text exibirDistancia;

	void Update () {
		if (Input.GetMouseButtonDown (0))
		{
			distancia += Time.deltaTime * 30;
			PlayerPrefs.SetInt ("Distancia",(int)distancia);
		}
		exibirDistancia.text = "Sua Distancia: " + (int)distancia + " Metros.";
	}
}
