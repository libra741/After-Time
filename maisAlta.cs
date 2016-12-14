using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class maisAlta : MonoBehaviour {
	[SerializeField]
	Text mAlta;

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetInt ("Distancia") > PlayerPrefs.GetInt ("mDistancia"))
		{
			PlayerPrefs.SetInt ("mDistancia",PlayerPrefs.GetInt ("Distancia"));
		}
		mAlta.text = "A maior Distancia é " + PlayerPrefs.GetInt ("mDistancia") + " Metros";
	
	}

	public void Voltar()
	{
		SceneManager.LoadScene ("menu");
	}
}
