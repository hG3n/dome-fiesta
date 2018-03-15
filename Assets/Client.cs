using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;

    public class Client : MonoBehaviour
    {

        public string name;
        public string localip;

        public int modelID = 0;
        public int teamID = 1;

        public bool ready = false;
        public bool admin = false;



        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SendAdmin()
        {
        }

        public void SendClient()
        {
            
        }
    }
