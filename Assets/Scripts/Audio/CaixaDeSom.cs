using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CaixaDeSom : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] listaAudio;
    private AudioSource saidaAudio;

    private void Awake()
    {
        this.saidaAudio = this.GetComponent<AudioSource>();
    }

    public void Tocar()
    {
        var sorteado = Random.Range(0, this.listaAudio.Length);
        this.saidaAudio.PlayOneShot(this.listaAudio[sorteado]);
    }
}
