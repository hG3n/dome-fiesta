using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour {

    public GameManager gamemanager;
    public OSCManager oscmanager;
    public GameObject Spawn;
    public List<float> radiusList;
    public List<GameObject> GameFieldList;

    public List<Transform> BlockSpawn;
    public List<Transform> PlayerSpawn;
    public List<Transform> FieldSpawn;
    public List<GameObject> FieldBlock;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CreateGameField()
    {
        //Calculate Information
        List<GameObject> clients = oscmanager.ConnectionList;
        int team_count = 0;
        List<int> teams = new List<int>(4);
        for (int i = 0; i<clients.Count;++i)
        {
            if (teams[clients[i].GetComponent<Client>().teamID]==0)
            {
                ++team_count;
            }
            ++teams[clients[i].GetComponent<Client>().teamID];
        }

        //Creating Blocks
        if (team_count == clients.Count || team_count == clients.Count/2)
        {
            //add first Transform Element with angle 0
            BlockSpawn.Add(Spawn.transform);

            // 1v1 2v2 3ffa 4ffa
            for (int i = 1; i < team_count; ++i)
            {
                Transform temp_block = Spawn.transform;
                float rotation_angle = 0;
                rotation_angle = i * (360 / team_count);

                temp_block.rotation = new Quaternion(temp_block.rotation.x,rotation_angle,temp_block.rotation.z,1);
                BlockSpawn.Add(temp_block);
            }
        }
        else
        {
            //other combinations

        }

        //Create Player Spawns
        for (int i = 0; i<clients.Count;++i)
        {
            Transform temp_player = Spawn.transform;

            PlayerSpawn.Add(temp_player);
        }

        //Spawn Blocks
        for (int i =0;i<BlockSpawn.Count;++i)
        {
            GameObject temp_block = Instantiate(FieldBlock[gamemanager.fieldblockskin],BlockSpawn[i]) as GameObject;
            GameFieldList.Add(temp_block);
        }

        //Spawn Fields
        for (int o = 0; o<teams.Count;++o)
        {
            if (teams[o] != 0)
            {
                //Calculate mid rotation angle
                if (o ==team_count-1)
                {
                    //Quaternion rotation_object = (BlockSpawn[o].rotation + new Quaternion(0,360,0,1)) / 2;
                }
                else
                {
                    //Quaternion rotation_object = (BlockSpawn[o].rotation + BlockSpawn[o + 1].rotation) / 2;
                }
                    

            }
        }
    }

    public void ClearGameField()
    {
        GameFieldList.Clear();
    }


}
