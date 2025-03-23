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
    string lineaLeida = "";

    List<PreguntaFalsoVerdadero> listaPreguntasFV;
    List<PreguntaFalsoVerdadero> listaPreguntasFVFaciles;
    List<PreguntaFalsoVerdadero> listaPreguntasFVDificiles;
    List<PreguntaAbierta> listaPreguntasAbiertas;
    List<PreguntaAbierta> listaPreguntasAbiertasFaciles;
    List<PreguntaAbierta> listaPreguntasAbiertasDificiles;
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

    // Start is called before the first frame update
    void Start()
    {
        listaPreguntasFV = new List<PreguntaFalsoVerdadero>();
        listaPreguntasFVFaciles = new List<PreguntaFalsoVerdadero>();
        listaPreguntasFVDificiles = new List<PreguntaFalsoVerdadero>();
        listaPreguntasAbiertas = new List<PreguntaAbierta>();
        listaPreguntasAbiertasFaciles = new List<PreguntaAbierta>();
        listaPreguntasAbiertasDificiles = new List<PreguntaAbierta>();
        lecturaPreguntas();
        FiltrarPreguntasFaciles();
        FiltrarPreguntasDificiles();
        mostrarPreguntaAleatoriaFacil();


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void mostrarPreguntaAleatoriaFacil()
    {
        List<int> listaNumerosAleatorios = new List<int>();

        if (leerPreguntaMultiple.listaPreguntasMultiplesFaciles.Count > 0) listaNumerosAleatorios.Add(1);
        if (listaPreguntasFVFaciles.Count > 0) listaNumerosAleatorios.Add(2);
        if (listaPreguntasAbiertasFaciles.Count > 0) listaNumerosAleatorios.Add(3);

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
        if (listaPreguntasFVDificiles.Count > 0) listaNumerosAleatorios.Add(2);
        if (listaPreguntasAbiertasDificiles.Count > 0) listaNumerosAleatorios.Add(3);

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

        int randomIndex = UnityEngine.Random.Range(0, listaPreguntasFVFaciles.Count);
        preguntaActualFV = listaPreguntasFVFaciles[randomIndex];

        textPreguntaFV.text = preguntaActualFV.Pregunta;
        respuestaFV = preguntaActualFV.Respuesta;
        listaPreguntasFVFaciles.RemoveAt(randomIndex);
    }
    public void mostrarPreguntasFVDificiles()
    {
        panelPreguntasPM.SetActive(false);
        panelPreguntasFV.SetActive(true);
        panelPreguntasAbiertas.SetActive(false);

        int randomIndex = UnityEngine.Random.Range(0, listaPreguntasFVDificiles.Count);
        preguntaActualFV = listaPreguntasFVDificiles[randomIndex];

        textPreguntaFV.text = preguntaActualFV.Pregunta;
        respuestaFV = preguntaActualFV.Respuesta;
        listaPreguntasFVDificiles.RemoveAt(randomIndex);
    }
    public void mostrarPreguntasAbiertasFaciles()
    {
        panelPreguntasPM.SetActive(false);
        panelPreguntasFV.SetActive(false);
        panelPreguntasAbiertas.SetActive(true);

        int randomIndex = UnityEngine.Random.Range(0, listaPreguntasAbiertasFaciles.Count);
        preguntaActualAbierta = listaPreguntasAbiertasFaciles[randomIndex];

        textPreguntaAbierta.text = preguntaActualAbierta.Pregunta;
        respuestaAbierta = preguntaActualAbierta.Respuesta;
        listaPreguntasAbiertasFaciles.RemoveAt(randomIndex);
    }
    public void mostrarPreguntasAbiertasDificiles()
    {
        panelPreguntasPM.SetActive(false);
        panelPreguntasFV.SetActive(false);
        panelPreguntasAbiertas.SetActive(true);

        int randomIndex = UnityEngine.Random.Range(0, listaPreguntasAbiertasDificiles.Count);
        preguntaActualAbierta = listaPreguntasAbiertasDificiles[randomIndex];

        textPreguntaAbierta.text = preguntaActualAbierta.Pregunta;
        respuestaAbierta = preguntaActualAbierta.Respuesta;
        listaPreguntasAbiertasDificiles.RemoveAt(randomIndex);
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
        }
        else
        {
            mostrarPanelIncorrectoFV();
            cantidadErrores += 1;
        }
    }

    public void comprobarRespuestaVerdadero()
    {
        if (respuestaFV.ToLower().Equals("true"))
        {

            mostrarPanelCorrectoFV();
            cantidadAciertos += 1;
        }
        else
        {
            mostrarPanelIncorrectoFV();
            cantidadErrores += 1;
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
        }
        else
        {
            Debug.Log("Respuesta Incorrecta es la 1");
            mostrarPanelIncorrectoPM();
            cantidadErrores += 1;
        }
    }

    public void comprobarRespuesta2()
    {
        if (textResp2.text.Equals(respuestaPM))
        {
            Debug.Log("Respuesta Correcta es la 2");
            mostrarPanelCorrectoPM();
            cantidadAciertos += 1;
        }
        else
        {
            Debug.Log("Respuesta Incorrecta es la 2");
            mostrarPanelIncorrectoPM();
            cantidadErrores += 1;
        }
    }
    public void comprobarRespuesta3()
    {
        if (textResp3.text.Equals(respuestaPM))
        {
            Debug.Log("Respuesta Correcta es la 3");
            mostrarPanelCorrectoPM();
            cantidadAciertos += 1;
        }
        else
        {
            Debug.Log("Respuesta Incorrecta es la 3");
            mostrarPanelIncorrectoPM();
            cantidadErrores += 1;
        }
    }
    public void comprobarRespuesta4()
    {
        if (textResp4.text.Equals(respuestaPM))
        {
            Debug.Log("Respuesta Correcta es la 4");
            mostrarPanelCorrectoPM();
            cantidadAciertos += 1;
        }
        else
        {
            Debug.Log("Respuesta Incorrecta es la 4");
            mostrarPanelIncorrectoPM();
            cantidadErrores += 1;
        }
    }
    #endregion

    public void lecturaPreguntas()
    {
        leerPreguntaMultiple.LecturaPreguntasMultiples();
        LecturaPreguntasFalsoVerdadero();
        LecturaPreguntasAbiertas();
    }
    #region Lectura archivos


    public void LecturaPreguntasFalsoVerdadero()
    {
        try
        {
            StreamReader sr1 = new StreamReader("Assets/Files/preguntasFalso_Verdadero.txt");
            while ((lineaLeida = sr1.ReadLine()) != null)
            {
                string[] lineaPartida = lineaLeida.Split("-");
                string pregunta = lineaPartida[0];
                string respuesta = lineaPartida[1];
                string versiculo = lineaPartida[2];
                string difucltad = lineaPartida[3];

                PreguntaFalsoVerdadero objPFV = new PreguntaFalsoVerdadero(pregunta, respuesta, versiculo, difucltad);

                listaPreguntasFV.Add(objPFV);

            }
            Debug.Log("El tamaño de la lista es " + listaPreguntasFV.Count);
        }
        catch (Exception e)
        {
            Debug.Log("ERROR!!!!! " + e.ToString());
        }
        finally
        { Debug.Log("Executing finally block."); }
    }

    public void LecturaPreguntasAbiertas()
    {
        try
        {
            StreamReader sr1 = new StreamReader("Assets/Files/ArchivoPreguntasAbiertas.txt");
            while ((lineaLeida = sr1.ReadLine()) != null)
            {
                string[] lineaPartida = lineaLeida.Split("-");
                string pregunta = lineaPartida[0];
                string respuesta = lineaPartida[1];
                string versiculo = lineaPartida[2];
                string difucltad = lineaPartida[3];

                PreguntaAbierta objPA = new PreguntaAbierta(pregunta, respuesta, versiculo, difucltad);

                listaPreguntasAbiertas.Add(objPA);

            }
            Debug.Log("El tamaño de la lista es " + listaPreguntasAbiertas.Count);
        }
        catch (Exception e)
        {
            Debug.Log("ERROR!!!!! " + e.ToString());
        }
        finally
        { Debug.Log("Executing finally block."); }
    }
    #endregion

    #region filtraciones

    private void FiltrarPreguntasFVFaciles()
    {
        listaPreguntasFVFaciles = listaPreguntasFV.FindAll(p => p.Dificultad.ToLower() == "facil");
        Debug.Log("Total preguntas faciles: " + listaPreguntasFVFaciles.Count);
    }

    private void FiltrarPreguntasFVDificiles()
    {
        listaPreguntasFVDificiles = listaPreguntasFV.FindAll(p => p.Dificultad.ToLower() == "dificil");
        Debug.Log("Total preguntas dificiles: " + listaPreguntasFVDificiles.Count);
    }

    private void FiltrarPreguntasAbiertasFaciles()
    {
        listaPreguntasAbiertasFaciles = listaPreguntasAbiertas.FindAll(p => p.Dificultad.ToLower() == "facil");
        Debug.Log("Total preguntas faciles: " + listaPreguntasAbiertasFaciles.Count);
    }

    private void FiltrarPreguntasAbiertasDificiles()
    {
        listaPreguntasAbiertasDificiles = listaPreguntasAbiertas.FindAll(p => p.Dificultad.ToLower() == "dificil");
        Debug.Log("Total preguntas dificiles: " + listaPreguntasAbiertasDificiles.Count);
    }

    private void FiltrarPreguntasFaciles()
    {
        leerPreguntaMultiple.FiltrarPreguntasMultiplesFaciles();
        FiltrarPreguntasFVFaciles();
        FiltrarPreguntasAbiertasFaciles();
    }

    private void FiltrarPreguntasDificiles()
    {
        leerPreguntaMultiple.FiltrarPreguntasMultiplesDificiles();
        FiltrarPreguntasFVDificiles();
        FiltrarPreguntasAbiertasDificiles();
    }

    public void reiniciarJuego()
    {
        FiltrarPreguntasFaciles();
        FiltrarPreguntasDificiles();
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




#endregion
