using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Purchasing;

[System.Serializable]
public class Dialogue {

    public string dialogueName;
    public string name;

    public string language = "ptbr/eng/ita";

    [TextArea(3, 10)]
    public string[] sentences;

    public bool isDone = false;

    public string GetHash() {
        int prime = 31;
        int result = 1;
        foreach (var s in sentences) {
            result = result * prime + s.GetHashCode();
        }
        return result.ToString();
    }

    public void localize() {
        for (int i = 0; i < sentences.Count(); i++) {
            var op = LocalizationSettings.StringDatabase.GetLocalizedString(dialogueName, i.ToString());
            sentences[i] = op;
        }

    }
}
