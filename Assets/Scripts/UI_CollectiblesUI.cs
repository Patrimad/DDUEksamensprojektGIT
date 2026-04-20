using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_CollectibleUI : MonoBehaviour
{
    public static UI_CollectibleUI Instance;
    [SerializeField] public TextMeshProUGUI scoreText;

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
