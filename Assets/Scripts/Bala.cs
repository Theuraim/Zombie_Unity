using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public float Velocidade = 20;
    private Rigidbody rBody;
    public int Dano = 1;

    private void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rBody.MovePosition(rBody.position + transform.forward * Velocidade * Time.deltaTime);
    }

    void OnTriggerEnter(Collider objetoDeColisao)
    {
        switch(objetoDeColisao.tag)
        {
            case "Inimigo":
                objetoDeColisao.GetComponent<ControlaInimigo>().TomarDano(Dano);
            break;

            case "ChefeDeFase":
                objetoDeColisao.GetComponent<ControlaChefe>().TomarDano(Dano);
            break;
        }

        Destroy(gameObject);
    }
}