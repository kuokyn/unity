using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duck_vision_script : MonoBehaviour
{

    public GameObject Duck; // ������ �� �����

    private WanderingAi NPC; // ������ NPC

	GameObject SpottedPlayer; // �� ���� �������

    public Vector3 RunVector; // ����������� ��������

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ���� ����� ����� � ���� ��������� ����
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player"){
            SpottedPlayer = other.gameObject;
            NPC = Duck.GetComponent<WanderingAi>();

            // ���� ����������
            NPC.action = 3;
            RunVector = transform.position - SpottedPlayer.transform.position; // ����������� ������� ����������� ��������� ����
        }
    }

    // ���� ����� ����� �� ���� ��������� ����
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            SpottedPlayer = null;

        }
    }
}
