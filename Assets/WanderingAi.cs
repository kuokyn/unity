using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Accessibility;

public class WanderingAi : MonoBehaviour
{

    public float speed; // текущая скорость утки

	public float speedMin = 2.0f; // скорость утки при обычном движении

	public float speedMax = 7.0f; // скорость утки, когда она испугалась

	public float dist = 5.0f; // дистанция отхода

	public GameObject player; // от кого убегать

	public Animator anim; // анимация убегания

	Boolean TimerSet = false; // текущий таймер

	float timer = 0f; // текущий таймер

	public int action; // что делает утка (1-покой, 2-движение, 3-побег)

	Boolean IsScared = false; // испугана ли утка

	public GameObject Vision; // область видимости утки

	Vector3 run; // куда убегает утка

	// Start is called before the first frame update
	void Start()
    {
        speed = speedMin;
    }

    // Update is called once per frame
    void Update()
    {
        // определяем поле speed для анимации
        anim.SetFloat("walkspeed", speed);

        // таймер который заводит сам себя
        if (TimerSet == false)
        {
            timer = UnityEngine.Random.Range(3, 10); // вызов таймера на рандомное время от 3 до 9 секунд
            action = UnityEngine.Random.Range(1, 3); // выбор случайного действия 1 - движение, 2 - покой
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

        // уточка просто гуляет
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

        // уточка стоит
        if (action == 2)
        {
			anim.ResetTrigger("Walk");
			anim.SetTrigger("Idle");
		}

        // уточка испугалась
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
            run.y = 0; // чтобы утка не убегала наверх
            run = run / run.magnitude; // делим вектор на его же длину
            transform.Rotate(Vector3.up * Vector3.Angle(transform.forward, run)); // поворачиваем утку
            transform.Translate(0, 0, speed * Time.deltaTime);

			// убгаем из дистанции видимости
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
