using UnityEngine;
using System.Collections;

public class Class {

    private string name;
    private int levels;
    public string Name { get { return name; } }
    public int Levels { get { return levels; } }
    public Class (string name, int levels = 1)
    {
        this.name = name;
        this.levels = levels;
    }
    public void SetLevels(int levels)
    {
        this.levels = levels;
    }
}
