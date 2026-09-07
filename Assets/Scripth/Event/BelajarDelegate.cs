using UnityEngine;
using System;

public class BelajarDelegate : MonoBehaviour
{
    delegate void ContohDelegate();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UjiDelegate();
        UjiDelegate2();
        ActionDelegate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UjiDelegate()
    {
        ContohDelegate halo = PanggilHello;
        halo();
    }

    void UjiDelegate2()
    {
        ContohDelegate halo = PanggilHello;
        halo += PanggilNama;
        halo();
    }

    void ActionDelegate()
    {
        Action halo = PanggilHello;
        halo += PanggilNama;
        halo();
    }

    void PanggilHello()
    {
        Debug.Log("Hello");
    }

    void PanggilNama()
    {
        Debug.Log("Halo Pak Surya");
    }
}
