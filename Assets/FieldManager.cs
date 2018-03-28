using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{

    public GameManager gamemanager;
    public OSCManager oscmanager;
    public GameObject Spawn;
    public GameObject Field;

    //Team ID
    public List<int> spawned_teams;
    //Player Count per Team
    public List<int> spawned_teams_count;

    //Blocks
    public List<GameObject> GameFieldList;
    //Team Field
    public List<GameObject> GameFieldTeam;

    //Check for creation
    public bool field_created = false;

    //Spawning List for Blocks in Order
    public List<Transform> BlockSpawn;

    //Spawn List for GameManager
    public List<Transform> PlayerSpawn;
    
    //Block Skins
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
        Debug.Log("Creating Game Field");
        //Calculate Information
        List<GameObject> clients = oscmanager.ConnectionList;
        Debug.Log("Client Count: " + clients.Count);
        //Analyze all Clients
        for (int i = 0; i<clients.Count;++i)
        {
            int temp_team = clients[i].GetComponent<Client>().teamID;
            bool found = false;
            for (int o = 0; o<spawned_teams.Count; ++o)
            {
                //Team already found
                if (temp_team == spawned_teams[o])
                {
                    ++spawned_teams_count[o];
                    found = true;
                }               
            }
            if (!found)
            {
                //Add Team Count if not found already
                spawned_teams.Add(temp_team);
                spawned_teams_count.Add(1);
            }
        }

        //DEBUG INFORMATION
        Debug.Log("Team ID List:");
        for(int i = 0;i<spawned_teams.Count ;++i)
        {
            Debug.Log("Team: " + spawned_teams[i] + " Player Count: "+ spawned_teams_count[i]);
        }
        //

        /*
        //add first Transform Element with angle 0
        BlockSpawn.Add(Spawn.transform);

        // 1v1 2v2 3ffa 4ffa
        for (int i = 1; i < spawned_teams.Count; ++i)
        {
            Transform temp_block = Spawn.transform;
            float rotation_angle = 0;
            rotation_angle = i * (360 / spawned_teams.Count);

            temp_block.rotation = new Quaternion(temp_block.rotation.x,rotation_angle,temp_block.rotation.z,1);
            BlockSpawn.Add(temp_block);
        }
        */

        //Create Block Spawns
        BlockSpawn.Add(Spawn.transform);
        for(int i  = 0; i < spawned_teams.Count-1 ;++i)
        {
            Transform temp_block = Instantiate(Spawn.transform) as Transform;

            float rotation_angle = 360/clients.Count;
            rotation_angle = rotation_angle * spawned_teams_count[i] + BlockSpawn[BlockSpawn.Count-1].eulerAngles.y;
            Debug.Log("Rotation Angle:" + rotation_angle);
            temp_block.rotation = Quaternion.Euler(0,rotation_angle,0);
            Debug.Log("Spawn Block with Rotation:" + temp_block.eulerAngles);
            BlockSpawn.Add(temp_block);
        }

        //Spawn Blocks
        for (int i = 0; i < BlockSpawn.Count; ++i)
        {
            GameObject temp_block = Instantiate(FieldBlock[gamemanager.fieldblockskin], BlockSpawn[i]) as GameObject;
            temp_block.transform.parent = gameObject.transform;
            GameFieldList.Add(temp_block);
        }
        Debug.Log("Spawned Block Count: " + GameFieldList.Count);


        //Spawn Fields
        for (int o = 0; o<spawned_teams.Count;++o)
        {
            //Calculate mid rotation angle
            Quaternion rotation_object;
            if (o == spawned_teams.Count - 1)
            {
                rotation_object = Quaternion.Euler(-8, 180 + BlockSpawn[o].eulerAngles.y / 2,0);
            }
            else
            {
                rotation_object = Quaternion.Euler(-8, BlockSpawn[o].eulerAngles.y / 2 + BlockSpawn[o + 1].eulerAngles.y / 2, 0);
            }
            Transform temp_transform = Instantiate(Spawn.transform) as Transform;
            temp_transform.rotation = rotation_object;

            //Create Field and set Radius
            GameObject temp_field = Instantiate(Field,temp_transform) as GameObject;
            temp_field.transform.parent = gameObject.transform;
            temp_field.GetComponentInChildren<GameField>().teamID = spawned_teams[o];

            //Calculate Radius
            float distance_radius = Vector3.Distance(GameFieldList[o].transform.GetChild(0).position,temp_field.transform.GetChild(0).position);
            temp_field.GetComponentInChildren<SphereCollider>().radius = distance_radius;
            Debug.Log("Field Rotation: " +  temp_field.transform.eulerAngles);
            Debug.Log("Field Radius: " + temp_field.GetComponentInChildren<SphereCollider>().radius);
            Debug.Log("Field ID: " + temp_field.GetComponentInChildren<GameField>().teamID);
            GameFieldTeam.Add(temp_field);

        }
        Debug.Log("Spawned Teams: " + spawned_teams.Count);
        Debug.Log("Client Count: " + clients.Count);
        //Create Player Spawn
        for (int i =0;i<clients.Count;++i)
        {
            int spawned_count_team = 1;
            for (int a = 0;a<i ;++a)
            {
                if (clients[i].GetComponent<Client>().teamID == clients[a].GetComponent<Client>().teamID)
                {
                    ++spawned_count_team;
                }
            }

            //Find Team
            for (int o = 0; o<spawned_teams.Count;++o)
            {
                if (spawned_teams[o] == clients[i].GetComponent<Client>().teamID)
                {
                    Transform rotation_object = Instantiate(Spawn.transform) as Transform;
                    float rotation_angle = 0;
                    if (spawned_teams_count[o] > 1)
                    {
                        //2v2 other combination
                        if (o == spawned_teams.Count - 1)
                        {
                            rotation_angle = 360 - BlockSpawn[o].eulerAngles.y;
                        }
                        else
                        {
                            rotation_angle = BlockSpawn[o+1].eulerAngles.y - BlockSpawn[o].eulerAngles.y;
                        }
                        
                        //Set Angle between two Blocks 
                        float player_rotation = rotation_angle/(spawned_teams_count[o] + 1) * spawned_count_team;
                        
                        //Add First Block Rotation
                        rotation_angle += BlockSpawn[o].eulerAngles.y;

                        rotation_object.rotation = Quaternion.Euler(0, rotation_angle,0);
                    }
                    else
                    {
                        //1v1 3ffa 4ffa
                        if (o == spawned_teams.Count - 1)
                        {
                            rotation_object.rotation = Quaternion.Euler(0, 180 + BlockSpawn[o].eulerAngles.y / 2, 0);
                        }
                        else
                        {
                            rotation_object.rotation = Quaternion.Euler(0, BlockSpawn[o].eulerAngles.y / 2 + BlockSpawn[o + 1].eulerAngles.y / 2, 0);
                        }
                    }
                    Debug.Log("Player Rotation: " + rotation_object.eulerAngles);
                    PlayerSpawn.Add(rotation_object);
                }
            }
        }
        Debug.Log("Game Field Created");
        field_created = true;
    }

    public void ClearGameField()
    {
        BlockSpawn.Clear();
        GameFieldList.Clear();
        GameFieldTeam.Clear();
        PlayerSpawn.Clear();
        spawned_teams.Clear();
        spawned_teams_count.Clear();
        field_created  = false;
    }


}
