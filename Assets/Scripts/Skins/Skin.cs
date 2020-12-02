using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skin : MonoBehaviour
{
    public GameObject lort;
    public GameObject lortToiletPaper;
    public GameObject lortHeadset;
    public GameObject lortHat;

    // Start is called before the first frame update
    void Start()
    {
        int index = FindObjectOfType<SkinSelector>().boughtIndex;

        switch (index)
        {
            case 0:
                lort.SetActive(true);
                break;

            case 1:
                lortToiletPaper.SetActive(true);
                break;

            case 2:
                lortHeadset.SetActive(true);
                break;

            case 3:
                lortHat.SetActive(true);
                break;
        }
    }
}
