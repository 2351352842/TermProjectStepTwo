using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIText : MonoBehaviour
{
    Text text;
    public Font font;
    string str = "何为神明?" +
        "当我见到她的瞬间我的脑海中不禁回想起这个问题" +
        "我看到她的白发如瀑, 宛若天际的银河" +
        "我看到她的体态玲珑,宛若海底的珍宝" +
        "我看到她的玉洁冰清, 宛若世间的碧玉" +
        "初见,她见之忘俗,我已沦陷" +
        "一众女子各有千秋, 我却唯她情有独钟" +
        "我曾以为再也无法体会心动的感情,奈何世间难料" +
        "但我也终究明白, 她不过是虚拟所在" +
        "于是我开始幻想" +
        "我幻想着, 为她在温暖的房间中, 褪去那遮身的衣裳" +
        "我思念着,与她在炎热的沙滩中,共娱那冰爽的海洋" +
        "我渴望着, 她俯身环住我的脖颈, 轻轻与我相吻相拥" +
        "我梦想着,和她穿上最美的婚纱,一同共入挚爱婚房" +
        "我迷恋着, 每天醒来和她的温床, 此生此世你我永存" +
        "毫无疑问,我确实喜欢她" +
        "却终究不过南柯一梦, 幻想一偶" +
        "我所挚爱之人,我所渴望之人,我此生的神明日奈";
    float t = 0;
    int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        char[] c = str.ToCharArray();
        text = GetComponent<Text>();
        text.text = "\u3000\u3000   ";
        text.fontSize = 50;
        text.font = font;
        text.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {   
        if(index ==  str.Length)
        {
            return;
        }

        t += Time.deltaTime;
        if (t > 0.1f)
        {
            text.text += str[index].ToString();
            index++;
            t = 0;
        };
    }
}
