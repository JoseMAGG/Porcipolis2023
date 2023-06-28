using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableComponent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TamagotchiEvent.instance.OnCerdoMuerto += DisableComponent_;
    }

    // Update is called once per frame
    public void DisableComponent_()
    {
        TamagotchiEvent.instance.OnCerdoMuerto -= DisableComponent_;

        this.gameObject.SetActive(false);
    }
}
