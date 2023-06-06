using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorZumbis : MonoBehaviour
{
    [SerializeField]
    private ReservaFixa reserva;
    private float contadorTempo = 0;
    public float TempoGerarZumbi = 1;
    public LayerMask LayerZumbi;
    private float distanciaGeracao = 3;
    private float DistanciaJogadorGeracao = 20;
    private GameObject jogador;
    private float tempoProximoAumentoDificuldade = 30;
    private float contadorAumentarDificuldade;

    public void Start()
    {
        jogador = GameObject.FindWithTag("Jogador");
        contadorAumentarDificuldade = tempoProximoAumentoDificuldade;
    }

    // Update is called once per frame
    public void Update()
    {
        bool bGerarZumbisDistancia = Vector3.Distance(transform.position, jogador.transform.position) > DistanciaJogadorGeracao;
        if (bGerarZumbisDistancia)
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

        if (this.reserva.TemObjeto()) { 
            GameObject zumbi = this.reserva.PegarObjeto();
            zumbi.transform.position = posicaoCriacao;
            var controleZumbi = zumbi.GetComponent<ControlaInimigo>();
            controleZumbi.GeraZumbis = this;
        }
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
}
