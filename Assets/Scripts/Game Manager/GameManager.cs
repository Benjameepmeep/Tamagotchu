using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager gameManagerInstance;

    // Pet Information
    public string petName;
    public int petAge;

    // Levels for the pet
    [Header("Tomogachi Stats")]

    public HealthLevels healthLevel;
    public int health;
    public enum HealthLevels
    { Healthy, Fine, Ill, DeathlySick, Dead }

    [Space(10)]
    public MoodLevels moodLevel;
    public int happiness;
    public enum MoodLevels
    { Happy, Fine, Neutral, Bored, Sad, Angry }

    [Space(10)]
    public HungerLevels hungerLevel;
    public int hunger;
    public enum HungerLevels
    { Stuffed, Satisfied, Hungry, Starving, Famished }

    [Space(20)]
    [SerializeField] private int petDeathCountdown;
    public bool pauseCountdown;

        // Create an instance of the GameManager that is static between scenes
    void Awake()
    {
        if (gameManagerInstance != null)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        gameManagerInstance = this;
        return;
    }

    void Start()
    {

        return;
    }

    
    void Update()
    {

        return;
    }

    private void UpdateLevels()
    {

        return;
    }
}
