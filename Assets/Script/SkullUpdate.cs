using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkullUpdate : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private TextMeshProUGUI DeathText;
    private enum SkullState { zero, cem, duzentos, trezentos, quatrocentos}
    SkullState state;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        UpdateAnimationStateSkull();
    }

    void UpdateAnimationStateSkull()
    {

        if (DeathText.text == "x " + 0)
        {
            state = SkullState.zero;
        }
        if (DeathText.text == "x " + 25)
        {
            state = SkullState.cem;
        }
        if (DeathText.text == "x " + 50)
        {
            state = SkullState.duzentos;
        }
        if (DeathText.text == "x " + 100)
        {
            state = SkullState.trezentos;
        }
        if (DeathText.text == "x " + 200)
        {
            state = SkullState.quatrocentos;
        }

        anim.SetInteger("state", (int)state);
    }
}
