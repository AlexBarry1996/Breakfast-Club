using UnityEngine;
using UnityEngine.UI;
using Mono.Data.SqliteClient;
using System.Collections;
using System.Data;
//when noOfActions == 0, Turnmanager.herosNotMoved += 1, once that equals 4, all  heros are done, enemies turn
//check to see if isPlaying = true in Unit, so that only one hero moves at one time
public class Player : MonoBehaviour
{

    //STATS
    //=====================
    int maxHealth;
    public int health;
    public int speed;
    int stamina;
    int fatigue;
    int might;
    int knowledge;
    int willpower;
    int awareness;
    //========================

    //I use playersPlaying as a way to make sure that there is never more than one instance of the player class playing at any given time
    public static int playersPlaying;

    //actionNotAllowd makes it so the actions menu does not SetActive(true) when we are targeting another character
    public bool actionNotAllowd;

    //preformingAction = true when we select an action on the action menu, once the action is completed ispreformingAction = false
    public bool isPreformingAction;

    //When using the move action I found that if you accidently click on another character you get stuck in a loop, in the MouseManager script we make it so we cannot select another character when clicking on a tile to move to
    public static bool aPlayerIsMoving;

    public static bool isYourTurn;
    

    bool isConscious;
    bool isPoisoned;
    bool isStunned;
    bool isDiseased;
    //IsPlaying should = true whenever it's one, the herosTurn, two, the gameobject is the selectedObject in MouseManager, three, playersPlaying == 0 and four, when noOfActions > 0
    public bool isPlaying = false;

    int iD;

    public GameObject actionButtons;
    public GameObject mouseManager;
    public GameObject moveButton;



    
    int oldNoOfActions;
    public int noOfActions;
    bool isOutOfActions;

    // DATABASE CONNECTION======================================================================
    static Player _instance;
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
    //===========================================================================================

    public static Player Instance
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

        mouseManager = GameObject.Find("Mouse Manager");

        moveButton = GameObject.Find("Move Button");

        actionButtons = GameObject.Find("TurnActions");

        setPlayerStats();

        actionNotAllowd = true;

        health = maxHealth;

        playersPlaying = 0;
        aPlayerIsMoving = false;
    }
    void Start()
    {
        setPlayerStats();
        //StartOfHeroTurn();
        moveButton.SetActive(false);
        actionButtons.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(playersPlaying);

        if (mouseManager.GetComponent<MouseManager>().selectedObject == gameObject && noOfActions > 0)
        {                
            if(playersPlaying == 0 || isPreformingAction)
            {
                actionNotAllowd = false;
                isPlaying = true;
                playersPlaying = 1;
            }
            
        }
        else
        {
            isPlaying = false;
            playersPlaying = 0;
        }

        if (health <= 0)
        {
            gameObject.GetComponent<Player>().isConscious = false;
        }
        else
        {
            gameObject.GetComponent<Player>().isConscious = true;
        }

        if (TurnsManager.heroesTurn)
        {
            isYourTurn = true;
        }
        else
        {
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
        if (gameObject == currentGameObject && gameObject.GetComponent<Player>().noOfActions == 0)
        {
            actionButtons.SetActive(false);
        }

        if (noOfActions <= 0 && !isOutOfActions)
        {
            TurnsManager.herosNotMoved -= 1;
            playersPlaying = 0;
            Debug.Log("heros not moved =" + TurnsManager.herosNotMoved);
            actionNotAllowd = true;
            isOutOfActions = true;
            playersPlaying = 0;
            aPlayerIsMoving = false;
        }

        

    }







    public void setPlayerStats()
    {
        IDbConnection connection = Player.Instance.CreateTable();
        IDbCommand command = connection.CreateCommand();

        if (gameObject.name == "Averic")
        {
            iD = 1;
        }
        if (gameObject.name == "Leoric")
        {
            iD = 2;

        }
        if (gameObject.name == "Jain")
        {
            iD = 3;

        }
        if (gameObject.name == "Grisban")
        {
            iD = 4;
        }

        string sqlQuery = "SELECT Health, Speed, Stamina, Might, Knowledge, Willpower, Awareness FROM Hero_Stats WHERE Hero_ID = (" + iD + ")";
        command.CommandText = sqlQuery;
        IDataReader reader = command.ExecuteReader();
        command.Dispose();
        command = null;

        while (reader.Read())
        {
            //Need to specifically grab the correct data type at each index.         
            maxHealth = reader.GetInt32(0);
            speed = reader.GetInt32(1);
            stamina = reader.GetInt32(2);
            might = reader.GetInt32(3);
            knowledge = reader.GetInt32(4);
            willpower = reader.GetInt32(5);
            awareness = reader.GetInt32(6);
            //Debug.Log("name = " + name + " ID = " + iD + " health = " + maxHealth + " stamina = " + stamina + " speed = " + speed + " might = " + might + " knowledge = " + knowledge + " willpower = " + willpower + " awarness = " + awareness);
        }

        reader.Close();
        reader = null;
    }

    //Each player will have an invisible trigger surrounding it in all the adjacent tiles
    void SelectItem()
    {

    }


    public void Move()
    {
        if (isConscious)
        {
            GameObject currentGameObject = mouseManager.GetComponent<MouseManager>().selectedObject;
            //Unit unit = FindObjectOfType<Unit>();
            //unit.currentPath = null;           
            if (currentGameObject == gameObject)
            {
                Debug.Log(gameObject + "SELECTED");
                aPlayerIsMoving = true;
                gameObject.GetComponent<Player>().isPreformingAction = true;
                Debug.Log(gameObject + "is performing action");
                gameObject.GetComponent<Player>().actionNotAllowd = false; 
                
            }
            moveButton.SetActive(true);
            actionButtons.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("PLAYER IS UNCONSCIOUS");
        }
    }

    void Attack()
    {
        Debug.Log("Attack chosen");
        var beginRoll = GameObject.FindWithTag("Dice");

        //ForceInRandomDirection.rollDice = true;
    }

    void Skills()
    {

    }

    public void Rest()
    {
        if (isPlaying)
        {
            fatigue = 0;
            noOfActions -= 2;
        }
        
    }

    void Search()
    {

    }

    void Stand_Up()
    {

    }

    void Revive()
    {
        //
    }

    void Interacte_w_door()
    {

    }

    void Special()
    {

    }



}
