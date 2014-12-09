using UnityEngine;
using System.Collections;

public class Advance : MonoBehaviour {
    public bool Selection;
	void Start () {
        if (Selection)
        {
            Application.LoadLevel("SelectionMenu");
        }
        else
        {
            Application.LoadLevel("Loading");
        }
	}

}
