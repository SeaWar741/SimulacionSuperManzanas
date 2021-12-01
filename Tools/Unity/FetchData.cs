using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;
using SimpleJSON;

public class FetchData : MonoBehaviour
{
    public GameObject carPrefab;
    public GameObject busPrefab;
    public GameObject biciclePrefab;

    private JSONNode jsonData;
    private GameObject instance;
    private List<Tuple<float, float>> agentSteps = new List<Tuple<float, float>>();
    private bool isLoaded = false;

    private void Start()
    {
        StartCoroutine(GetData());
    }

    //GetData: Funcion de conexion con API. Se descarga una copia de un archivo JSON.
    public IEnumerator GetData(){
        //FetchData preguntas = new FetchData();

        string URL = "http://localhost:3000/simulate";

        UnityWebRequest infoRequest = UnityWebRequest.Get(URL);

        yield return infoRequest.SendWebRequest();

        if (infoRequest.isDone){
            jsonData = JSON.Parse(infoRequest.downloadHandler.text);

            //          jsonData["Bicicles"][0][0][0]);
            //                                     ^
            //                                  ^  0 - Posiciones / 1 - Rotacion
            //                               ^  Numero agente
            //                               Numero de step

            isLoaded = true;
            LoadData();
        }
    }

    //Crea las listas de steps de cada agente
    private void LoadData(){
        if (isLoaded){
            //instanciate a car for each element in the json file with the data for each step Cars
            for (int i = 0; i < jsonData["Cars"][0].Count; i++){
                //fiill step list of positions
                List<Tuple<float, float>> steps = new List<Tuple<float, float>>();
                for (int j = 0; j < 150; j++){
                    float x1 = float.Parse(jsonData["Cars"][j][i][0][0]);
                    float z1 = float.Parse(jsonData["Cars"][j][i][0][1]);
                    Tuple<float, float> step = new Tuple<float, float>(x1, z1);
                    steps.Add(step);
                }
                //fiill step list of orientations
                List<int> rotations = new List<int>();
                for (int j = 0; j < 150; j++){
                    int rotation = int.Parse(jsonData["Cars"][j][i][1]); 
                    rotations.Add(rotation);
                    //0 arriba
                    //1 derecha
                    //2 abajo
                    //3 izquierda
                }
                
                instance = Instantiate(carPrefab, new Vector3(steps[0].Item1, 0, steps[0].Item2), Quaternion.identity);
                instance.AddComponent<AgentBehavior>();
                instance.GetComponent<AgentBehavior>().steps = steps;
                instance.GetComponent<AgentBehavior>().orientation = rotations;
            }

            //instanciate a bus for each element in the json file with the data for each step Buses
            for (int i = 0; i < jsonData["Buses"][0].Count; i++){
                //fiill step list of positions
                List<Tuple<float, float>> steps = new List<Tuple<float, float>>();
                for (int j = 0; j < 150; j++){
                    float x1 = float.Parse(jsonData["Buses"][j][i][0][0]);
                    float z1 = float.Parse(jsonData["Buses"][j][i][0][1]);
                    Tuple<float, float> step = new Tuple<float, float>(x1, z1);
                    steps.Add(step);
                }
                //fiill step list of orientations
                List<int> rotations = new List<int>();
                for (int j = 0; j < 150; j++){
                    int rotation = int.Parse(jsonData["Buses"][j][i][1]); 
                    rotations.Add(rotation);
                    //0 arriba
                    //1 derecha
                    //2 abajo
                    //3 izquierda
                }
                
                instance = Instantiate(busPrefab, new Vector3(steps[0].Item1, 0, steps[0].Item2), Quaternion.identity);
                instance.AddComponent<AgentBehavior>();
                instance.GetComponent<AgentBehavior>().steps = steps;
                instance.GetComponent<AgentBehavior>().orientation = rotations;
            }

            //instanciate a bicicle for each element in the json file with the data for each step bicicle
            for (int i = 0; i < jsonData["Bicicles"][0].Count; i++){
                //fiill step list of positions
                List<Tuple<float, float>> steps = new List<Tuple<float, float>>();
                for (int j = 0; j < 150; j++){
                    float x1 = float.Parse(jsonData["Bicicles"][j][i][0][0]);
                    float z1 = float.Parse(jsonData["Bicicles"][j][i][0][1]);
                    Tuple<float, float> step = new Tuple<float, float>(x1, z1);
                    steps.Add(step);
                }
                //fiill step list of orientations
                List<int> rotations = new List<int>();
                for (int j = 0; j < 150; j++){
                    int rotation = int.Parse(jsonData["Bicicles"][j][i][1]); 
                    rotations.Add(rotation);
                    //0 arriba
                    //1 derecha
                    //2 abajo
                    //3 izquierda
                }
                
                instance = Instantiate(biciclePrefab, new Vector3(steps[0].Item1, 0, steps[0].Item2), Quaternion.identity);
                instance.AddComponent<AgentBehavior>();
                instance.GetComponent<AgentBehavior>().steps = steps;
                instance.GetComponent<AgentBehavior>().orientation = rotations;
            }

        }
    }
}