using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    float timer = 0;
    float distance = 0;
    float playerX = 0;
    float playerY = 0;
    float playerZ = 0;
    float HP = 10;
    float speed = 0;
    private GameObject prefab;
    public GameObject cap;
    public List<GameObject> pieces;
    private Vector3 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = transform.position;
        prefab = GameObject.FindGameObjectWithTag("Holder").GetComponent<PrefabHolder>().getPrefab();
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = transform.position;
        //Debug.Log(playerPos.ToString());
        timer += Time.deltaTime;

        if (timer >= 1)
        {
            Debug.Log("working");
            timer = 0;
            
            playerX = Mathf.Abs(playerPos.x);
            playerY = Mathf.Abs(playerPos.y);
            playerZ = Mathf.Abs(playerPos.z);
            distance = playerX + playerY + playerZ;
            distance = distance/50;

            for (int i = 0; i < distance; i++)
            {
                float randomMinePos = Random.Range(-1, 1);
                Vector3 mineVec = new Vector3(playerX + randomMinePos, playerY, playerZ + randomMinePos);
                Instantiate(prefab, mineVec , Quaternion.identity);
            }


        }

        //rotate
        if (Input.GetKeyDown(KeyCode.W))
        {
            speed++;
            if (speed > 10)
            {
                speed = 10;
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            cap.transform.localRotation = cap.transform.localRotation * Quaternion.Euler(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            cap.transform.localRotation = cap.transform.localRotation * Quaternion.Euler(0, 0, -1);
        }
        if (Input.GetKeyDown(KeyCode.S))
        { 
            speed--;
            if (speed < 0)
            {
                speed = 0;
            }
        }

        //move forward based on speed.
        transform.position += cap.transform.up * speed * Time.deltaTime;
    }

    public void hit()
    {
        HP--;
        if (HP != 0)
        {
            int index = Random.Range(0, pieces.Count - 1);
            GameObject go = pieces[index];
            pieces.Remove(go);
            go.transform.parent = null;
            go.AddComponent<Rigidbody>();
            go.GetComponent<Rigidbody>().useGravity = false;
            go.GetComponent<Rigidbody>().AddForce(Random.Range(-1000, 1000), Random.Range(0, 1000), Random.Range(-1000, 1000));
        }
        

        cap.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.Lerp(Color.green * 10, Color.red * 10, 1 - (HP / 100)));

        if (HP <= 0)
        {
            Destroy(cap);
        }

    }


}
