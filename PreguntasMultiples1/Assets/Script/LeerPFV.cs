using models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LeerPFV : MonoBehaviour
{
    string lineaLeida = "";
    public List<PreguntaFalsoVerdadero> listaPreguntasFV;
    public List<PreguntaFalsoVerdadero> listaPreguntasFVFaciles;
    public List<PreguntaFalsoVerdadero> listaPreguntasFVDificiles;
    string respuestaFV;
    // Start is called before the first frame update
    void Start()
    {
        listaPreguntasFV = new List<PreguntaFalsoVerdadero>();
        listaPreguntasFVFaciles = new List<PreguntaFalsoVerdadero>();
        listaPreguntasFVDificiles = new List<PreguntaFalsoVerdadero>();
        LecturaPreguntasFalsoVerdadero();
        FiltrarPreguntasFV();

    }

    // Update is called once per frame
    void Update()
    {

    }
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

    public void FiltrarPreguntasFVFaciles()
    {
        listaPreguntasFVFaciles = listaPreguntasFV.FindAll(p => p.Dificultad.ToLower() == "facil");
        Debug.Log("Total preguntas faciles: " + listaPreguntasFVFaciles.Count);
    }

    public void FiltrarPreguntasFVDificiles()
    {
        listaPreguntasFVDificiles = listaPreguntasFV.FindAll(p => p.Dificultad.ToLower() == "dificil");
        Debug.Log("Total preguntas dificiles: " + listaPreguntasFVDificiles.Count);
    }

    public void FiltrarPreguntasFV()
    {
        FiltrarPreguntasFVFaciles();
        FiltrarPreguntasFVDificiles();
    }

}
