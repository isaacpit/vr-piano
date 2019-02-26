using UnityEngine;

public class DistanceReader : MonoBehaviour
{
    public GameObject objToMeasure;

    Vector3 originalPos;
    Vector3 objOriginalPos;

    public float Distance { get { return Vector3.Distance(originalPos, objOriginalPos); } }
    private float originalDistance;
    
    private void Awake()
    {
        MeasureNewObject(objToMeasure);
    }

    public float GetDistanceRatioX()
    {
        return (transform.position.x - objToMeasure.transform.position.x) / (originalPos.x - objOriginalPos.x);
    }

    public float GetDistanceRatioY()
    {
        return (transform.position.y - objToMeasure.transform.position.y) / (originalPos.y - objOriginalPos.y);
    }

    public float GetDistanceRatioZ()
    {
        return (transform.position.z - objToMeasure.transform.position.z) / (originalPos.z - objOriginalPos.z);
    }

    public float GetDistanceRatio()
    {
        return Distance / originalDistance;
    }

    public void MeasureNewObject(GameObject obj)
    {
        objToMeasure = obj;
        originalPos = transform.position;
        objOriginalPos = objToMeasure.transform.position;
        originalDistance = Distance;
    }

    public float DistanceBetween(GameObject obj)
    {
        return (transform.position - obj.transform.position).sqrMagnitude;
    }

}
