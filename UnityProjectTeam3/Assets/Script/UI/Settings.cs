using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "SettingsData", menuName = "SettingsData")]
public class SettingsData : ScriptableObject
{
    public bool sounds;
    public bool nightmode;
}
