using UnityEngine;
using Mono.Data.SqliteClient;
using System.Collections;
using System.Data;

public class Enemies : MonoBehaviour
{

    int maxHealth;
    int health;
    //int maxSpeed;
    public int speed;
    public int statID;
    bool isPoisoned;
    bool isStunned;
    bool isConscious;


    //I use playersPlaying as a way to make sure that there is never more than one instance of the player class playing at any given time
    public static int playersPlaying;

    //actionNotAllowd makes it so the actions menu does not SetActive(true) when we are targeting another character
    public bool actionNotAllowd;

    //preformingAction = true when we select an action on the action menu, once the action is completed ispreformingAction = false
    public bool isPreformingAction;

    //When using the move action I found that if you accidently click on another character you get stuck in a loop, in the MouseManager script we make it so we cannot select another character when clicking on a tile to move to
    public static bool aPlayerIsMoving;

    //IsPlaying should = true whenever it's one, not the herosTurn, two, the gameobject is the selectedObject in MouseManager, three, playersPlaying == 0 and four, when noOfActions > 0
    public bool isPlaying = false;

    public static bool isYourTurn;

    int iD;

    public GameObject actionButtons;
    public GameObject mouseManager;
    public GameObject moveButton;




    int oldNoOfActions;
    public int noOfActions = 2;
    bool isOutOfActions;

    static Enemies _instance;
    string _databaseName = "/Character_Stats.db";
    private IDbConnection _databaseConnection;

    public IDbConnection CreateTable()
    {
        if (_databaseConnection != null && _databaseConnection.State == ConnectionState.Open)
        {
            return _databaseConnection;
        }
        else
        {
            string conn = "URI=file:" + Application.dataPath + _databaseName;

            _databaseConnection = (IDbConnection)new SqliteConnection(conn);
            _databaseConnection.Open();
            Debug.Log("Database created");
            return _databaseConnection;
        }
    }

    public static Enemies Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        noOfActions = 2;
        oldNoOfActions = noOfActions;

        _instance = this;
        CreateTable();
        setEnemyStats();
        maxHealth = health;


        moveButton = GameObject.Find("Enemy_Move_Button");       

        mouseManager = GameObject.Find("Mouse Manager");

        //moveButton = GameObject.Find("Enemy Move Button");

        actionButtons = GameObject.Find("TurnActions");

        moveButton.SetActive(false);

        actionNotAllowd = true;

        playersPlaying = 0;
        aPlayerIsMoving = false;

    }


    void Update()
    {
        if (mouseManager.GetComponent<MouseManager>().selectedObject == gameObject && noOfActions > 0)
        {
            if (playersPlaying == 0 || isPreformingAction)
            {
                actionNotAllowd = false;
                isPlaying = true;
                Debug.Log("SPIDER PLAYING");
                playersPlaying = 1;
            }

        }
        else
        {
            isPlaying = false;
            Debug.Log("SPIDER NOT PLAYING");
            playersPlaying = 0;
        }

        /*if (health <= 0)
        {
            gameObject.GetComponent<Enemies>().isConscious = false;
        }
        else
        {
            gameObject.GetComponent<Enemies>().isConscious = true;
        }*/

        if (!TurnsManager.heroesTurn)
        {
            //Debug.Log("game start");
            isYourTurn = true;
        }
        else
        {
            //Debug.Log("HerosTurn");
            isYourTurn = false;
            noOfActions = 2;
        }

        if (oldNoOfActions != noOfActions)
        {
            Debug.Log("Action used");
            moveButton.SetActive(false);
            isPreformingAction = false;
            oldNoOfActions = noOfActions;
        }

        if (isPlaying && !isPreformingAction && isYourTurn)
        {
            //Debug.Log("BUTTONS ON");
            actionButtons.SetActive(true);
        }

        GameObject currentGameObject = mouseManager.GetComponent<MouseManager>().selectedObject;
        //int targetsNoOfActions = currentGameObject.GetComponent<Player>.noOfActions = 0;
        if (gameObject == currentGameObject && gameObject.GetComponent<Enemies>().noOfActions == 0)
        {
            actionButtons.SetActive(false);
        }

        if (noOfActions <= 0 && !isOutOfActions)
        {
            TurnsManager.enemiesNotMoved -= 1;
            playersPlaying = 0;
            Debug.Log("enemies not moved =" + TurnsManager.enemiesNotMoved);
            actionNotAllowd = true;
            isOutOfActions = true;
            playersPlaying = 0;
            aPlayerIsMoving = false;
        }




    }

    public void setEnemyStats()
    {
        IDbConnection connection = Enemies.Instance.CreateTable();
        IDbCommand command = connection.CreateCommand();

        string sqlQuery = "SELECT Health, Speed FROM Enemy_Stats WHERE ID = (" + statID + ")";
        command.CommandText = sqlQuery;
        IDataReader reader = command.ExecuteReader();
        command.Dispose();
        command = null;

        while (reader.Read())
        {
            //Need to specifically grab the correct data type at each index.         
            maxHealth = reader.GetInt32(0);
            speed = reader.GetInt32(1);
            Debug.Log("name = " + name + " health = " + maxHealth + " speed = " + speed);
        }

        reader.Close();
        reader = null;
    }


    // DOES NOT WORK!!!
    public void MoveEnemy()
    {
        //Debug.Log("SELECTED");
        //GameObject enemyCurrentGameObject = mouseManager.GetComponent<MouseManager>().selectedObject;
        //Unit unit = FindObjectOfType<Unit>();
        //unit.currentPath = null;
        Debug.Log("button pressed");
        if (isPlaying == true)
        {
            Debug.Log("SELECTED");
            aPlayerIsMoving = true;

            isPreformingAction = true;

            Debug.Log(gameObject + "is performing action");
            actionNotAllowd = false;
        }
        Debug.Log(moveButton);
        moveButton.SetActive(true);
        Debug.Log("Button");
        actionButtons.gameObject.SetActive(false);
    }

    void Attack()
    {

    }

    void Interate_w_door()
    {

    }

    public void Rest()
    {
        if (isPlaying)
        {
            //fatigue = 0;
            noOfActions -= 2;
        }

    }
}
