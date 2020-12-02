using System;
using UnityEngine;

public class SkinSelector : MonoBehaviour
{
    public int boughtIndex;

    public static SkinSelector instance;

    private void Start() {
        
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
    }

    public void ParseSkin(int index) {
        boughtIndex = index;
    }
}
