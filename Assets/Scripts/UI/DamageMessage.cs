using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageMessage : MonoBehaviour
{
    [SerializeField] float timeToLeave = 2f;
    float timeLeft = 2f;
    

    private void OnEnable()
    {
        timeLeft = timeToLeave;
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0f )
        {
            gameObject.SetActive(false);
        }
    }
}
