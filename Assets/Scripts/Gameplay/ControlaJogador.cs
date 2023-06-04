using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaJogador : MonoBehaviour, IMatavel, ICuravel
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
        animacaoJogador.Movimentar(this.movimentaJogador.Direcao.magnitude);
    }

    void FixedUpdate()
    {
        movimentaJogador.Movimentar(statusJogador.Velocidade, true);

        movimentaJogador.RotacaoJogador();
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

    public void CurarVida (int quantidadeDeCura)
    {
        statusJogador.Vida += quantidadeDeCura;

        if (statusJogador.Vida > statusJogador.VidaInicial)
        {
            statusJogador.Vida = statusJogador.VidaInicial;
        }

        scriptControlaInterface.AtualizarVidaJogador();
    }
}
