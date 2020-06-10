using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [HideInInspector] public bool isAlive { get; set; }
   


    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        isAlive = true;
    }

    private void Update()
    {
        if (!GameManager.instance.isGameOver)
        {
            
        }
    }

    void OnDeath()
    {
        Debug.Log("Dead");
        GameManager.instance.isGameOver = true;
    }
}
