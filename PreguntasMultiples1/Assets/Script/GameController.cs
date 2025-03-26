using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using models;
using TMPro;
using Unity.VisualScripting;

public class GameController : MonoBehaviour
{
    public LeerPM leerPreguntaMultiple;
    public LeerPA leerPreguntaAbierta;
    public LeerPFV leerPreguntaFalsoVerdadero;
    string respuestaPM;
    string respuestaFV;
    string respuestaAbierta;
    int numeroPreguntaFacilGenerada;
    int numeroPreguntaDificilGenerada;
    int cantidadAciertos = 0;
    int cantidadErrores = 0;
    private PreguntaMultiple preguntaActualMultiple;
    private PreguntaFalsoVerdadero preguntaActualFV;
    private PreguntaAbierta preguntaActualAbierta;
    public TextMeshProUGUI textPreguntaMultiple;
    public TextMeshProUGUI textPreguntaFV;
    public TextMeshProUGUI textPreguntaAbierta;
    public TextMeshProUGUI textResp1;
    public TextMeshProUGUI textResp2;
    public TextMeshProUGUI textResp3;
    public TextMeshProUGUI textResp4;
    public TextMeshProUGUI textRespuestaAbierta;
    public TextMeshProUGUI textAciertos;
    public TextMeshProUGUI textErrores;
    public GameObject panelCorrectoPM;
    public GameObject panelIncorrectoPM;
    public GameObject panelCorrectoFV;
    public GameObject panelIncorrectoFV;
    public GameObject panelNoQuedanPreguntas;
    public GameObject panelPreguntasFV;
    public GameObject panelPreguntasPM;
    public GameObject panelPreguntasAbiertas;
    public GameObject panelRespuestaAbierta;
    public GameObject panelRondaDificil;
    public GameObject botonGenerarPreguntaPMFacil;
    public GameObject botonGenerarPreguntaPMDificil;
    public GameObject botonGenerarPreguntaFVFacil;
    public GameObject botonGenerarPreguntaFVDificil;
    public GameObject botonGenerarPreguntaAFacil;
    public GameObject botonGenerarPreguntaADificil;
    public GameObject panelEmpezar;
    public Timer timerManager;
    public AudioSource audioWin;
    public AudioSource audioLose;

    // Start is called before the first frame update
    void Start()
    {
        panelEmpezar.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void mostrarPreguntaAleatoriaFacil()
    {

        panelEmpezar.SetActive(false);
        List<int> listaNumerosAleatorios = new List<int>();

        if (leerPreguntaMultiple.listaPreguntasMultiplesFaciles.Count > 0) listaNumerosAleatorios.Add(1);
        if (leerPreguntaFalsoVerdadero.listaPreguntasFVFaciles.Count > 0) listaNumerosAleatorios.Add(2);
        if (leerPreguntaAbierta.listaPreguntasAbiertasFaciles.Count > 0) listaNumerosAleatorios.Add(3);

        if (listaNumerosAleatorios.Count > 0)
        {
            int numeroAleatorio = UnityEngine.Random.Range(0, listaNumerosAleatorios.Count);
            numeroPreguntaFacilGenerada = listaNumerosAleatorios[numeroAleatorio];

            if (numeroPreguntaFacilGenerada == 1)
            {
                mostrarPreguntasMultiplesFaciles();
            }
            else if (numeroPreguntaFacilGenerada == 2)
            {
                mostrarPreguntasFVFaciles();
            }
            else if (numeroPreguntaFacilGenerada == 3)
            {
                mostrarPreguntasAbiertasFaciles();
            }
        }
        else
        {
            timerManager.TimerStop();
            panelRondaDificil.SetActive(true);
            botonGenerarPreguntaPMDificil.SetActive(true);
            botonGenerarPreguntaFVDificil.SetActive(true);
            botonGenerarPreguntaADificil.SetActive(true);
            botonGenerarPreguntaPMFacil.SetActive(false);
            botonGenerarPreguntaFVFacil.SetActive(false);
            botonGenerarPreguntaAFacil.SetActive(false);
        }

    }

    public void mostrarPreguntaAleatoriaDificil()
    {

        List<int> listaNumerosAleatorios = new List<int>();

        if (leerPreguntaMultiple.listaPreguntasMultiplesDificiles.Count > 0) listaNumerosAleatorios.Add(1);
        if (leerPreguntaFalsoVerdadero.listaPreguntasFVDificiles.Count > 0) listaNumerosAleatorios.Add(2);
        if (leerPreguntaAbierta.listaPreguntasAbiertasDificiles.Count > 0) listaNumerosAleatorios.Add(3);

        if (listaNumerosAleatorios.Count > 0)
        {
            int numeroAleatorio = UnityEngine.Random.Range(0, listaNumerosAleatorios.Count);
            numeroPreguntaDificilGenerada = listaNumerosAleatorios[numeroAleatorio];

            if (numeroPreguntaDificilGenerada == 1)
            {
                mostrarPreguntasMultiplesDificiles();
            }
            else if (numeroPreguntaDificilGenerada == 2)
            {
                mostrarPreguntasFVDificiles();
            }
            else if (numeroPreguntaDificilGenerada == 3)
            {
                mostrarPreguntasAbiertasDificiles();
            }
        }
        else
        {
            timerManager.TimerStop();
            mostrarResultados();
            panelNoQuedanPreguntas.SetActive(true);
        }

    }

    #region muestra de preguntas
    public void mostrarPreguntasMultiplesFaciles()
    {
        panelPreguntasPM.SetActive(true);
        panelPreguntasFV.SetActive(false);
        panelPreguntasAbiertas.SetActive(false);
        int randomIndex = UnityEngine.Random.Range(0, leerPreguntaMultiple.listaPreguntasMultiplesFaciles.Count);
        preguntaActualMultiple = leerPreguntaMultiple.listaPreguntasMultiplesFaciles[randomIndex];

        textPreguntaMultiple.text = preguntaActualMultiple.Pregunta;
        textResp1.text = preguntaActualMultiple.Respuesta1;
        textResp2.text = preguntaActualMultiple.Respuesta2;
        textResp3.text = preguntaActualMultiple.Respuesta3;
        textResp4.text = preguntaActualMultiple.Respuesta4;
        respuestaPM = preguntaActualMultiple.RespuestaCorrecta;

        leerPreguntaMultiple.listaPreguntasMultiplesFaciles.RemoveAt(randomIndex);
    }

    public void mostrarPreguntasMultiplesDificiles()
    {
        panelPreguntasPM.SetActive(true);
        panelPreguntasFV.SetActive(false);
        panelPreguntasAbiertas.SetActive(false);
        int randomIndex = UnityEngine.Random.Range(0, leerPreguntaMultiple.listaPreguntasMultiplesDificiles.Count);
        preguntaActualMultiple = leerPreguntaMultiple.listaPreguntasMultiplesDificiles[randomIndex];

        textPreguntaMultiple.text = preguntaActualMultiple.Pregunta;
        textResp1.text = preguntaActualMultiple.Respuesta1;
        textResp2.text = preguntaActualMultiple.Respuesta2;
        textResp3.text = preguntaActualMultiple.Respuesta3;
        textResp4.text = preguntaActualMultiple.Respuesta4;
        respuestaPM = preguntaActualMultiple.RespuestaCorrecta;

        leerPreguntaMultiple.listaPreguntasMultiplesDificiles.RemoveAt(randomIndex);
    }

    public void mostrarPreguntasFVFaciles()
    {
        panelPreguntasPM.SetActive(false);
        panelPreguntasFV.SetActive(true);
        panelPreguntasAbiertas.SetActive(false);

        int randomIndex = UnityEngine.Random.Range(0, leerPreguntaFalsoVerdadero.listaPreguntasFVFaciles.Count);
        preguntaActualFV = leerPreguntaFalsoVerdadero.listaPreguntasFVFaciles[randomIndex];

        textPreguntaFV.text = preguntaActualFV.Pregunta;
        respuestaFV = preguntaActualFV.Respuesta;
        leerPreguntaFalsoVerdadero.listaPreguntasFVFaciles.RemoveAt(randomIndex);
    }
    public void mostrarPreguntasFVDificiles()
    {
        panelPreguntasPM.SetActive(false);
        panelPreguntasFV.SetActive(true);
        panelPreguntasAbiertas.SetActive(false);

        int randomIndex = UnityEngine.Random.Range(0, leerPreguntaFalsoVerdadero.listaPreguntasFVDificiles.Count);
        preguntaActualFV = leerPreguntaFalsoVerdadero.listaPreguntasFVDificiles[randomIndex];

        textPreguntaFV.text = preguntaActualFV.Pregunta;
        respuestaFV = preguntaActualFV.Respuesta;
        leerPreguntaFalsoVerdadero.listaPreguntasFVDificiles.RemoveAt(randomIndex);
    }
    public void mostrarPreguntasAbiertasFaciles()
    {
        panelPreguntasPM.SetActive(false);
        panelPreguntasFV.SetActive(false);
        panelPreguntasAbiertas.SetActive(true);

        int randomIndex = UnityEngine.Random.Range(0, leerPreguntaAbierta.listaPreguntasAbiertasFaciles.Count);
        preguntaActualAbierta = leerPreguntaAbierta.listaPreguntasAbiertasFaciles[randomIndex];

        textPreguntaAbierta.text = preguntaActualAbierta.Pregunta;
        respuestaAbierta = preguntaActualAbierta.Respuesta;
        leerPreguntaAbierta.listaPreguntasAbiertasFaciles.RemoveAt(randomIndex);
    }
    public void mostrarPreguntasAbiertasDificiles()
    {
        panelPreguntasPM.SetActive(false);
        panelPreguntasFV.SetActive(false);
        panelPreguntasAbiertas.SetActive(true);

        int randomIndex = UnityEngine.Random.Range(0, leerPreguntaAbierta.listaPreguntasAbiertasDificiles.Count);
        preguntaActualAbierta = leerPreguntaAbierta.listaPreguntasAbiertasDificiles[randomIndex];

        textPreguntaAbierta.text = preguntaActualAbierta.Pregunta;
        respuestaAbierta = preguntaActualAbierta.Respuesta;
        leerPreguntaAbierta.listaPreguntasAbiertasDificiles.RemoveAt(randomIndex);
    }
    #endregion

    #region paneles utiles
    public void mostrarPanelCorrectoPM()
    {
        panelCorrectoPM.SetActive(true);
        panelIncorrectoPM.SetActive(false);

    }

    public void mostrarPanelIncorrectoPM()
    {
        panelCorrectoPM.SetActive(false);
        panelIncorrectoPM.SetActive(true);
    }
    public void mostrarPanelCorrectoFV()
    {
        panelCorrectoFV.SetActive(true);
        panelIncorrectoFV.SetActive(false);

    }

    public void mostrarPanelIncorrectoFV()
    {
        panelCorrectoFV.SetActive(false);
        panelIncorrectoFV.SetActive(true);
    }

    public void mostrarRespuestaAbierta()
    {
        panelRespuestaAbierta.SetActive(true);
        textRespuestaAbierta.text = respuestaAbierta;
    }
    #endregion
    #region comprobación respuestas FV
    public void comprobarRespuestaFalso()
    {
        if (respuestaFV.ToLower().Equals("false"))
        {
            mostrarPanelCorrectoFV();
            cantidadAciertos += 1;
            audioWin.Play();
        }
        else
        {
            mostrarPanelIncorrectoFV();
            cantidadErrores += 1;
            audioLose.Play();
        }
    }

    public void comprobarRespuestaVerdadero()
    {
        if (respuestaFV.ToLower().Equals("true"))
        {

            mostrarPanelCorrectoFV();
            cantidadAciertos += 1;
            audioWin.Play();
        }
        else
        {
            mostrarPanelIncorrectoFV();
            cantidadErrores += 1;
            audioLose.Play();
        }
    }
    #endregion


    #region comprobación respuestas PM
    public void comprobarRespuesta1()
    {
        if (textResp1.text.Equals(respuestaPM))
        {
            Debug.Log("Respuesta Correcta es la 1");
            mostrarPanelCorrectoPM();
            cantidadAciertos += 1;
            audioWin.Play();
        }
        else
        {
            Debug.Log("Respuesta Incorrecta es la 1");
            mostrarPanelIncorrectoPM();
            cantidadErrores += 1;
            audioLose.Play();
        }
    }

    public void comprobarRespuesta2()
    {
        if (textResp2.text.Equals(respuestaPM))
        {
            Debug.Log("Respuesta Correcta es la 2");
            mostrarPanelCorrectoPM();
            cantidadAciertos += 1;
            audioWin.Play();
        }
        else
        {
            Debug.Log("Respuesta Incorrecta es la 2");
            mostrarPanelIncorrectoPM();
            cantidadErrores += 1;
            audioLose.Play();
        }
    }
    public void comprobarRespuesta3()
    {
        if (textResp3.text.Equals(respuestaPM))
        {
            Debug.Log("Respuesta Correcta es la 3");
            mostrarPanelCorrectoPM();
            cantidadAciertos += 1;
            audioWin.Play();
        }
        else
        {
            Debug.Log("Respuesta Incorrecta es la 3");
            mostrarPanelIncorrectoPM();
            cantidadErrores += 1;
            audioLose.Play();
        }
    }
    public void comprobarRespuesta4()
    {
        if (textResp4.text.Equals(respuestaPM))
        {
            Debug.Log("Respuesta Correcta es la 4");
            mostrarPanelCorrectoPM();
            cantidadAciertos += 1;
            audioWin.Play();
        }
        else
        {
            Debug.Log("Respuesta Incorrecta es la 4");
            mostrarPanelIncorrectoPM();
            cantidadErrores += 1;
            audioLose.Play();
        }
    }
    #endregion


    public void filtrarPreguntas()
    {
        leerPreguntaMultiple.FiltrarPreguntasMultiples();
        leerPreguntaAbierta.FiltrarPreguntasAbiertas();
        leerPreguntaFalsoVerdadero.FiltrarPreguntasFV();


    }

    public void reiniciarJuego()
    {
        timerManager.TimerReset();
        timerManager.TimerStart();
        filtrarPreguntas();
        mostrarPreguntaAleatoriaFacil();
        botonGenerarPreguntaPMDificil.SetActive(false);
        botonGenerarPreguntaFVDificil.SetActive(false);
        botonGenerarPreguntaADificil.SetActive(false);
        botonGenerarPreguntaPMFacil.SetActive(true);
        botonGenerarPreguntaFVFacil.SetActive(true);
        botonGenerarPreguntaAFacil.SetActive(true);
        cantidadErrores = 0;
        cantidadAciertos = 0;
    }
    public void mostrarResultados()
    {
        textAciertos.text = cantidadAciertos.ToString();
        textErrores.text = cantidadErrores.ToString();
    }
}
