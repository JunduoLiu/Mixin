using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; } // only modifiable within this class

    private void Awake()
    {
        Instance = this;
    }

    public Transform pfItemWorld;

    public Sprite stolenSprite;
    public Sprite questSprite;
}
