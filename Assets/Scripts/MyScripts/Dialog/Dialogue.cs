using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Purchasing;

[System.Serializable]
public class Dialogue  {
    public string name;

    public string language = "ptbr/eng/ita";

    [TextArea(3, 10)]
    public string[] sentences;

    public bool isDone = false;

    public string GetHash(){
        int prime = 31;
        int result = 1;
        foreach(var s in sentences){
            result = result * prime + s.GetHashCode();
        }
        return result.ToString();
    }
}
