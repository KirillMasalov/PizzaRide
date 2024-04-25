using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerPrefsExtensions
{ 
   public static float GetFloatOrDefault(string key, float defaultValue)
   {
      return PlayerPrefs.HasKey(key) ? PlayerPrefs.GetFloat(key) : defaultValue;
   }
   public static string GetStringOrDefault(string key, string defaultValue)
   {
      return PlayerPrefs.HasKey(key) ? PlayerPrefs.GetString(key) : defaultValue;
   }
   public static int GetIntOrDefault(string key, int defaultValue)
   {
      return PlayerPrefs.HasKey(key) ? PlayerPrefs.GetInt(key) : defaultValue;
   }
}
