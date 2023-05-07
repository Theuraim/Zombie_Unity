using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaJogador : MonoBehaviour, IMatavel
{
    private Vector3 direcao;
    public LayerMask MascaraChao;
    public GameObject TextoGameOver;
    public ControlaInterface scriptControlaInterface;
    public AudioClip SomDano;
    private MovimentoJogador movimentaJogador;
    private AnimacaoPersonagem animacaoJogador;
    public Status statusJogador;

    private void Start()
    {
        //Setting important variables
        movimentaJogador = GetComponent<MovimentoJogador>();
        animacaoJogador = GetComponent<AnimacaoPersonagem>();
        statusJogador = GetComponent<Status>();
    }
    // Update is called once per frame
    void Update()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);

        //transform.Translate(direcao * Velocidade * Time.deltaTime);

        animacaoJogador.Movimentar(direcao.magnitude);
    }

    void FixedUpdate()
    {
        movimentaJogador.Movimentar(direcao, statusJogador.Velocidade);

        movimentaJogador.RotacaoJogador(MascaraChao);
    }

    public void TomarDano(int dano)
    {
        statusJogador.Vida -= dano;
        scriptControlaInterface.AtualizarVidaJogador();
        ControlaAudio.instancia.PlayOneShot(SomDano);

        if (statusJogador.Vida <= 0)
        {
            Morrer();
        }
    }

    public void Morrer()
    {
        scriptControlaInterface.GameOver();
    }
}
