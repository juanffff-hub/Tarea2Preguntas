using models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LeerPA : MonoBehaviour
{
    string lineaLeida = "";
    public List<PreguntaAbierta> listaPreguntasAbiertas;
    public List<PreguntaAbierta> listaPreguntasAbiertasFaciles;
    public List<PreguntaAbierta> listaPreguntasAbiertasDificiles;
    string respuestaAbierta;
    // Start is called before the first frame update
    void Start()
    {
        listaPreguntasAbiertas = new List<PreguntaAbierta>();
        listaPreguntasAbiertasFaciles = new List<PreguntaAbierta>();
        listaPreguntasAbiertasDificiles = new List<PreguntaAbierta>();
        LecturaPreguntasAbiertas();
        FiltrarPreguntasAbiertas();
    }

    // Update is called once per frame
    void Update()
    {

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

    public void FiltrarPreguntasAbiertasFaciles()
    {
        listaPreguntasAbiertasFaciles = listaPreguntasAbiertas.FindAll(p => p.Dificultad.ToLower() == "facil");
        Debug.Log("Total preguntas faciles: " + listaPreguntasAbiertasFaciles.Count);
    }

    public void FiltrarPreguntasAbiertasDificiles()
    {
        listaPreguntasAbiertasDificiles = listaPreguntasAbiertas.FindAll(p => p.Dificultad.ToLower() == "dificil");
        Debug.Log("Total preguntas dificiles: " + listaPreguntasAbiertasDificiles.Count);
    }

    public void FiltrarPreguntasAbiertas()
    {
        FiltrarPreguntasAbiertasFaciles();
        FiltrarPreguntasAbiertasDificiles();
    }
}
