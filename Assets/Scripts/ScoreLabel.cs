using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreLabel : MonoBehaviour
{
    private int score = 0;
    private Text textComponent;
    // Start is called before the first frame update
    void Start()
    {
        textComponent = GetComponent<Text>();
        textComponent.text = score.ToString();
    }
    public void UpdateOnEnemyDestroyed(int enemyScore)
    {
        score += enemyScore;
        textComponent.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
