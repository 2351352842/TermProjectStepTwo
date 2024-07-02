using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using UnityEngine.UI;
namespace Invector.vCharacterController
{
    public class PlayerInitialization : MonoBehaviour
    {
        public static PlayerInitialization instance;
        private void Awake()
        {
            instance=this;
        }
        Image ima;
        string path;
        public Player player = new Player();
        //Rigidbody rb;
        GameObject rb;
        int level;
        // Start is called before the first frame update
        void Start()
        {
            path = Application.dataPath + "/Resources/player.json";
            string s = File.ReadAllText(path);
            ima = GetComponent<Image>();
            ima.sprite = Resources.Load("images/" + Random.Range(0, 7), typeof(Sprite)) as Sprite;
            player = JsonUtility.FromJson<Player>(s);
            transform.GetChild(0).GetComponent<Text>().text = player.name + "  Level:" + level;
            rb = GameObject.Find("Player");//.GetComponent<Rigidbody>();
            rb.transform.position = player.pos + new Vector3(0, 1, 0);
            rb.transform.eulerAngles = player.rot;
            level=player.level;
        }

        // Update is called once per frame
        void Update()
        {
            player.pos = rb.transform.position;
            player.rot = rb.transform.eulerAngles;
            var s = JsonUtility.ToJson(player);
            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                level++;
                player.level = level;
                player.speed = float.Parse(CSV.instance.speeds[level+1]);
                transform.GetChild(0).GetComponent<Text>().text = player.name +"  Level:"+level;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                level--;
                player.level = level;
                player.speed = float.Parse(CSV.instance.speeds[level + 1]);
                transform.GetChild(0).GetComponent<Text>().text = player.name + "  Level:" + level;

            }
            File.WriteAllText(path, s);
        }
    }
}
