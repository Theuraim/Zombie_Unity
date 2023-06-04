using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlaInterface : MonoBehaviour
{
    private ControlaJogador scriptControlaJogador;
    public Slider SliderVidaJogador;
    public GameObject PainelGameOver;
    public Text TextoTempoSobrevivencia;
    public Text TextoPontuacaoMaxima;
    private float tempoPontuacaoSalvo;
    public int quantidadeZumbisMortos;
    public Text TextoQuantidadeDeZumbisMortos;
    public Text AvisoAparicaoChefe;

    // Start is called before the first frame update
    void Start()
    {
        //After reloading turning back animations
        Time.timeScale = 1;
        scriptControlaJogador = GameObject.FindWithTag("Jogador").GetComponent<ControlaJogador>();
        SliderVidaJogador.maxValue = scriptControlaJogador.statusJogador.Vida;
        AtualizarVidaJogador();
        tempoPontuacaoSalvo = PlayerPrefs.GetFloat("PontuacaoMaxima");
    }

    public void AtualizarVidaJogador()
    {
        SliderVidaJogador.value = scriptControlaJogador.statusJogador.Vida;
    }

    public void AtualizarQuantideZumbisMortos()
    {
        quantidadeZumbisMortos++;
        TextoQuantidadeDeZumbisMortos.text = string.Format("x {0}", quantidadeZumbisMortos);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        PainelGameOver.SetActive(true);

        int minutos = (int)(Time.timeSinceLevelLoad / 60);
        int segundos = (int)(Time.timeSinceLevelLoad % 60);

        TextoTempoSobrevivencia.text = "Você sobreviveu por: " + minutos + "min e " + segundos + "s";

        AjustarPontuacaoMaxima(minutos, segundos);
    }

    void AjustarPontuacaoMaxima(int min, int seg)
    {
        if (Time.timeSinceLevelLoad > tempoPontuacaoSalvo)
        {
            tempoPontuacaoSalvo = Time.timeSinceLevelLoad;
            TextoPontuacaoMaxima.text = string.Format("Seu melhor tempo foi: {0}min e {1}s", min, seg);
            PlayerPrefs.SetFloat("PontuacaoMaxima", tempoPontuacaoSalvo);
        }

        if (TextoPontuacaoMaxima.text == "")
        {
            min = (int)(tempoPontuacaoSalvo / 60);
            seg = (int)(tempoPontuacaoSalvo % 60);

            TextoPontuacaoMaxima.text = string.Format("Seu melhor tempo foi: {0}min e {1}s", min, seg);
        }
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void AparecerTextoChefe()
    {
        StartCoroutine(DesaparecerTexto(2, AvisoAparicaoChefe));
    }

    IEnumerator DesaparecerTexto(float tempo, Text texto)
    {
        texto.gameObject.SetActive(true);
        Color corTexto = texto.color;
        corTexto.a = 1;
        float contador = 0;
        texto.color = corTexto;

        yield return new WaitForSeconds(tempo);

        while(texto.color.a > 0)
        {
            contador += Time.deltaTime / tempo;
            corTexto.a = Mathf.Lerp(1, 0, contador);
            texto.color = corTexto;
            if (texto.color.a <= 0)
            {
                texto.gameObject.SetActive(false);
            }

            yield return null;
        }
    }
}
