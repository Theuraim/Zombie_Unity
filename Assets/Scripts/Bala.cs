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
        Quaternion rotacaoOpostaBala = Quaternion.LookRotation(-transform.forward);

        switch(objetoDeColisao.tag)
        {
            case "Inimigo":
                ControlaInimigo inimigo = objetoDeColisao.GetComponent<ControlaInimigo>();
                inimigo.TomarDano(Dano);
                inimigo.ParticulaSangue(this.transform.position, rotacaoOpostaBala);
                break;

            case "ChefeDeFase":
                ControlaChefe chefe = objetoDeColisao.GetComponent<ControlaChefe>();
                chefe.TomarDano(Dano);
                chefe.ParticulaSangue(this.transform.position, rotacaoOpostaBala);
            break;
        }

        Destroy(gameObject);
    }
}