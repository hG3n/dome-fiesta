using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Component : MonoBehaviour {

    public List<GameObject> Select_objects;
    public int select_ui = -1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (select_ui > 0)
        {

        }
	}

    void SelectHorizontal()
    {
        ++select_ui;
        if (select_ui > Select_objects.Count)
        {
            select_ui = 0;
        }
        if (select_ui >= 0)
        {
            for (int i = 0; i < Select_objects.Count; ++i)
            {
                if (i == select_ui)
                {
                    Select_objects[i].active = true;
                }
                else
                {
                    Select_objects[i].active = false;
                }
            }
        }
    }

    void SelectVertical()
    {

    }
}
