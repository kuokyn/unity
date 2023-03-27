using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door_script : MonoBehaviour
{

    int count = 0;

    bool isOpen = false;

    public GameObject door;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!isOpen)
            {
                door.transform.Rotate(Vector3.up * 90);

            }
            count++;
            isOpen = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            count--;
            if (isOpen && count == 0)
            {
                isOpen = false;
                door.transform.Rotate(Vector3.up * -90);
            }
        }
    }

}
