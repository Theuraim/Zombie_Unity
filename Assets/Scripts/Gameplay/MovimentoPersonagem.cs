using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoPersonagem : MonoBehaviour
{
    public Vector3 Direcao { get; protected set; }

    private Rigidbody rBody;


    public void Awake()
    {
        rBody = GetComponent<Rigidbody>();
    }

    public void SetDirecao(Vector2 direcao)
    {
        this.Direcao = new Vector3(direcao.x, 0, direcao.y);
    }

    public void SetDirecao(Vector3 direcao)
    {
        this.Direcao = direcao;
    }

    public void Movimentar(float velocidade, bool bMovJogador = false)
    {   
        rBody.MovePosition(rBody.position + ((bMovJogador? this.Direcao : this.Direcao.normalized) * velocidade * Time.deltaTime));
    }

    public void Rotacionar(Vector3 direcao)
    {
        if (direcao != Vector3.zero)
        {
            Quaternion novaRotacao = Quaternion.LookRotation(direcao);
            rBody.MoveRotation(novaRotacao);
        }
    }

    public void Morrer()
    {
        rBody.constraints = RigidbodyConstraints.None;
        rBody.velocity = Vector3.zero;
        rBody.isKinematic = false;
        GetComponent<Collider>().enabled = false;
    }

    public void Reiniciar()
    {
        rBody.isKinematic = true;
        GetComponent<Collider>().enabled = true;
    }
}
