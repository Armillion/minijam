using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class counter : MonoBehaviour
{
    public float cooldown = 5f;
    private float cnt = 0f;

    // Update is called once per frame
    void Update()
    {
        cnt += Time.deltaTime;
        if(cnt > cooldown)
        {
            Destroy(gameObject);
        }
    }
}
