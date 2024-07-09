using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour
{
    Text text;
    public static int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "得分：" + score;
    }
    public void ReStart()
    {
        score = 0;
        CreateEnemy.isBoss = false;
        BossHealth.num = 1;

        UnityEngine.SceneManagement.SceneManager.LoadScene(2);

    }
    public void ReMain()
    {
        score = 0;
        CreateEnemy.isBoss = false;
        BossHealth.num = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
