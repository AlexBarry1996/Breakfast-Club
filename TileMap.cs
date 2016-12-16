using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TileMap : MonoBehaviour
{

    public GameObject selectedUnit;
    public TileType[] tileTypes;
    MouseManager mm;
    public static int[,] tiles;
    Node[,] graph;
    public GameObject[] maptiles;
    public static bool indoorrange = false;
    public static bool opendoor1 = false;

    int mapSizeX = 30;
    int mapSizeZ = 30;

    void Update()
    {
        selectedUnit = mm.selectedObject;
        //selectedUnit = mm.enemySelectedObject;
        if (selectedUnit.GetComponent<Unit>().tileX == 20 && selectedUnit.GetComponent<Unit>().tileZ == 9 || selectedUnit.GetComponent<Unit>().tileX == 21 && selectedUnit.GetComponent<Unit>().tileZ == 9)
        {
            indoorrange = true;
        }
    }

    public void ClearMap()
    {
        if (indoorrange == true)
        {
            
            //Debug.Log("InPosition");
            foreach (GameObject element in maptiles)
            {
                Destroy(element);
            }


            Debug.Log("Tiles cleared");
            GenerateMapData();
            GenerateMapVisuals();
            Debug.Log("Tiles created");
            opendoor1 = false;
        }

    }

    void Start()
    {
        selectedUnit.GetComponent<Unit>().tileX = (int)selectedUnit.transform.position.x;
        selectedUnit.GetComponent<Unit>().tileZ = (int)selectedUnit.transform.position.z;
        selectedUnit.GetComponent<Unit>().map = this;

        GenerateMapData();
        GeneratePathfindingGraph();
        GenerateMapVisuals();
    
        mm = GameObject.FindObjectOfType<MouseManager>();
        maptiles = GameObject.FindGameObjectsWithTag("maptile");
    }

    

    public void GenerateMapData()
    {
        //Allocate our map tiles
        tiles = new  int[mapSizeX, mapSizeZ];

        //Initialize map to be grass
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int z = 0; z < mapSizeZ; z++)
            {
                tiles[x, z] = 2;
            }
        }
        tiles[7, 19] = 1;
        tiles[7, 18] = 1;
        tiles[7, 17] = 1;
        tiles[7, 16] = 1;
        tiles[7, 15] = 1;
        tiles[7, 14] = 1;
        if (opendoor1 == false)
        {
            tiles[20, 10] = 2;
            tiles[21, 10] = 2;
            
        }

        else if (opendoor1 == true)
        {
            
            tiles[20, 10] = 0;
            tiles[21, 10] = 0;
            opendoor1 = false;

        }

        tiles[8, 20] = 0;
        tiles[8, 19] = 0;
        tiles[8, 18] = 0;
        tiles[8, 17] = 0;
        tiles[8, 16] = 0;
        tiles[8, 15] = 0;
        tiles[8, 14] = 0;
        tiles[8, 13] = 0;
        tiles[8, 12] = 0;
        
        tiles[9, 20] = 0;
        tiles[9, 19] = 0;
        tiles[9, 18] = 0;
        tiles[9, 17] = 0;
        tiles[9, 16] = 0;
        tiles[9, 15] = 0;
        tiles[9, 14] = 0;
        tiles[9, 13] = 0;
        tiles[9, 12] = 0;

        tiles[10, 19] = 0;
        tiles[10, 18] = 0;
        tiles[10, 17] = 0;
        tiles[10, 16] = 0;
        tiles[10, 15] = 0;
        tiles[10, 14] = 0;

        tiles[11, 17] = 0;
        tiles[11, 16] = 0;

        tiles[12, 17] = 0;
        tiles[12, 16] = 0;

        tiles[13, 18] = 0;
        tiles[13, 17] = 0;
        tiles[13, 16] = 0;
        tiles[13, 15] = 0;
        tiles[13, 14] = 0;
        tiles[13, 13] = 0;

        tiles[14, 18] = 0;
        tiles[14, 17] = 0;
        tiles[14, 16] = 0;
        tiles[14, 15] = 0;
        tiles[14, 14] = 0;
        tiles[14, 13] = 0;

        tiles[15, 18] = 0;
        tiles[15, 17] = 0;
        tiles[15, 16] = 0;
        tiles[15, 15] = 0;
        tiles[15, 14] = 0;
        tiles[15, 13] = 0;

        tiles[16, 18] = 0;
        tiles[16, 17] = 0;
        tiles[16, 16] = 0;
        tiles[16, 15] = 0;
        tiles[16, 14] = 0;
        tiles[16, 13] = 0;
        tiles[16, 9] = 0;
        tiles[16, 8] = 0;
        tiles[16, 7] = 0;

        tiles[17, 15] = 0;
        tiles[17, 14] = 0;
        tiles[17, 9] = 0;
        tiles[17, 8] = 0;
        tiles[17, 7] = 0;
        tiles[17, 6] = 0;

        tiles[18, 17] = 0;
        tiles[18, 16] = 0;
        tiles[18, 15] = 0;
        tiles[18, 14] = 0;
        tiles[18, 13] = 0;
        tiles[18, 9] = 0;
        tiles[18, 8] = 0;
        tiles[18, 7] = 0;
        tiles[18, 6] = 0;
        tiles[18, 5] = 0;
        tiles[18, 4] = 0;

        tiles[19, 17] = 0;
        tiles[19, 16] = 0;
        tiles[19, 15] = 0;
        tiles[19, 14] = 0;
        tiles[19, 12] = 0;
        tiles[19, 9] = 0;
        tiles[19, 8] = 0;
        tiles[19, 7] = 0;
        tiles[19, 6] = 0;
        tiles[19, 5] = 0;
        tiles[19, 4] = 0;

        tiles[20, 17] = 0;
        tiles[20, 16] = 0;
        tiles[20, 15] = 0;
        tiles[20, 13] = 0;
        tiles[20, 12] = 0;
        tiles[20, 11] = 0;
        
        tiles[20, 9] = 0;
        tiles[20, 8] = 0;
        tiles[20, 7] = 0;
        tiles[20, 6] = 0;

        tiles[21, 17] = 0;
        tiles[21, 16] = 0;
        tiles[21, 15] = 0;
        tiles[21, 14] = 0;
        tiles[21, 13] = 0;
        tiles[21, 12] = 0;
        tiles[21, 11] = 0;
        tiles[21, 9] = 0;
        tiles[21, 8] = 0;
        tiles[21, 7] = 0;

        tiles[22, 17] = 0;
        tiles[22, 16] = 0;
        tiles[22, 15] = 0;
        tiles[22, 14] = 0;
        tiles[22, 13] = 0;
        tiles[22, 12] = 0;

        tiles[23, 16] = 0;
        tiles[23, 15] = 0;
        tiles[23, 14] = 0;
        tiles[23, 13] = 0;
        tiles[23, 12] = 0;
        //U-shaped mountain range        

        //if you want a whole row/colum to be the same tile type
        for (int i = 0; i < mapSizeX; i++)
        {
            //tiles[0, i] = 2;
            
        }
    }

    public float CostToEnterTile(int sourceX, int sourceZ, int targetX, int targetZ)
    {
        TileType tt = tileTypes[tiles[targetX, targetZ]];

        if (UnitCanEnterTile(targetX, targetZ) == false)
            return Mathf.Infinity;

        float cost = tt.movementCost;

        


        return cost;
    }

    void GeneratePathfindingGraph()
    {
        graph = new Node[mapSizeX, mapSizeZ];
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int z = 0; z < mapSizeZ; z++)
            {
                graph[x, z] = new Node();
                graph[x, z].x = x;
                graph[x, z].z = z;
            }
        }
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int z = 0; z < mapSizeZ; z++)
            {
                //This is 4 way connected map
                //Also works for 6-way hexes and 8-way tiles, n-way variable areas (like EU4)
                /* if(x > 0)
                  graph[x,y].neighbours.Add( graph[x-1, y]);
              if(x < mapSizeX-1)
                  graph[x,y].neighbours.Add( graph[x+1, y]);
              if(y > 0)
                  graph[x,y].neighbours.Add( graph[x, y-1]);
              if(y < mapSizeY-1)
                  graph[x,y].neighbours.Add( graph[x, y+1]);
                  */
                //8-way diagonal movement
                //Left
                if (x > 0)
                {
                    graph[x, z].neighbours.Add(graph[x - 1, z]);
                    if (z > 0)
                        graph[x, z].neighbours.Add(graph[x, z - 1]);
                    if (z < mapSizeZ - 1)
                        graph[x, z].neighbours.Add(graph[x, z + 1]);
                }

                //Right
                if (x < mapSizeX - 1)
                {
                    graph[x, z].neighbours.Add(graph[x + 1, z]);
                    if (z > 0)
                        graph[x, z].neighbours.Add(graph[x + 1, z - 1]);
                    if (z < mapSizeZ - 1)
                        graph[x, z].neighbours.Add(graph[x + 1, z + 1]);
                }
                //Straight, up and down.
                if (z > 0)
                    graph[x, z].neighbours.Add(graph[x, z - 1]);
                if (z < mapSizeZ - 1)
                    graph[x, z].neighbours.Add(graph[x, z + 1]);
            }
        }
    }


    void GenerateMapVisuals()
    {
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int z = 0; z < mapSizeZ; z++)
            {

                TileType tt = tileTypes[tiles[x, z]];

                GameObject go = (GameObject)Instantiate(tt.tileVisualPrefab, new Vector3(x, 0, z), Quaternion.identity);

                ClickableTile ct = go.GetComponent<ClickableTile>();
                ct.tileX = x;
                ct.tileZ = z;
                ct.map = this;
            }
        }
    }

    public Vector3 TileCoordToWorldCoord(int x, int z)
    {
        return new Vector3(x, 0, z);
    }

    public bool UnitCanEnterTile(int x, int z)
    {
        return tileTypes[tiles[x, z]].isWalkable;
    }

    public void GeneratePathTo(int x, int z)
    {
        //Makes current path == null at the start of path finding which causes player to not move, insted make current path = null at the end of the turn to avoid this 
        //Clears unit old path
        //selectedUnit.GetComponent<Unit>().currentPath = null;

        List<Node> currentPath = new List<Node>();

        if (UnitCanEnterTile(x, z) == false)
        {
            //clicked on mountain
            return;
        }


        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

        //Q -- list of ndes we haven't checked yet
        List<Node> unvisited = new List<Node>();

        
            Node source = graph[
                             selectedUnit.GetComponent<Unit>().tileX,
                             selectedUnit.GetComponent<Unit>().tileZ
                            ];
            Node target = graph[
                             x,
                             z
                             ];
        
        

        dist[source] = 0;
        prev[source] = null;

        //Initialize everything ot have infinity distance.
        foreach (Node v in graph)
        {
            if (v != source)
            {
                dist[v] = Mathf.Infinity;
                prev[v] = null;
            }

            unvisited.Add(v);

        }

        while (unvisited.Count > 0)
        {
            //u is univisted node with smallest dist

            Node u = null;

            foreach (Node possibleU in unvisited)
            {
                if (u == null || dist[possibleU] < dist[u])
                {
                    u = possibleU;
                }
            }

            if (u == target)
            {
                break; //Exit the while loop
            }

            unvisited.Remove(u);

            foreach (Node v in u.neighbours)
            {
                //float alt = dist[u] + u.DistanceTo(v);
                float alt = dist[u] + CostToEnterTile(u.x, u.z, v.x, v.z);
                if (alt < dist[v])
                {
                    dist[v] = alt;
                    prev[v] = u;
                }
            }
        }

        if (prev[target] == null)
        {
            //no route between target and the source
            return;
        }

        

        Node curr = target;

        //Step through the prev chain and add it to our path
        while (curr != null)
        {
            currentPath.Add(curr);
            curr = prev[curr];
        }

        currentPath.Reverse();

        //Debug.Log(currentPath.Count);
        

        

        selectedUnit.GetComponent<Unit>().currentPath = currentPath;
        
        Debug.Log(selectedUnit.GetComponent<Unit>().currentPath.Count);
    }

}