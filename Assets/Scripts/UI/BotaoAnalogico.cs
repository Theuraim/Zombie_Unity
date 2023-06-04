using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BotaoAnalogico : MonoBehaviour, IDragHandler
{
    [SerializeField]
    private RectTransform imagemFundo;
    [SerializeField]
    private RectTransform imagemBolinha;
    [SerializeField]
    private MeuEventoDinamicoVector2 QuandoMudarValor;

    public void OnDrag(PointerEventData dadosMouse)
    {
        var posicaoMouse = CalcularPosicaoMouse(dadosMouse);
        var posicaoLimitada = this.LimitarPosicao(posicaoMouse);
        this.PosicionarJoystick(posicaoLimitada);

        this.QuandoMudarValor.Invoke(posicaoLimitada);
    }

    private Vector2 LimitarPosicao(Vector2 posicaoMouse)
    {
        var posicaoLimitada = posicaoMouse / this.TamanhoImagem();

        if (posicaoLimitada.magnitude > 1) {
            posicaoLimitada = posicaoLimitada.normalized; 
        }

        return posicaoLimitada;
    }

    private float TamanhoImagem()
    {
        return this.imagemFundo.rect.width / 2;
    }

    private void PosicionarJoystick(Vector2 posicaoMouse)
    {
        this.imagemBolinha.localPosition = posicaoMouse * TamanhoImagem();
    }

    private Vector2 CalcularPosicaoMouse(PointerEventData dadosMouse)
    {
        Vector2 posicao;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(imagemFundo, dadosMouse.position, dadosMouse.enterEventCamera, out posicao);

        return posicao;
    }
}

[Serializable]
public class MeuEventoDinamicoVector2 : UnityEvent<Vector2>{ }