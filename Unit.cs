using UnityEngine;
using System;
using System.Collections.Generic;

public class Unit : MonoBehaviour
{

    public int tileX;
    public int tileZ;
    float tileXChange;
    float tileZChange;
    public TileMap map;
    public GameObject turnManager;


    public List<Node> currentPath = null;

    int moveSpeed;

    void Awake()
    {
        tileXChange = gameObject.transform.position.x;
        tileZChange = gameObject.transform.position.z;
        tileX = Convert.ToInt32(tileXChange);
        tileZ = Convert.ToInt32(tileZChange);
        //Debug.Log(tileX);
        //Debug.Log(tileZ);
    }
    void Update()
    {
        Player player = GetComponent<Player>();
        Enemies enemies = GetComponent<Enemies>();

        if (player != null)
        {
            if (currentPath != null && player.isPlaying == true && player.isPreformingAction == true && !player.actionNotAllowd)
            {
                Debug.Log(currentPath.Count);
                int currNode = 0;

                while (currNode < currentPath.Count - 1)
                {
                    Vector3 start = map.TileCoordToWorldCoord(currentPath[currNode].x, currentPath[currNode].z) +
                        new Vector3(0, 2, 0);
                    Vector3 end = map.TileCoordToWorldCoord(currentPath[currNode + 1].x, currentPath[currNode + 1].z) +
                        new Vector3(0, 2, 0);


                    Debug.DrawLine(start, end, Color.red);

                    currNode++;
                }
            }
        }
        else if (enemies != null)
        {

            if (currentPath != null && enemies.isPlaying == true && enemies.isPreformingAction == true && !enemies.actionNotAllowd)
            {
                Debug.Log(currentPath.Count);
                int currNode = 0;

                while (currNode < currentPath.Count - 1)
                {
                    Vector3 start = map.TileCoordToWorldCoord(currentPath[currNode].x, currentPath[currNode].z) +
                        new Vector3(0, 2, 0);
                    Vector3 end = map.TileCoordToWorldCoord(currentPath[currNode + 1].x, currentPath[currNode + 1].z) +
                        new Vector3(0, 2, 0);


                    Debug.DrawLine(start, end, Color.red);

                    currNode++;
                }
            }
        }
        else
        {
            Debug.LogError("Neither player nor enemy found.");
        }
    }

    public void MoveNextTile()
    {
        Player player = GetComponent<Player>();
        Enemies enemies = GetComponent<Enemies>();
        if (player != null)
        {
            if (player.isPlaying == true && player.isPreformingAction == true)
            {
                Debug.Log("Clicked");
                player.noOfActions -= 1;

                player.isPlaying = false;
                moveSpeed = player.speed;

                float remainingMovement = moveSpeed;
                while (remainingMovement > 0)
                {

                    if (currentPath == null)
                    {
                        Debug.Log("No Path");
                        return;
                    }

                    // Gets cost from current tile to next tile
                    remainingMovement -= map.CostToEnterTile(currentPath[0].x, currentPath[0].z, currentPath[1].x, currentPath[1].z);

                    //Moves us to next tile in sequence
                    tileX = currentPath[1].x;
                    tileZ = currentPath[1].z;

                    //Debug.Log(tileX);
                    //Debug.Log(tileZ);

                    transform.position = map.TileCoordToWorldCoord(tileX, tileZ);

                    //Removes first node from the path / Current path.
                    currentPath.RemoveAt(0);


                    if (currentPath.Count == 1)
                    {
                        //Only one tile left in the path, and it must be our destination -- we are standing on it
                        //Clear our pathfinding info

                        currentPath = null;
                        //break;
                    }
                }
            }
        }

        else if (enemies != null)
        {
            Debug.Log(gameObject);
            
                Debug.Log("Clicked");
        //        enemies.noOfActions -= 1;

                enemies.isPlaying = false;
                moveSpeed = enemies.speed;
            
            float remainingMovement = moveSpeed;
                while (remainingMovement > 0)
                {

                    if (currentPath == null)
                    {
                        Debug.Log("No Path");
                        return;
                    }

                    // Gets cost from current tile to next tile
                    remainingMovement -= map.CostToEnterTile(currentPath[0].x, currentPath[0].z, currentPath[1].x, currentPath[1].z);
                
                //Moves us to next tile in sequence
                tileX = currentPath[1].x;
                    tileZ = currentPath[1].z;

                    //Debug.Log(tileX);
                    //Debug.Log(tileZ);

                    transform.position = map.TileCoordToWorldCoord(tileX, tileZ);
                Debug.Log("Clicked2");
                //Removes first node from the path / Current path.
                currentPath.RemoveAt(0);


                    if (currentPath.Count == 1)
                    {
                        //Only one tile left in the path, and it must be our destination -- we are standing on it
                        //Clear our pathfinding info

                        currentPath = null;
                        //break;
                    }
                }
                //player.isPreformingAction = false;
            }
        
    }
}
