using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using extOSC;
public class OSCManager : MonoBehaviour
{

    public GameManager GameManager;
    public UIManager UIManager;
    public string localip;
    public int client_limit = 4;
    public bool all_ready = false;


    private OSCReceiver _receiver;
    private OSCTransmitter _transmitter;

    public bool sendadmin;

    //Receive
    private const string _osc_network_data = "/server/networkdata/";
    private const string _osc_gamemode = "/server/gamemode/";
    private const string _osc_replay = "/server/replay/";
    private const string _osc_server_start = "/server/start/";

    //Send
    private const string _osc_network_clients = "/server/networkclients/";
    private const string _osc_connect = "/client/connection/";
    private const string _osc_admin = "/client/admin/";
    private const string _osc_start = "/client/start/";
    private const string _osc_end = "/client/end/";
    private const string _osc_score = "/client/score/";

    public List<GameObject> ConnectionList;





    // Use this for initialization
    void Start()
    {
        _transmitter = gameObject.AddComponent<OSCTransmitter>();
        _transmitter.RemotePort = 6969;

        _receiver = gameObject.AddComponent<OSCReceiver>();
        _receiver.LocalPort = 7000;

        _receiver.Bind(_osc_network_data, ReceiveClient);
        _receiver.Bind(_osc_gamemode, ReceiveMode);
        _receiver.Bind(_osc_server_start, ReceiveServerStart);
        _receiver.Bind(_osc_replay, ReceiveReplay);
        localip = GetLocalIPAddress();
        UIManager.UpdateAdress(localip);
    }

    // Update is called once per frame
    void Update()
    {
        if (sendadmin)
        {
            SendAdmin();
            sendadmin = false;
        }
    }

    //Ready
    void ReceiveMode(OSCMessage message)
    {
        Debug.Log("Change Game Mode: " + message);
        GameManager.GetComponent<GameManager>().gamemode = message.Values[0].IntValue;
        GameManager.GetComponent<GameManager>().ballmode = message.Values[1].IntValue;
        GameManager.GetComponent<GameManager>().win_score = message.Values[2].IntValue;
        GameManager.GetComponent<GameManager>().ballskin = message.Values[3].IntValue;
        SendClients();
    }

    //Ready
    void ReceiveClient(OSCMessage message)
    {
        Debug.Log("Receive Client: " + message);
        bool found = false;
        int clientID = 0;
        string clientip = message.Values[1].StringValue;
        if (ConnectionList.Count != 0)
        {
            for (int i = 0; i < ConnectionList.Count; ++i)
            {
                if (clientip == ConnectionList[i].GetComponent<Client>().localip)
                {
                    found = true;
                    clientID = i;
                }
            }
        }

        if (!found)
        {
            if (ConnectionList.Count <= client_limit)
            {
                GameObject newclient = new GameObject("Client");
                newclient.AddComponent<Client>();
                newclient.GetComponent<Client>().name = message.Values[0].StringValue;
                newclient.GetComponent<Client>().localip = message.Values[1].StringValue;
                newclient.GetComponent<Client>().modelID = message.Values[2].IntValue;
                newclient.GetComponent<Client>().teamID = message.Values[3].IntValue;
                newclient.GetComponent<Client>().ready = message.Values[4].BoolValue;
                if (ConnectionList.Count == 0)
                {
                    newclient.GetComponent<Client>().admin = true;
                }
                ConnectionList.Add(newclient);

                //Sending Admin Rights to Client
                SendAdmin();
                CheckReady();
            }
        }
        else
        {
            ConnectionList[clientID].GetComponent<Client>().name = message.Values[0].StringValue;
            ConnectionList[clientID].GetComponent<Client>().localip = message.Values[1].StringValue;
            ConnectionList[clientID].GetComponent<Client>().modelID = message.Values[2].IntValue;
            ConnectionList[clientID].GetComponent<Client>().teamID = message.Values[3].IntValue;
            ConnectionList[clientID].GetComponent<Client>().ready = message.Values[4].BoolValue;
            CheckReady();
        }
        SendClients();
        CheckReady();
    }

    //Ready
    void ReceiveServerStart(OSCMessage message)
    {
        Debug.Log(message);
        CheckReady();
        if (all_ready)
        {
            if (ConnectionList.Count == 1)
            {
                GameObject temp_client = Instantiate(ConnectionList[0]) as GameObject;
                temp_client.GetComponent<Client>().teamID = 1;
                temp_client.GetComponent<Client>().modelID = 0;
                temp_client.GetComponent<Client>().name = "testbot";
                temp_client.GetComponent<Client>().ready = true;

                Debug.Log("Client Team ID" + temp_client.GetComponent<Client>().teamID);
                Debug.Log("Client Model ID" + temp_client.GetComponent<Client>().modelID);
                Debug.Log("Client Name " + temp_client.GetComponent<Client>().name);
                Debug.Log("Client Ready " + temp_client.GetComponent<Client>().ready);
                ConnectionList.Add(temp_client);
                
                GameManager.GameStart();
            }
            else
            {
                GameManager.GameStart();
            }
            
        }
        SendClients();
    }

    //Ready
    void ReceiveReplay(OSCMessage message)
    {
        CheckReady();
        if (all_ready)
        {
            GameManager.Rematch();
        }
        SendClients();
    }

    //Ready
    public void SendAdmin()
    {
        for (int i = 0; i < ConnectionList.Count; ++i)
        {
            if (ConnectionList[i].GetComponent<Client>().admin)
            {
                string ip = ConnectionList[i].GetComponent<Client>().localip;
                _transmitter.RemoteHost = ip;

                OSCMessage message = new OSCMessage(_osc_admin);
                message.AddValue(OSCValue.Bool(true));
                _transmitter.Send(message);
            }
        }
    }

    //Ready
    public void SendUpdateScore()
    {
        for (int i = 0; i < ConnectionList.Count; ++i)
        {
            string ip = ConnectionList[i].GetComponent<Client>().localip;
            _transmitter.RemoteHost = ip;

            OSCMessage message = new OSCMessage(_osc_score);
            for (int o = 0; o<GameManager.GetComponent<GameManager>().Teamscore.Count;++o)
            {
                message.AddValue(OSCValue.Int(GameManager.GetComponent<GameManager>().Teamscore[o]));
            }
            Debug.Log("Sending Score Update");
            _transmitter.Send(message);
        }
    }

    //Ready
    public void SendClients()
    {
        OSCMessage message = new OSCMessage(_osc_network_clients);
        for (int i =0;i<ConnectionList.Count;++i)
        {
            Client client = ConnectionList[i].GetComponent<Client>();
            message.AddValue(OSCValue.String(client.localip));
            message.AddValue(OSCValue.String(client.name));
            message.AddValue(OSCValue.Int(client.modelID));
            message.AddValue(OSCValue.Int(client.teamID));
            message.AddValue(OSCValue.Bool(client.ready));

        }

        for (int i = 0; i < ConnectionList.Count; ++i)
        {
            Client client = ConnectionList[i].GetComponent<Client>();
            _transmitter.RemoteHost = client.localip;
            _transmitter.Send(message);
        }

    }

    //Ready
    public string GetLocalIPAddress()
    {
        Debug.Log("Get Local IP Adress...");

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

    //Ready
    public void SendStartGame()
    {
        OSCMessage message = new OSCMessage(_osc_start);
        for (int i  = 0; i<ConnectionList.Count;++i)
        {
            _transmitter.RemoteHost = ConnectionList[i].GetComponent<Client>().localip;
            _transmitter.Send(message);
        }

    }

    //Ready 
    public void SendEndGame(int teamid)
    {
        OSCMessage message = new OSCMessage(_osc_end);
        message.AddValue(OSCValue.Int(teamid));
        for (int i = 0; i < ConnectionList.Count; ++i)
        {
            _transmitter.RemoteHost = ConnectionList[i].GetComponent<Client>().localip;
            _transmitter.Send(message);
        }
    }

    //Ready
    public void CheckReady()
    {
        Debug.Log("Check Ready");
        bool ready = true;
        for (int i = 0; i < ConnectionList.Count;++i)
        {
            if (!ConnectionList[i].GetComponent<Client>().ready)
            {
                ready = false;
            }
        }
        Debug.Log("All Ready: " + ready);
        all_ready =  ready;
    }


}