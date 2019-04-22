using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionManager : SimpleSingleton<ExplosionManager>
{
    //[Header("Settings")]

    [Header("References")]
    public Transform rocketPosition1;
    public Transform rocketPosition2;
    public Transform rocketTrailParticleSystemTransform;

    //[Header("Prefabs")]

    //[Header("Accessors")]

    [Header("Private")]
    public bool fireFromOne = true;

    public IEnumerator ExplodeAt(Transform enemyTransform)
    {
        if (fireFromOne)
        {
            rocketTrailParticleSystemTransform.position = rocketPosition1.position;
        }
        else
        {
            rocketTrailParticleSystemTransform.position = rocketPosition2.position;
        }
        fireFromOne = !fireFromOne;

        rocketTrailParticleSystemTransform.LookAt(enemyTransform);
        rocketTrailParticleSystemTransform.GetComponent<ParticleSystem>().Play(true);

        float rocketSpeed = 20; //Hardcoded because I didnt know how to get the speed

        float timeToHit = (enemyTransform.position.z - 0) / rocketSpeed;
        //TODO Calc time to hit
        float currentTime = 0;

        while (timeToHit > currentTime)
        {
            currentTime += Time.deltaTime;
            timeToHit = (enemyTransform.position.z - 0) / rocketSpeed;
            print(currentTime + "   " + timeToHit + "   " + enemyTransform.position.z);
            rocketTrailParticleSystemTransform.LookAt(enemyTransform);
            yield return null;
        }

        rocketTrailParticleSystemTransform.GetComponent<ParticleSystem>().Stop(true,ParticleSystemStopBehavior.StopEmittingAndClear);

        enemyTransform.gameObject.SetActive(false);//TODO REMOVE THIS
    }

}
