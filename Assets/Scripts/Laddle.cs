using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laddle : MonoBehaviour
{
    public static Laddle instance;
    public GameObject scoopOfSoup;
    public GameObject tangYuanSoup;
    public static bool hasSoup;
    private float cooldown;

    private void Start()
    {
        instance = this;
    }
    private void Update()
    {
        if(cooldown > 0)
        {
            cooldown -= Time.deltaTime;
            if (cooldown < 0)
            {
                cooldown = 0;
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "TangYuan")
        {
            if(cooldown == 0)
            {
                scoopOfSoup.SetActive(true);
                other.gameObject.SetActive(false);
                cooldown = 3.0f;
                hasSoup = true;
            }
            
        }
        else if(other.gameObject.tag == "EmptyBowl")
        {
            scoopOfSoup.SetActive(false);
        }
    }

    public void Reset()
    {
        scoopOfSoup.SetActive(false);
    }
}
