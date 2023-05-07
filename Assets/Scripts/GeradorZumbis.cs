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

    public void Start()
    {
        jogador = GameObject.FindWithTag("Jogador");
    }

    // Update is called once per frame
    public void Update()
    {
        if (Vector3.Distance(transform.position, jogador.transform.position) > DistanciaJogadorGeracao)
        {
            contadorTempo += Time.deltaTime;

            if (contadorTempo >= TempoGerarZumbi)
            {
                StartCoroutine(GerarNovoZumbi());
                contadorTempo = 0;
            }
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

        Instantiate(Zumbi, posicaoCriacao, transform.rotation);
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
