using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
[RequireComponent(typeof(Rigidbody))]
public class player : MonoBehaviour {//NESTE SCRIPT O PERSONAGEM DEVE SE MOVER PARA FRENTE NO EIXO Z(AZUL), PARA TIRAR DÚVIDAS, CONTATE WWW.SCHULTZGAMES.COM
	private float ponteiroX, ponteiroY, novaPosicX;
	private int indicePosic;
	private bool podeMover, estaNoChao, pulouR;
	private Vector3 posicInicial;
	[SerializeField]
	[Range(0f,1)] float TempoParaMover = 0.15f;
	[SerializeField]
	[Range(1,5)] int QuantoMover = 1;
	[SerializeField]
	[Range(1,20)] float forcaDoPulo = 5.0f;
	[SerializeField]
	[Range(0,100)] float velocidadeJogador = 5.0f;
	bool podePular = true;
	[SerializeField]
	LayerMask LayersNaoIgnoradas = -1;
	private Rigidbody corpoRigido;
	Animator anim;
	[SerializeField]
	GameObject pista;
	[SerializeField]
	Transform local;
	[SerializeField]
	Transform[] pontos;
	float relogio=30;
	[SerializeField]
	Text textorelo;
	bool morto;
	CapsuleCollider col;
	void Start(){
		col = GetComponent<CapsuleCollider>();
		anim = GetComponent<Animator> ();
		corpoRigido = GetComponent<Rigidbody> ();
		corpoRigido.constraints = RigidbodyConstraints.FreezeRotation;
		posicInicial = transform.position;
		novaPosicX = posicInicial.x + indicePosic*QuantoMover;
		indicePosic = 0;
		pulouR = false;
		podeMover = true;

	}

	void Update () {
		relogio -= Time.deltaTime;
		estaNoChao = Physics.Linecast (transform.position, transform.position - Vector3.up, LayersNaoIgnoradas);
		if (podeMover) {
			DetectarMovimento ();
		}
		textorelo.text = "" + (int)relogio+" Segundos para o amanhã!";

		if(relogio <= 0)
		SceneManager.LoadScene ("gameover");
		
	}

	IEnumerator EsperarParaMover(float tempo) {
		yield return new WaitForSeconds(tempo);
		podeMover = true;
	}
	IEnumerator EsperarParaPular(float tempo) {
		yield return new WaitForSeconds(tempo);
		pulouR = false;
	}

	void DetectarMovimento(){
		podeMover = false;
		StartCoroutine (EsperarParaMover(TempoParaMover));

		ponteiroX = ponteiroY = 0;
		if (Input.GetMouseButton (0)) {
			ponteiroX = Input.GetAxis ("Mouse X");
			ponteiroY = Input.GetAxis ("Mouse Y");
		}
		if (Input.touchCount > 0){
			ponteiroX = Input.touches[0].deltaPosition.x;
			ponteiroY = Input.touches[0].deltaPosition.y;
		}
		//DETECTAR EIXO X
		if (ponteiroX > 5 && indicePosic < 1 || Input.GetKeyDown(KeyCode.D))
		{
			indicePosic ++;
			novaPosicX = posicInicial.x + indicePosic*QuantoMover;
		}
		else if (ponteiroX <-5 && indicePosic > -1 || Input.GetKeyDown(KeyCode.A))
		{
			indicePosic --;
			novaPosicX = posicInicial.x + indicePosic*QuantoMover;
		}

	
		//DETECTAR EIXO Y
//		if (Input.GetKey(KeyCode.Space)) {
//			Pular ();
			//anim.SetTrigger ("pular");
		//}
		if (ponteiroY > 1f && podePular) {
			Pular ();
			anim.SetTrigger ("pular");
		}		/*else {
			anim.SetTrigger ("correndo");
		}
		if (ponteiroY < -1f && podePular) {
		//Pular ();
			anim.SetTrigger ("slide");
	} else {
		anim.SetTrigger ("correndo");
	}
	}*/
	}
	void FixedUpdate(){
		Vector3 proximaPosic = new Vector3 (novaPosicX, transform.position.y, transform.position.z);
		transform.position = Vector3.Lerp (transform.position, proximaPosic, Time.deltaTime * 5);
	//	corpoRigido.velocity = new Vector3 (corpoRigido.velocity.x, corpoRigido.velocity.y, velocidadeJogador);
		if (Input.GetButtonDown ("Fire1")&& !morto) {
		//	anim.SetTrigger ("CORRER", true);
			col.height = 0.16f;
			anim.speed = corpoRigido.velocity.magnitude/10;
			corpoRigido.AddForce (transform.forward * velocidadeJogador);
		} 
	}


	void Pular(){
		if(estaNoChao == true && pulouR == false){
			corpoRigido.AddForce(Vector3.up * forcaDoPulo, ForceMode.Impulse);
			pulouR = true;
			StartCoroutine (EsperarParaPular(0.5f));
			col.height = 0;
		}
	}
	void OnTriggerEnter(Collider outro){

		if (outro.tag == "local") {
			Instantiate (pista, local.position, local.rotation);
		}
		if (outro.tag == "relogio") {
			relogio += 15;
		}
		if (outro.tag == "obs") {
			anim.SetTrigger ("caindo");
			SceneManager.LoadScene ("gameover");
			corpoRigido.velocity = new Vector3 (transform.position.x, transform.position.y, 0);
			anim.speed = 1;
			morto = true;
		}
	}
}