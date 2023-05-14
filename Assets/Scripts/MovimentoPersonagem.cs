using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoPersonagem : MonoBehaviour
{
    private Rigidbody rBody;

    public void Awake()
    {
        rBody = GetComponent<Rigidbody>();
    }

    public void Movimentar(Vector3 direcao, float velocidade)
    {
        rBody.MovePosition(rBody.position + (direcao.normalized * velocidade * Time.deltaTime));
    }

    public void Rotacionar(Vector3 direcao)
    {
        Quaternion novaRotacao = Quaternion.LookRotation(direcao);
        rBody.MoveRotation(novaRotacao);
    }

    public void Morrer()
    {
        rBody.constraints = RigidbodyConstraints.None;
        rBody.velocity = Vector3.zero;
        rBody.isKinematic = false;
        GetComponent<Collider>().enabled = false;
    }
}
