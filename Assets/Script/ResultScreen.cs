using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour
{
    [SerializeField] private FloatSO ggSO1;
    [SerializeField] private FloatSO ggSO2;
    [SerializeField] private FloatSO ggSO3;
    [SerializeField] private TextMeshProUGUI ggResult1;
    [SerializeField] private TextMeshProUGUI ggResult2;
    [SerializeField] private TextMeshProUGUI ggResult3;
    [SerializeField] private FloatSO deathSO1;
    [SerializeField] private FloatSO deathSO2;
    [SerializeField] private FloatSO deathSO3;
    [SerializeField] private TextMeshProUGUI deathResult1;
    [SerializeField] private TextMeshProUGUI deathResult2;
    [SerializeField] private TextMeshProUGUI deathResult3;


    private void Start()
    {
        ggResult1.text = ": " + ggSO1.Value + "/6";
        ggResult2.text = ": " + ggSO2.Value + "/6";
        ggResult3.text = ": " + ggSO3.Value + "/6";
        deathResult1.text = "x " + deathSO1.Value;
        deathResult2.text = "x " + deathSO2.Value;
        deathResult3.text = "x " + deathSO3.Value;
    }
}
