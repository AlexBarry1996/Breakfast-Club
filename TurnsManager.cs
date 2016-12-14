using UnityEngine;
using System.Collections;

public class TurnsManager : MonoBehaviour
{
    static public bool heroesTurn;
    static bool herosDoneMoving;
    static public int noOfHeroes;
    static public int herosNotMoved;
    static public int noOfHerosrefreshed;

    static bool enemiesDoneMoving;
    static public int noOfEnemies;
    static public int enemiesNotMoved;

    bool heroActionsRestored;
    bool enemyActionsRestored;
   

    // Use this for initialization
    void Start()
    {
        noOfHeroes = 4;
        herosNotMoved = noOfHeroes;


        heroesTurn = false;
        enemiesDoneMoving = true;

        noOfEnemies = 4;
        enemiesNotMoved = noOfEnemies;

        bool heroActionsRestored = false;
        bool enemyActionsRestored = false;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (herosDoneMoving == true && enemyActionsRestored == false && heroesTurn == true)
        {
            //restore enemy actions 
            heroesTurn = false;      
            enemyActionsRestored = true;
            herosNotMoved = noOfHeroes;
            enemiesDoneMoving = false;
            
            Debug.Log("Heros turn over");
        }

        if (herosNotMoved < 1)
        {
            herosDoneMoving = true;
        }

        if(enemiesNotMoved < 1)
        {
            enemiesDoneMoving = true;
        }



        if (enemiesDoneMoving == true && heroActionsRestored == false && heroesTurn == false)
        {           
            heroesTurn = true;            
            heroActionsRestored = true;
            herosNotMoved = noOfHeroes;
            herosDoneMoving = false;
            
            Debug.Log("Overlords Turn over");
        }


        
    }

   
}