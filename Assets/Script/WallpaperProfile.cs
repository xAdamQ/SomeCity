using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WallpaperType { Transetion, Shift }

[System.Serializable, CreateAssetMenu(fileName = "Wallpaper Profile", menuName = "Mine/Wallpaper Profile")]
public class WallpaperProfile : ScriptableObject
{
    public WallpaperType WallpaperType;
    public Color[] Palette;
    //public float EndColorPercantage = 1;
    public float MixTime;

}
