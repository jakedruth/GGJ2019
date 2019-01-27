using UnityEngine;

public class worldHubManager : MonoBehaviour
{

    public quest firstQuest;
    public quest secondQuest;
    public quest thirdQuest;

    public int costMultiplyer = 1;


    public int money = 0;

    private int maxUpgrade = 10;
    public int currentHouseLevel = 0;
    public int currentHouseExp = 0;

    public int currentLenghtLevel = 0;
    public int currentLenghtExp = 0;

    public int currentStranghtLevel = 0;
    public int currentStrangthExp = 0;

    public int currentHealthLevel = 0;
    public int currentHealthExp = 0;

    public int currentSheildLevel = 0;
    public int currentSheildExp = 0;


    private int currentCost = 0;

    public static worldHubManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            GameObject.Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);


        firstQuest.current_room = 0;
        secondQuest.current_room = 0;
        thirdQuest.current_room = 0;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void IncreaseHealth()
    {
        currentCost = (currentHealthLevel + 1) * costMultiplyer;
        if (currentHealthLevel != maxUpgrade && money > 0)
        {
            money--;
            currentHealthExp++;
            if (currentHealthExp == currentCost)
            {
                currentHealthLevel++;
                currentHealthExp = 0;
            }
        }



    }

    public void IncreaseHouse()
    {
        currentCost = (currentHouseLevel + 1) * costMultiplyer;
        if (currentHouseLevel != maxUpgrade && money > 0)
        {
            money--;
            currentHouseExp++;
            if (currentHouseExp == currentCost)
            {
                currentHouseLevel++;
                currentHouseExp = 0;
            }
        }

    }

    public void IncreaseLenght()
    {
        currentCost = (currentLenghtLevel + 1) * costMultiplyer;
        if (currentLenghtLevel != maxUpgrade && money > 0)
        {
            money--;
            currentLenghtExp++;
            if (currentLenghtExp == currentCost)
            {
                currentLenghtLevel++;
                currentLenghtExp = 0;
            }
        }

    }

    public void IncreaseSheild()
    {
        currentCost = (currentSheildLevel + 1) * costMultiplyer;
        if (currentSheildLevel != maxUpgrade && money > 0)
        {
            money--;
            currentSheildExp++;
            if (currentSheildExp == currentCost)
            {
                currentSheildLevel++;
                currentSheildExp = 0;
            }
        }


    }

    public void IncreaseStrenght()
    {
        currentCost = (currentStranghtLevel + 1) * costMultiplyer;
        if (currentStranghtLevel != maxUpgrade && money > 0)
        {
            money--;
            currentStrangthExp++;
            if (currentStrangthExp == currentCost)
            {
                currentStranghtLevel++;
                currentStrangthExp = 0;
            }
        }


    }

    public void LoadQuestRoom(int questNum)
    {

        Debug.Log("loadroom");
        if (questNum == 1)
        {
            firstQuest.loadRoom();
        }
        else if (questNum == 2)
        {
            secondQuest.loadRoom();
        }
        else if (questNum == 3)
        {
            thirdQuest.loadRoom();
        }
    }

    public void CheckQuestReward(int questNum)
    {

        Debug.Log("checking reward");
        if (questNum == 1)
        {
            firstQuest.checkReward();
        }
        else if (questNum == 2)
        {
            secondQuest.checkReward();
        }
        else if (questNum == 3)
        {
            thirdQuest.checkReward();
        }
    }
}
