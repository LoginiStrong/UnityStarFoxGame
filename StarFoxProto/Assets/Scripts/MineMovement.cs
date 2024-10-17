using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MineMovement : MonoBehaviour
{
    public GameObject sphere;
    public Color primary;
    public Color secondary;
    private float positionx;
    private float positionz;
    private float original_position_of_minex;
    private float original_position_of_minez;
    private float rand;
    float timer;
    private bool wasPrimary = false;
    // Start is called before the first frame update
    void Start()
    {
        original_position_of_minex = transform.position.x;
        original_position_of_minez = transform.position.z;
        rand = Random.Range(-10.0f, 10.0f);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 2)
        {
            timer = 0;
            if (wasPrimary == true)
            {
                wasPrimary = false;
                sphere.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", secondary);
            }
            else
            {
                sphere.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", primary);
                wasPrimary = true;
            }
            
        }
        positionx = original_position_of_minex + Mathf.Cos(Time.time + rand) * 5;
        positionz = original_position_of_minez + Mathf.Sin(Time.time + rand) * 5;
        transform.position = new Vector3(positionx, transform.position.y, positionz);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<Movement>().hit();
            Destroy(this.gameObject);
        }
    }

}
