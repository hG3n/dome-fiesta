using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject Gamemanager;
    public GameObject HomeScreen;
    public List<Text> Adress;
    public List<Text> Countdown;

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

    public IEnumerator CountDown()
    {
        for (int i=3; i>=0; --i)
        {
            yield return new WaitForSeconds(1.0f);
            for (int o = 0; o < Countdown.Count;++o)
            {
                Countdown[o].text = o.ToString();
            }
            if (i == 0)
            {
                for (int o = 0; o < Countdown.Count; ++o)
                {
                    Countdown[o].text = "START";
                }
            }
        }
        yield return new WaitForSeconds(0.3f);
        for (int o = 0; o < Countdown.Count; ++o)
        {
            Countdown[o].text = "";
        }
    }

}
