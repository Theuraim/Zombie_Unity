using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaJogador : MonoBehaviour
{
    public float Velocidade = 10;
    private Vector3 direcao;
    public LayerMask MascaraChao;
    public GameObject TextoGameOver;
    private Rigidbody rBody;
    private Animator animatorJogador;
    public int Vida = 100;
    public ControlaInterface scriptControlaInterface;

    private void Start()
    {
        //Setting important variables
        rBody = GetComponent<Rigidbody>();
        animatorJogador = GetComponent<Animator>();

        //After reloading turning back animations
        Time.timeScale = 1;
    }
    // Update is called once per frame
    void Update()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);

        //transform.Translate(direcao * Velocidade * Time.deltaTime);

        if (direcao != Vector3.zero)
        {
            animatorJogador.SetBool("Movendo", true);
        }
        else
        {
            animatorJogador.SetBool("Movendo", false);
        }

        if (Vida <= 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                SceneManager.LoadScene("MainScene");
            }
        }
    }

    void FixedUpdate()
    {
        rBody.MovePosition(rBody.position +
                          (direcao * Velocidade * Time.deltaTime));

        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(raio.origin, raio.direction * 100, Color.red);

        RaycastHit impacto;
        
        if (Physics.Raycast(raio, out impacto, 100, MascaraChao))
        {
            Vector3 posicaoMiraJogador = impacto.point - transform.position;

            posicaoMiraJogador.y = transform.position.y;

            Quaternion novaRotacao = Quaternion.LookRotation(posicaoMiraJogador);

            rBody.MoveRotation(novaRotacao);
        }
    }

    public void TomarDano(int dano = 20)
    {
        Vida -= dano;
        scriptControlaInterface.AtualizarVidaJogador();

        if (Vida <= 0)
        {
            Time.timeScale = 0;
            TextoGameOver.SetActive(true);
        }
    }
}
