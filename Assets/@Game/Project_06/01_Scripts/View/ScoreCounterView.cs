using System;
using TMPro;
using UnityEngine;

public class ScoreCounterView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _orderText;
    [SerializeField] private TextMeshProUGUI _situationText;


    public void SetOrder(int count)
    {
        _orderText.text = $"目標: ブロックを{count}列消す";
    }

    public void SetSitsuation(int count)
    {
        _situationText.text = $"クリアまであと{Math.Max(0,  count)}列";
    }
}
