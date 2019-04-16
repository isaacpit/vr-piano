using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReticle : MonoBehaviour
{
    public GameObject reticle;
    public GameObject objective;
    public float reticleSpeedLimiter;
    
    public void AttachReticle()
    {
        if (reticle != null)
        {
            reticle.gameObject.SetActive(true);
        }
        
    }

    public void RemoveReticle()
    {
        if (reticle != null)
        {
            reticle.gameObject.SetActive(false);
        }
        
    }

    private void Update()
    {
        GameObject obj = GetComponent<Enemy>().m_objective;
        if (reticle.activeInHierarchy && obj != null)
        {

            //reticle.gameObject.transform.LookAt(obj.transform.position);
            reticle.gameObject.transform.Rotate(new Vector3(0, 1, 0), 90.0f);
            Vector3 p = reticle.gameObject.transform.position;
            p.y += Mathf.Sin(Time.time) / reticleSpeedLimiter;
            p.x += Mathf.Cos(Time.time) / reticleSpeedLimiter ;
            //p.x = Mathf.PerlinNoise(p.y, p.z);
            reticle.gameObject.transform.position = p;

        }
        
        
    }
}
