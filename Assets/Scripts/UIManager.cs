using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject Gamemanager;
    public GameObject HomeScreen;
    public List<Text> Adress;

    public GameObject Camera;



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {


    }

    private void OnDisable()
    {

    }

    public void UpdateAdress(string adress)
    {
        for (int i = 0; i < Adress.Count; ++i)
        {
            Adress[i].text = adress;
        }
    }

    public void StartGame()
    {
        HomeScreen.active = false;
    }

    public void EndGame()
    {
        HomeScreen.active = true;
    }

}
