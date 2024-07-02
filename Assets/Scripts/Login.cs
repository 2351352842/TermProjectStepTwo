using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    InputField name;
    InputField id;
    Button loginButton;
    Player player = new Player();
    string path;
    // Start is called before the first frame update
    void Start()
    {
        name = GetComponent<InputField>();
        id = GameObject.Find("id").GetComponent<InputField>();
        loginButton = GameObject.Find("进入场景").GetComponent<Button>();
        loginButton.onClick.AddListener(login);
        path = Application.dataPath + "/Resources/player.json";

       
    }

    // Update is called once per frame
    public void login()
    {
        player.id = int.Parse(id.text);
        player.name=name.text;
        player.pos = new Vector3((float)-31.80859375, (float)0.0034502148628234865, (float)25.05238914489746);
        player.rot = new Vector3((float)0.0, (float)0.0, (float)0.0);
        player.level = 0;
        player.power = 0;
        player.speed = 1.13f;
        var s = JsonUtility.ToJson(player);
        File.WriteAllText(path, s);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    void Update()
    {

    }
}
