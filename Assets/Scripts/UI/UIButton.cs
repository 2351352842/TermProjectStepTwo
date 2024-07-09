using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(GetClick);
    }
    public void GetClick()
    {

        print("Button点击了我");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
