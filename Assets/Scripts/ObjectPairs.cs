using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPairs : MonoBehaviour
{
    public Transform aMarker, bMarker;
    public Transform oneMarker, twoMarker, threeMarker, fourMarker;
    public GameObject options;
    public List<Pair> pairs;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        HidePair();
    }

    public void ShowPair(UXF.Trial trial)
    {
        Pair pair = (Pair)trial.settings.GetObject("pair");
        Instantiate(pair.prefabA, aMarker.position, aMarker.rotation, aMarker);

        bool recall = trial.settings.GetBool("recall");

        if (recall)
        {
            GameObject option1Prefab = (GameObject) trial.settings.GetObject("option_object_1");
            GameObject option2Prefab = (GameObject) trial.settings.GetObject("option_object_2");
            GameObject option3Prefab = (GameObject) trial.settings.GetObject("option_object_3");
            GameObject option4Prefab = (GameObject) trial.settings.GetObject("option_object_4");

            GameObject option1 = Instantiate(option1Prefab, oneMarker.position, oneMarker.rotation, oneMarker);
            option1.name = option1Prefab.name;
            GameObject option2 = Instantiate(option2Prefab, twoMarker.position, twoMarker.rotation, twoMarker);
            option2.name = option2Prefab.name;
            GameObject option3 = Instantiate(option3Prefab, threeMarker.position, threeMarker.rotation, threeMarker);
            option3.name = option3Prefab.name;
            GameObject option4 = Instantiate(option4Prefab, fourMarker.position, fourMarker.rotation, fourMarker);
            option4.name = option4Prefab.name;

            options.SetActive(true);
        }
        else
        {
            Instantiate(pair.prefabB, bMarker.position, bMarker.rotation, bMarker);
        }
    }

    public void HidePair()
    {
        foreach (Transform child in aMarker) Destroy(child.gameObject);
        foreach (Transform child in bMarker) Destroy(child.gameObject);
        foreach (Transform child in oneMarker) Destroy(child.gameObject);
        foreach (Transform child in twoMarker) Destroy(child.gameObject);
        foreach (Transform child in threeMarker) Destroy(child.gameObject);
        foreach (Transform child in fourMarker) Destroy(child.gameObject);
        options.SetActive(false);
    }
}

[System.Serializable]
public class Pair 
{
    public GameObject prefabA;
    public GameObject prefabB;
}
