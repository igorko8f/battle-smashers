using System;
using DG.Tweening;
using Project.Internal;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyLable;
    private float _money = 0;
    private const string MoneySavekey = "MONEY_SAVEKEY";
    public float Money => _money;

    [Button]
    [GUIColor(0, 1, 0)]
    private void Get1000()
    {
        AddMoney(1000);
    }

    public void Start()
    {
        _money = ES3.Load(MoneySavekey, 0f);
        moneyLable.text = Mathf.RoundToInt(_money).ToString();
    }

    public bool HasEnoughtMoney(float moneyForCheck)
    {
        return _money >= moneyForCheck;
    }

    public bool SpentMoney(float moneyToSpend)
    {
        if (HasEnoughtMoney(moneyToSpend) == false)
        {
            return false;
        }

        _money -= moneyToSpend;
        moneyLable.text = Mathf.RoundToInt(_money).ToString();
        ES3.Save(MoneySavekey, _money);
        
        return true;
    }

    public void AddMoney(float moneyToAdd)
    {
        var currentMoney = _money;
        var moneyto = currentMoney + moneyToAdd;
        
        DOTween.To(()=> currentMoney, x=> currentMoney = x, moneyto, 0.5f).OnUpdate(() =>
        {
            moneyLable.text = Mathf.RoundToInt(currentMoney).ToString();
        });
        
        _money += moneyToAdd;
        ES3.Save(MoneySavekey, _money);
    }
}
