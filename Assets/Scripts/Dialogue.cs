using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public Text MeText;
    public Text NiggerText;
    int index = 0;
    int myLineIndex = -1;
    int niggerLineIndex = -1;
    List<string> myLine = new List<string>();
    List<string> niggerLine = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        
        myLine.Add("Hello");
        niggerLine.Add("Hi");
        myLine.Add("Nice to meet you");
        niggerLine.Add("Nice to meet you,too");
        myLine.Add("How are you");
        niggerLine.Add("I\'m fine ,thank,and you");
        myLine.Add("I\'m fine");
        niggerLine.Add("각것것것1");
        myLine.Add("각것것것2");
        niggerLine.Add("각것각것3");
        myLine.Add("각것각것4");
        niggerLine.Add("각것것것5");
        myLine.Add("각것것것6");
        niggerLine.Add("각것각것7");
        myLine.Add("댔령샙!!!");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.End))
        {
            index++;
            if (index % 2 == 1)
            {
                myLineIndex++;
            }
            if (index % 2 == 0)
            {
                niggerLineIndex++;
            }
        }
        if (myLineIndex >= 0)
        {
            MeText.text = myLine[myLineIndex];
        }
        if (niggerLineIndex >= 0&&niggerLineIndex< niggerLine.Count)
        {
            NiggerText.text = niggerLine[niggerLineIndex];
        }
        if (niggerLineIndex == niggerLine.Count)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        }
    }
}
