using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject hologram1;
    public GameObject hologram2;
    public GameObject hologram3;
    public GameObject hologram4;
    private GamePanel hologram1Panel;
    private GamePanel hologram2Panel;
    private GamePanel hologram3Panel;
    private GamePanel hologram4Panel;

    public GameObject fireBallHologram;
    private CorePanel fireBallPanel;

    public GameObject text;
    public float interval = 10;

    void Awake () {
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 30;
    }

    void Start()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        hologram1Panel = hologram1.GetComponent<GamePanel>();
        hologram2Panel = hologram2.GetComponent<GamePanel>();
        hologram3Panel = hologram3.GetComponent<GamePanel>();
        hologram4Panel = hologram4.GetComponent<GamePanel>();

        fireBallPanel = fireBallHologram.GetComponent<CorePanel>();

        hologram1Panel.setAutoPilot(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup > interval)
        {
            hologram2Panel.setAutoPilot(false);
        }
        if (Time.realtimeSinceStartup > interval * 2)
        {
            hologram3Panel.setAutoPilot(false);
        }
        if (Time.realtimeSinceStartup > interval * 3)
        {
            hologram4Panel.setAutoPilot(false);
        }

        float stability = (
            hologram1Panel.getStability() +
            hologram2Panel.getStability() +
            hologram3Panel.getStability() +
            hologram4Panel.getStability()) / 4;
        
        fireBallPanel.stability = stability;

        string textBlock = "";

        if (stability < .1)
        {
            textBlock = "░░░░░░░░░░";
        }
        else if (stability < .2) {
            textBlock = "█░░░░░░░░░";
        }
        else if (stability < .3) {
            textBlock = "██░░░░░░░░";
        }
        else if (stability < .4) {
            textBlock = "███░░░░░░░";
        }
        else if (stability < .5) {
            textBlock = "████░░░░░░";
        }
        else if (stability < .6) {
            textBlock = "█████░░░░░";
        }
        else if (stability < .7) {
            textBlock = "██████░░░░";
        }
        else if (stability < .8) {
            textBlock = "███████░░░";
        }
        else if (stability < .9) {
            textBlock = "████████░░";
        }
        else if (stability < 1) {
            textBlock = "█████████░";
        }
        else {
            textBlock = "██████████";
        }

        text.GetComponent<TextMesh>().text = Mathf.RoundToInt(stability*100) + "% stable\n" +textBlock;

    }
}
