using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaInimigo : MonoBehaviour
{
    public GameObject Jogador;
    public float Velocidade = 5;
    private Rigidbody rBody;
    private Animator animatorInimigo;

    // Start is called before the first frame update
    void Start()
    {
        //Setting important variables
        rBody = GetComponent<Rigidbody>();
        animatorInimigo = GetComponent<Animator>();
        Jogador = GameObject.FindWithTag("Jogador");

        //Activating a random zumbi model
        int geraTipoZumbi = Random.Range(1, 28);
        transform.GetChild(geraTipoZumbi).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Vector3 direcao = Jogador.transform.position - transform.position;

        float distancia = Vector3.Distance(transform.position, Jogador.transform.position);

        Quaternion novaRotacao = Quaternion.LookRotation(direcao);
        rBody.MoveRotation(novaRotacao);

        if (distancia > 2.5)
        {
            rBody.MovePosition(rBody.position + (direcao.normalized * Velocidade * Time.deltaTime));

            animatorInimigo.SetBool("Atacando", false);
        }
        else
        {
            animatorInimigo.SetBool("Atacando", true);
        }
    }

    void AtacaJogador()
    {
        if (Jogador != null) 
        {
            int dano = Random.Range(20, 30);
            Jogador.GetComponent<ControlaJogador>().TomarDano(dano);
        }
    }
}
