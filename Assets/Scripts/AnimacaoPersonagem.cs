using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacaoPersonagem : MonoBehaviour
{
    private Animator animate;

    public void Awake()
    {
        animate = GetComponent<Animator>();
    }

    public void Atacar(bool estado)
    {
        animate.SetBool("Atacando", estado);
    }

    public void Movimentar(float valorMovimento)
    {
        animate.SetFloat("Movendo", valorMovimento);
    }
}
