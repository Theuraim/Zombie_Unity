using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReservavel
{
    void SetReserva(IReservaObjetos reserva);
    void AoEntrarNaReserva();
    void AoSairDaReserva();
}
