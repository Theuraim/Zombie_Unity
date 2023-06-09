using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorChefe : MonoBehaviour
{
    private float tempoParaProximaGeracao = 0;
    public float tempoEntreGeracoes = 60;
    public GameObject ChefePrefab;
    private ControlaInterface scriptControlaInterface;
    public Transform[] PosicoesGeracao;
    private Transform jogador;

    [SerializeField]
    private ReservaFixa reservaChefe;

    private void Start()
    {
        tempoParaProximaGeracao = tempoEntreGeracoes;
        scriptControlaInterface = GameObject.FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;
        jogador = GameObject.FindWithTag("Jogador").transform;
    }

    private void Update()
    {
        if (Time.timeSinceLevelLoad > tempoParaProximaGeracao)
        {
            if (this.reservaChefe.TemObjeto())
            {
                Vector3 posicaoCriacao = CalcularPosicao();
                var chefe = this.reservaChefe.PegarObjeto();
                ControlaChefe controleChefe = chefe.GetComponent<ControlaChefe>();
                controleChefe.SetPosicao(posicaoCriacao);
                scriptControlaInterface.AparecerTextoChefe();
                tempoParaProximaGeracao = Time.timeSinceLevelLoad + tempoEntreGeracoes;
            }
        }
    }

    Vector3 CalcularPosicao()
    {
        Vector3 posicaoMaiorDistancia = Vector3.zero;

        float maiorDistancia = 0;

        foreach (Transform posicao in PosicoesGeracao)
        {
            float distanciaEntreOJogador = Vector3.Distance(posicao.position, jogador.position);

            if (distanciaEntreOJogador > maiorDistancia)
            {
                maiorDistancia = distanciaEntreOJogador;
                posicaoMaiorDistancia = posicao.position;
            }
        }

        return posicaoMaiorDistancia;
    }
}
