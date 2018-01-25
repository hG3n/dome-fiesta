using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;

public class ControlManager : MonoBehaviour {

    public OSC osc;
    public string ip;
    public List<string> Controls;
    public List<string> OSCIPs;
    public List<GameObject> Controller;
    public GameObject Unit;

	// Use this for initialization
	void Start ()
    {
        Debug.Log("Host IP: "+LocalIPAddress());
        ip = LocalIPAddress();
        osc.SetAddressHandler("/connect",GetNewPlayer);
        
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void AddControl(string source)
    {
        Controls.Add(source);
    }

    void SendID(string adress, int id)
    {

    }

    void GetNewPlayer(OscMessage message)
    {
        string _ip = "";
        for (int i = 0; i < 4; ++i)
        {
            _ip += message.GetInt(i).ToString();
            if (i != 3)
            {
                _ip += ".";
            }
        }
        for (int i = 0; i < OSCIPs.Count; ++i)
        {
            if (_ip ==OSCIPs[i])
            {
                return;
            }
        }
        OSCIPs.Add(_ip);
        Controller.Add(Unit);
        Controller[Controller.Count - 1].GetComponent<ControlUnit>().Source = OSCIPs[OSCIPs.Count];
        Debug.Log("Connection received: " + ip);
    }


    void SendMessage(string adress, string ip, string _message)
    {
    }

    public string LocalIPAddress()
    {
        IPHostEntry host;
        string localIP = "";
        host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                localIP = ip.ToString();
                break;
            }
        }
        return localIP;
    }
}
