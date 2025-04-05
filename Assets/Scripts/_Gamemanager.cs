using UnityEngine;

public class _Gamemanager : MonoBehaviour
{
    public static _Gamemanager Instance;
    // ASTEROID HP
    public int maxSmallAsteroidHealth = 2;
    public int maxBigAsteroidHealth = 6;
    public int maxCrystalAsteroidHealth = 5;
    // ASTEROID HP
    
    //BACKGROUNDSELECTER
    public Sprite selectedBackground;
    //BACKGROUNDSELECTER
    

    // SCORE
    public int score = 0;

    void Start()
    {

    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void AddScore(int amount) 
    {
        score += amount;
        Debug.Log("Score: " + score);
    }
}
