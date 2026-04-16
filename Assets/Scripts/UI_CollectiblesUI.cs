using UnityEngine;
using UnityEngine.UI;

public class UI_CollectibleUI : MonoBehaviour
{
    public static UI_CollectibleUI Instance;
    public Text scoreText;

    void Awake() 
    { 
        Instance = this;
    }

    void Start() 
    { 
        UpdateScore(CollectibleManager.Instance ? CollectibleManager.Instance.totalScore : 0); 
    }

    public void UpdateScore(int newScore)
    {
        if (scoreText) scoreText.text = "Score: " + newScore;
    }
}
