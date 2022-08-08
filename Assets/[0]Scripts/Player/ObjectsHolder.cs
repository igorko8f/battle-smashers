using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectsHolder : MonoBehaviour
{
    [SerializeField] private float objectsInListDistance = 1f;
    [SerializeField] private Transform listBeginPosition;
    [SerializeField] private TextMeshPro capacityText;
    [SerializeField] private bool showCurrentObjectsCount = true;
    [SerializeField] private bool changeTextPosition = false;
    [SerializeField] private bool isPlayer = false;

    private int maximumCapacity = 4;
    private CharacterData _data;

    public void SetCharacterData(CharacterData data)
    {
        _data = data;
        maximumCapacity = _data.InventoryCapacity;
    }

    public bool HandsIsEmpty()
    {
        return false;
    }

    public bool HandsIsFull()
    {
        return false;
    }

    public bool AddToList()
    {
        if (HandsIsFull())
        {
            return false;
        }
        
        SortCollection();
        
        return true;
    }

    private void SortCollection()
    {
        
    }

    public void ActualizeObjectsCountText(int currentObjectCount)
    {
        string text = "";

        if (showCurrentObjectsCount && currentObjectCount > 0)
        {
            text = currentObjectCount + "/" + _data.InventoryCapacity;
        }
        
        if (currentObjectCount >= _data.InventoryCapacity)
        {
            text = "MAX";
        }

        capacityText.text = text;

        if (changeTextPosition)
        {
            var beginYPosition = listBeginPosition.position;
            beginYPosition.y += (currentObjectCount + 5) * objectsInListDistance;

            capacityText.transform.position = beginYPosition;
        }
    }
}
