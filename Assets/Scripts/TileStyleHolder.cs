using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileStyle
{
    public int Number;
    public Color32 TileColor;
    public Color32 TextColor;
}

public class TileStyleHolder : MonoBehaviour
{
    public static TileStyleHolder Instance;

    public TileStyle[] TileStyles;

    private void Awake()
    {
        Instance = this;
    }
}
