using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoJogador : MovimentoPersonagem
{
    [SerializeField]
    private CaixaDeSom som;
    public void AudioPasso()
    {
        som.Tocar();
    }
    public void RotacaoJogador()
    {
        if (this.Direcao != Vector3.zero)
        {
            Vector3 posicaoMiraJogador = this.Direcao;
            Rotacionar(posicaoMiraJogador);
        }
    }
}
