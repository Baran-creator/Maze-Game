using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    private Rigidbody rg;
    public float Speed;
    public UnityEngine.UI.Button btn;
    public UnityEngine.UI.Text time, health, durum;
    float timeSayac = 10;
    int healthSayac = 3;
    bool oyunDevam = true;
    bool oyunTamam = false;
    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (oyunDevam && !oyunTamam)
        {
            timeSayac -= Time.deltaTime;
            time.text = (int)timeSayac + "";
        }
        else if (!oyunTamam)
        {
            durum.text = ("Baþarýsýz Oldun!");
            btn.gameObject.SetActive(true);
        }
        if (timeSayac < 0) oyunDevam = false;
    }

    private void FixedUpdate()
    {
        if (oyunDevam && !oyunTamam) { 
        float yatay = Input.GetAxis("Horizontal");
        float dikey = Input.GetAxis("Vertical");
        Vector3 kuvvet = new Vector3(-dikey, 0, yatay);
        rg.AddForce (kuvvet*Speed);
        }
        else
        {
            rg.velocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision cls)
    {
        string ObjName = cls.gameObject.name;
        
        if (ObjName.Equals("WinPlane"))
        {
           //print("Oyun Tamamlandý!");
           oyunTamam = true;
            durum.text = ("Oyun Tamamlandý!");
            btn.gameObject.SetActive(true);
        }
        else if (!ObjName.Equals("Plane") && !ObjName.Equals("MainPlane"))
        {
            healthSayac -= 1;
            health.text = healthSayac+"";
            if (healthSayac == 0)
                oyunDevam = false;
        }

    }
} 