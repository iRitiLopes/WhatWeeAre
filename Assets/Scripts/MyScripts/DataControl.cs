using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
class PlayerData
{
    public bool[] Level1Itens = new bool[2];
}

public class DataControl : MonoBehaviour {

    public DataControl dataControl;

    private string filePath;

    private bool[] Level1Itens = new bool[2];


    void Awake()
    {
        if(dataControl == null)
        {
            dataControl = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        filePath = Application.persistentDataPath + "/playerInfo.dat";
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(filePath);
        PlayerData data = new PlayerData();

        //conteudo a ser salvo ex: data.variavel = variavel
        data.Level1Itens = Level1Itens;

        bf.Serialize(file, data);

        file.Close();
    }

    public void Load()
    {
        if (File.Exists(filePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filePath, FileMode.Open);

            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            //conteudo a ser carregado ex: variavel = data.variavel;
            Level1Itens = data.Level1Itens;
        }
    }

    public void SetLevel1Itens (bool value, int index)
    {
        Level1Itens[index] = value;
    }

    public bool GetLevel1Itens(int index)
    {
        return Level1Itens[index];
    }
}
