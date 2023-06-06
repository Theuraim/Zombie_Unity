using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaArma : MonoBehaviour
{
    public GameObject Bala;
    public GameObject CanoDaArma;
    public AudioClip Somtiro;

    [SerializeField]
    private ReservaExtensivel reservaBalas;

    // Update is called once per frame
    void Update()
    {
        var toquesNaTela = Input.touches;
        foreach(var toque in toquesNaTela)
        {
            if (toque.phase == TouchPhase.Began)
            {
                this.Atirar();
                ControlaAudio.instancia.PlayOneShot(Somtiro);
            }
        }
    }

    private void Atirar()
    {
        if (this.reservaBalas.TemObjeto())
        {
            var bala = this.reservaBalas.PegarObjeto();
            bala.transform.position = CanoDaArma.transform.position;
            bala.transform.rotation = CanoDaArma.transform.rotation;
        }
    }
}
