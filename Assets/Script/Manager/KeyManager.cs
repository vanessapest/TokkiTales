using UnityEngine;
using UnityEngine.SceneManagement;

// für die Keys zuständig
public class KeyManager : MonoBehaviour
{
    public static KeyManager Instance { get; private set; } // muss Singleton sein

    public int totalKeysInLevel = 2; 

    private int collectedKeys = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        // NICHT DontDestroyOnLoad –> Key Manager gehört nur zum Level
        // braucht man nicht in jeder Szene
    }

    private void Start()
    {
        collectedKeys = 0;
    }

    public void CollectKey()
    {
        collectedKeys++;
    }

    // geben die Werte nach außen weiter -> LevelGoal.cs
    public int GetCollectedKeys() => collectedKeys;
    public int GetTotalKeys() => totalKeysInLevel;
}

