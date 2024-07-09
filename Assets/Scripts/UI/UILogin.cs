using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILogin : MonoBehaviour
{
    public InputField userName;
    public InputField passWord;
    public InputField r_userName;
    public InputField r_passWord;
    public InputField cr_passWord;
    public GameObject registerPanel;
    public GameObject settingPanel;
    string name;
    string pass;
    public Slider audioSlider;
    public Toggle audioToggle;
    public Text showMusicName;
    public AudioSource au;
    public List<AudioClip> audioClips = new List<AudioClip>();
    int index = 0;
    public static int leave = 1;
    // Start is called before the first frame update
    void Start()
    {
        au = au.GetComponent<AudioSource>();
        showMusicName.text = "正在播放:" + au.clip.name;
    }
    public void UpMusic()
    {
        index--;
        if (index < 0)
        {
            index = audioClips.Count - 1;
        }
        au.clip=audioClips[index];
        au.Play();
        showMusicName.text = "正在播放:" + au.clip.name;

    }
    public void NextMusic()
    {
        index++;
        if (index > audioClips.Count - 1)
        {
            index = 0;
        }
        au.clip = audioClips[index];
        au.Play();
        showMusicName.text = "正在播放:" + au.clip.name;

    }
    public void OpenRegisterPanel()
    {
        registerPanel.SetActive(true);
    }
    public void CloseRegisterPanel()
    {
        registerPanel.SetActive(false);
        r_userName.text = "";
        r_passWord.text = "";
        cr_passWord.text = "";
    }
    public void ConfirmRegister()
    {
        name = r_userName.text;
        pass = r_passWord.text;
        if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(pass) && r_passWord.text == cr_passWord.text)
        {
            print("注册成功");
            registerPanel.SetActive(false);
        }
        else
        {
            name = "";
            pass = "";
        }
        r_userName.text = "";
        r_passWord.text = "";
        cr_passWord.text = "";

    }
    public void Login()
    {
        if (userName.text == name && passWord.text == pass && !string.IsNullOrEmpty(name))
        {
            print("登录成功");
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
        else
        {
            print("登录失败");
            userName.text = "";
            passWord.text = "";
        }
    }
    public void OpenSettingPanel()
    {
        settingPanel.SetActive(true);
    }
    public void CloseSettingPanel()
    {
        settingPanel.SetActive(false);
        print(leave);
    }
    public void EasyT(bool isSelect)
    {
        if(isSelect)
        {
            leave = 1;
        }
    }
    public void NomalT(bool isSelect)
    {
        if (isSelect)
        {
            leave = 2;
        }
    }
    public void HardT(bool isSelect)
    {
        if (isSelect)
        {
            leave = 3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        au.mute = audioToggle.isOn;
        au.volume = audioSlider.value;
    }
}
