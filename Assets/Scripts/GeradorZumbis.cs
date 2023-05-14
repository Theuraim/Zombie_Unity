using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorZumbis : MonoBehaviour
{
    public GameObject Zumbi;
    private float contadorTempo = 0;
    public float TempoGerarZumbi = 1;
    public LayerMask LayerZumbi;
    private float distanciaGeracao = 3;
    private float DistanciaJogadorGeracao = 20;
    private GameObject jogador;
    private int quantidadeMaximaZumbis = 2;
    private int quantidadeZumbis;
    private float tempoProximoAumentoDificuldade = 30;
    private float contadorAumentarDificuldade;

    public void Start()
    {
        jogador = GameObject.FindWithTag("Jogador");
        contadorAumentarDificuldade = tempoProximoAumentoDificuldade;

        for(int i = 0; i < quantidadeMaximaZumbis; i++)
        {
            StartCoroutine(GerarNovoZumbi());
        }
    }

    // Update is called once per frame
    public void Update()
    {
        bool bGerarZumbisDistancia = Vector3.Distance(transform.position, jogador.transform.position) > DistanciaJogadorGeracao;
        if ((bGerarZumbisDistancia) && (quantidadeZumbis < quantidadeMaximaZumbis))
        {
            contadorTempo += Time.deltaTime;

            if (contadorTempo >= TempoGerarZumbi)
            {
                StartCoroutine(GerarNovoZumbi());
                contadorTempo = 0;
            }
        }

        if (Time.timeSinceLevelLoad > contadorAumentarDificuldade)
        {
            quantidadeMaximaZumbis++;
            contadorAumentarDificuldade = Time.timeSinceLevelLoad + tempoProximoAumentoDificuldade;
        }
    }

    private IEnumerator GerarNovoZumbi()
    {
        Vector3 posicaoCriacao = AleatorizarPosicao();
        Collider[] colisores = Physics.OverlapSphere(posicaoCriacao, 1, LayerZumbi);

        while (colisores.Length > 0)
        {
            posicaoCriacao = AleatorizarPosicao();
            colisores = Physics.OverlapSphere(posicaoCriacao, 1, LayerZumbi);
            yield return null;
        }

        ControlaInimigo zumbi = Instantiate(Zumbi, posicaoCriacao, transform.rotation).GetComponent<ControlaInimigo>();
        zumbi.GeraZumbis = this;
        quantidadeZumbis++;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanciaGeracao);
    }

    private Vector3 AleatorizarPosicao()
    {
        Vector3 posicao = Random.insideUnitSphere * distanciaGeracao;
        posicao += transform.position;
        posicao.y = 0;

        return posicao;
    }

    public void DiminuiQuantidadeZumbis()
    {
        quantidadeZumbis--;
    } 
}
