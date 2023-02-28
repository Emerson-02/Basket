using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Json_S_L : MonoBehaviour
{
    private string dataP;
    Heroi h1;

    // Start is called before the first frame update
    void Start()
    {
        h1 = new Heroi();
        //h1.nome = "Marcelo";
        //h1.idade = 50;

        dataP = Path.Combine(Application.dataPath, "Heroi.json");
        //Save();

        Load();
        print(h1.nome + ":" + h1.idade);
    }

    public void Save()
    {
        string jsonString = JsonUtility.ToJson(h1);
        File.WriteAllText(dataP, jsonString);
    }

    public void Load()
    {
        string jsonString = File.ReadAllText(dataP);
        JsonUtility.FromJsonOverwrite(jsonString, h1);
    }


}

public class Heroi
{
    public string nome;
    public int idade;

}
