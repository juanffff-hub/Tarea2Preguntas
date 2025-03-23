using models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LeerPM : MonoBehaviour
{
    string lineaLeida = "";
    public List<PreguntaMultiple> listaPreguntasMultiples;
    public List<PreguntaMultiple> listaPreguntasMultiplesFaciles;
    public List<PreguntaMultiple> listaPreguntasMultiplesDificiles;
    string respuestaPM;

    // Start is called before the first frame update
    void Start()
    {
        listaPreguntasMultiples = new List<PreguntaMultiple>();
        listaPreguntasMultiplesFaciles = new List<PreguntaMultiple>();
        listaPreguntasMultiplesDificiles = new List<PreguntaMultiple>();
        LecturaPreguntasMultiples();
        FiltrarPreguntasMultiples();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LecturaPreguntasMultiples()
    {
        try
        {
            StreamReader sr1 = new StreamReader("Assets/Files/ArchivoPreguntasM.txt");
            while ((lineaLeida = sr1.ReadLine()) != null)
            {
                string[] lineaPartida = lineaLeida.Split("-");
                string pregunta = lineaPartida[0];
                string respuesta1 = lineaPartida[1];
                string respuesta2 = lineaPartida[2];
                string respuesta3 = lineaPartida[3];
                string respuesta4 = lineaPartida[4];
                string respuestaCorrecta = lineaPartida[5];
                string versiculo = lineaPartida[6];
                string difucltad = lineaPartida[7];

                PreguntaMultiple objPM = new PreguntaMultiple(pregunta, respuesta1, respuesta2, respuesta3,
                    respuesta4, respuestaCorrecta, versiculo, difucltad);

                listaPreguntasMultiples.Add(objPM);

            }
            Debug.Log("El tamaño de la lista es " + listaPreguntasMultiples.Count);
        }
        catch (Exception e)
        {
            Debug.Log("ERROR!!!!! " + e.ToString());
        }
        finally
        { Debug.Log("Executing finally block."); }
    }

    public void FiltrarPreguntasMultiplesFaciles()
    {
        listaPreguntasMultiplesFaciles = listaPreguntasMultiples.FindAll(p => p.Dificultad.ToLower() == "facil");
        Debug.Log("Total preguntas fáciles: " + listaPreguntasMultiplesFaciles.Count);
    }

    public void FiltrarPreguntasMultiplesDificiles()
    {
        listaPreguntasMultiplesDificiles = listaPreguntasMultiples.FindAll(p => p.Dificultad.ToLower() == "dificil");
        Debug.Log("Total preguntas dificiles: " + listaPreguntasMultiplesDificiles.Count);
    }

    public void FiltrarPreguntasMultiples()
    {
        FiltrarPreguntasMultiplesFaciles();
        FiltrarPreguntasMultiplesDificiles();
    }
}
