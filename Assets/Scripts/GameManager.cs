using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject hologram1;
    public GameObject hologram2;
    public GameObject hologram3;
    public GameObject hologram4;
    private GamePanel hologram1Panel;
    private GamePanel hologram2Panel;
    private GamePanel hologram3Panel;
    private GamePanel hologram4Panel;


    public float interval = 10;

    void Start()
    {
        hologram1Panel = hologram1.GetComponent<GamePanel>();
        hologram2Panel = hologram2.GetComponent<GamePanel>();
        hologram3Panel = hologram3.GetComponent<GamePanel>();
        hologram4Panel = hologram4.GetComponent<GamePanel>();

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
    }
}
