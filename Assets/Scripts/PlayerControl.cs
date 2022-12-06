using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Profiling;

public class PlayerControl : MonoBehaviour
{

    public GameObject playerModel;
    public GameObject playerModel_UI;

    private struct PlayerData
    {
        public float health;
        public GameObject player;
        public Vector2 direction;
        public PlayerData(GameObject player)
        {
            this.player = player;
            this.direction = new Vector2(0, 0);
            this.health = 1;
        }
    }

    private List<PlayerData> players = new List<PlayerData>();

    private const int PlayerCount = 100;

    private int frameCount = 0;
    private float timeCount = 0;

    // Start is called before the first frame update

    private void setHealth(GameObject player, float health)
    {
        Transform hp = player.transform.Find("HP");
        if (hp != null)
        {
            Profiler.BeginSample("SetHealth_Shader");
            hp.gameObject.GetComponent<MeshRenderer>().material.SetFloat("_Health", health);
            Profiler.EndSample();
        }
        else
        {
            hp = player.transform.Find("Canvas").Find("HP");
            Profiler.BeginSample("SetHealth_UI");
            hp.gameObject.GetComponent<Image>().fillAmount = health;
            Profiler.EndSample();
        }
    }

    void Start()
    {
        for (int i = 0; i < PlayerCount; ++i)
        {
            GameObject newPlayer = Instantiate(i < PlayerCount / 2? playerModel : playerModel_UI);

            Vector3 pos = newPlayer.transform.position;
            pos.x = Random.Range((float)-100, 100);
            pos.z = Random.Range((float)-50, 150);
            newPlayer.transform.position = pos;

            setHealth(newPlayer, Random.Range((float)0, 1));

            PlayerData playerData = new PlayerData(newPlayer);
            players.Add(playerData);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeCount += Time.deltaTime * 1000;
        while (frameCount < timeCount)
        {
            for (int i = 0; i < PlayerCount; ++i)
            {
                PlayerData data = players[i];
                
                if (frameCount % 1000 == 0) // generate new direction
                {
                    float r = Random.Range(0, 2 * Mathf.PI);
                    data.direction = new Vector2(Mathf.Cos(r), Mathf.Sin(r));
                }

                Vector3 pos = data.player.transform.position;
                pos.x = Mathf.Min(Mathf.Max((float)-100, pos.x + data.direction.x * (float)0.1), (float)100);
                pos.z = Mathf.Min(Mathf.Max((float)-50, pos.z + data.direction.y * (float)0.1), (float)150);
                if (pos.x == -100 || pos.x == 100 || pos.z == -50 || pos.z == 150)
                    data.direction = -data.direction;


                Profiler.BeginSample(i < PlayerCount /2 ? "SetPos_Shader" : "SetPos_UI");
                data.player.transform.position = pos;
                Profiler.EndSample();

                data.health = Mathf.Min(Mathf.Max((float)0, data.health + (float)0.0005 * (pos.x < 0 ? 1: -1)),(float)1);
                setHealth(data.player, data.health);

                players[i] = data;
            }
            ++frameCount;
        }
    }
}
