using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiamondController : MonoBehaviour
{
    [SerializeField]
    private playerController pc;

    [SerializeField]
    private Image[] diamonds;

    [SerializeField]
    private Sprite[] abilities;

    [SerializeField]
    private Sprite[] numbers;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int abilityIndex = (int)pc.currentColor;
        diamonds[0].sprite = abilities[abilityIndex];

        diamonds[1].sprite = numbers[pc.redLimit];
        diamonds[2].sprite = numbers[pc.yellowLimit];
        diamonds[3].sprite = numbers[pc.blueLimit];

    }
}
