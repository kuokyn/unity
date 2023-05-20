using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Accessibility;

public class WanderingAi : MonoBehaviour
{

    public float speed; // ������� �������� ����

	public float speedMin = 2.0f; // �������� ���� ��� ������� ��������

	public float speedMax = 7.0f; // �������� ����, ����� ��� ����������

	public float dist = 5.0f; // ��������� ������

	public GameObject player; // �� ���� �������

	public Animator anim; // �������� ��������

	Boolean TimerSet = false; // ������� ������

	float timer = 0f; // ������� ������

	public int action; // ��� ������ ���� (1-�����, 2-��������, 3-�����)

	Boolean IsScared = false; // �������� �� ����

	public GameObject Vision; // ������� ��������� ����

	Vector3 run; // ���� ������� ����

	// Start is called before the first frame update
	void Start()
    {
        speed = speedMin;
    }

    // Update is called once per frame
    void Update()
    {
        // ���������� ���� speed ��� ��������
        anim.SetFloat("walkspeed", speed);

        // ������ ������� ������� ��� ����
        if (TimerSet == false)
        {
            timer = UnityEngine.Random.Range(3, 10); // ����� ������� �� ��������� ����� �� 3 �� 9 ������
            action = UnityEngine.Random.Range(1, 3); // ����� ���������� �������� 1 - ��������, 2 - �����
            TimerSet = true;
            speed = speedMin;
            IsScared = false;
        }
        else
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0)
        {
            TimerSet = false;
        }

        // ������ ������ ������
        if (action == 1)
        {
            anim.ResetTrigger("Idle");
            anim.SetTrigger("Walk");
            transform.Translate(0, 0, speed * Time.deltaTime);

            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.SphereCast(ray, 0.75f, out hit)){
                if (hit.distance < dist)
                {
                    float angle = UnityEngine.Random.Range(-110, 110);
                    transform.Rotate(0, angle, 0);
                }
            }
        }

        // ������ �����
        if (action == 2)
        {
			anim.ResetTrigger("Walk");
			anim.SetTrigger("Idle");
		}

        // ������ ����������
		if (action == 3)
		{
			anim.ResetTrigger("Idle");
			anim.SetTrigger("Walk");
            if (!IsScared)
            {
                timer = 2;
                IsScared = true;
            }
            speed = speedMax;
            run = Vision.GetComponent<Duck_vision_script>().RunVector;
            run.y = 0; // ����� ���� �� ������� ������
            run = run / run.magnitude; // ����� ������ �� ��� �� �����
            transform.Rotate(Vector3.up * Vector3.Angle(transform.forward, run)); // ������������ ����
            transform.Translate(0, 0, speed * Time.deltaTime);

			// ������ �� ��������� ���������
			Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
			if (Physics.SphereCast(ray, 0.75f, out hit)){
				if (hit.distance < dist)
				{
					float angle = UnityEngine.Random.Range(-110, 110);
					transform.Rotate(0, angle, 0);
				}
			}
		}
	}
}
