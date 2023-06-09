using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaInimigo : MonoBehaviour, IMatavel, IReservavel
{
    public GameObject Jogador;
    private MovimentoPersonagem movimentoInimigo;
    private AnimacaoPersonagem animacaoInimigo;
    private Status statusInimigo;
    public AudioClip SomMorte;
    private Vector3 posicaoAleatoria;
    private Vector3 direcao;
    private float contadorVagar;
    private float tempoEntrePosicoesAleatorias = 4;
    private float porcentagemGerarKitMedico = 0.1f;
    public GameObject KitMedicoPrefab;
    private ControlaInterface scriptControlaInterface;
    [HideInInspector]
    public GeradorZumbis GeraZumbis;
    public GameObject ParticulaSangueZumbi;

    private IReservaObjetos reserva;

    public void SetReserva(IReservaObjetos reserva)
    {
        this.reserva = reserva;
    }

    public void Awake()
    {
        movimentoInimigo = GetComponent<MovimentoPersonagem>();
        animacaoInimigo = GetComponent<AnimacaoPersonagem>();
    }

    // Start is called before the first frame update
    public void Start()
    {
        //Setting important variables
        Jogador = GameObject.FindWithTag("Jogador");
        statusInimigo = GetComponent<Status>();
        scriptControlaInterface = GameObject.FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;

        //Activating a random zumbi model
        AleatorizarZumbi();
    }

    public void FixedUpdate()
    {
        float distancia = Vector3.Distance(transform.position, Jogador.transform.position);

        animacaoInimigo.Movimentar(direcao.magnitude);

        if (distancia > 15)
        {
            Vagar();
        }
        else if (distancia > 2.5)
        {
            direcao = Jogador.transform.position - transform.position;

            movimentoInimigo.Rotacionar(direcao);

            movimentoInimigo.SetDirecao(direcao);
            movimentoInimigo.Movimentar(statusInimigo.Velocidade);

            animacaoInimigo.Atacar(false);
        }
        else
        {
            direcao = Jogador.transform.position - transform.position;
            animacaoInimigo.Atacar(true);
        }
    }

    public void AtacaJogador()
    {
        if (Jogador != null) 
        {
            int dano = Random.Range(20, 30);
            Jogador.GetComponent<ControlaJogador>().TomarDano(dano);
        }
    }

    private void AleatorizarZumbi()
    {
        int geraTipoZumbi = Random.Range(1, transform.childCount);
        transform.GetChild(geraTipoZumbi).gameObject.SetActive(true);
    }

    public void TomarDano(int dano)
    {
        statusInimigo.Vida -= dano;


        if (statusInimigo.Vida <= 0)
        {
            Morrer();
        }
    }

    public void ParticulaSangue(Vector3 posicao, Quaternion rotacao)
    {
        Instantiate(ParticulaSangueZumbi, posicao, rotacao);
    }

    public void Morrer()
    {
        Invoke("VoltarReserva", 5);
        animacaoInimigo.Morrer();
        this.enabled = false;
        movimentoInimigo.Morrer();
        ControlaAudio.instancia.PlayOneShot(SomMorte);
        VerificarGeracaoKitMedico(porcentagemGerarKitMedico);
        scriptControlaInterface.AtualizarQuantideZumbisMortos();
    }

    private void VoltarReserva()
    {
        this.movimentoInimigo.Reiniciar();
        this.enabled = true;
        this.reserva.DevolverObjeto(this.gameObject);
    }

    private void Vagar()
    {
        contadorVagar -= Time.deltaTime;

        if (contadorVagar <= 0)
        {
            posicaoAleatoria = AletorizarPosicao();
            contadorVagar += tempoEntrePosicoesAleatorias + Random.Range(-1f, 1f);
        }

        bool chegouPerto = Vector3.Distance(transform.position, posicaoAleatoria) <= 0.05;

        if (!chegouPerto) {
            direcao = posicaoAleatoria - transform.position;
            movimentoInimigo.SetDirecao(direcao);
            movimentoInimigo.Movimentar(statusInimigo.Velocidade);
        }

    }
    private Vector3 AletorizarPosicao()
    {
        Vector3 posicao = Random.insideUnitSphere * 10;
        posicao += transform.position;
        posicao.y = transform.position.y;

        return posicao;
    }

    private void VerificarGeracaoKitMedico(float porcentagemGeracao)
    {
        if (Random.value <= porcentagemGeracao)
        {
            Instantiate(KitMedicoPrefab, transform.position, Quaternion.identity);
        }
    }

    public void AoEntrarNaReserva()
    {
        this.movimentoInimigo.Reiniciar();
        this.enabled = true;
        this.gameObject.SetActive(false);
    }

    public void AoSairDaReserva()
    {
        this.gameObject.SetActive(true);
    }
}
