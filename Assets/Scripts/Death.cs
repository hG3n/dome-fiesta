using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour {

    public ParticleSystem ps;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (ps)
        {
            if (!ps.IsAlive())
            {
                Destroy(this.gameObject);
            }
        }
    }
}
