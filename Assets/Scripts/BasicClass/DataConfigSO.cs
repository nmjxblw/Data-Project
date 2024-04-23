using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[System.Serializable]
public class DataConfig : IComparable<DataConfig>
{
    public string gameName;
    public float totalTime;
    public float price;
    public int reviews;
    public float rating;
    public string imageName;
    public Sprite sprite => Resources.Load<Sprite>("Images/" + imageName);
    public int CompareTo(DataConfig other)
    {
        return gameName.CompareTo(other.gameName);
    }
}
