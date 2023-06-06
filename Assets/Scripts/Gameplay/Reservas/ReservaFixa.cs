using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReservaFixa : MonoBehaviour, IReservaObjetos
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private int quantidade;

    private Stack<GameObject> reserva;

    private void Awake()
    {
        this.reserva = new Stack<GameObject>();
        this.CriarTodosObjetos();
    }

    private void CriarTodosObjetos()
    {
        for(var i = 0; i < this.quantidade; i++)
        {
            this.CriarNovoObjeto();
        }
    }

    private void CriarNovoObjeto()
    {
        var objeto = GameObject.Instantiate(this.prefab, this.transform);
        var controlaComponente = objeto.GetComponent<IReservavel>();
        controlaComponente.SetReserva(this);
        this.DevolverObjeto(objeto);
    }

    public void DevolverObjeto(GameObject objeto)
    {
        var objetoReservavel = objeto.GetComponent<IReservavel>();
        objetoReservavel.AoEntrarNaReserva();
        this.reserva.Push(objeto);
    }

    public GameObject PegarObjeto() 
    {
        var objeto = this.reserva.Pop();
        var objetoReservavel = objeto.GetComponent<IReservavel>();
        objetoReservavel.AoSairDaReserva();
        return objeto;
    }

    public bool TemObjeto()
    {
        return this.reserva.Count > 0;
    }

}
