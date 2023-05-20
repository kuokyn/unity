using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duck_vision_script : MonoBehaviour
{

    public GameObject Duck; // объект на сцене

    private WanderingAi NPC; // объект NPC

	GameObject SpottedPlayer; // от кого убегать

    public Vector3 RunVector; // направление движения

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Если игрок вошел в зону видимости утки
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player"){
            SpottedPlayer = other.gameObject;
            NPC = Duck.GetComponent<WanderingAi>();

            // утка испугалась
            NPC.action = 3;
            RunVector = transform.position - SpottedPlayer.transform.position; // определение вектора направления убегающей утки
        }
    }

    // Если игрок вышел из зоны видимости утки
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            SpottedPlayer = null;

        }
    }
}
