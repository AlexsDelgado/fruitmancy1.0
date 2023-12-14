using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    public static Points Instance;

    [SerializeField] private float cantidadPuntos;

    private void Awake()
    {
        if (Points.Instance == null)
        {
            Points.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SumarPuntos(float puntos)
    {
        cantidadPuntos += puntos;
    }
}
