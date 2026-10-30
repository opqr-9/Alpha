using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button buildButton;

    private void Start()
    {
        buildButton.onClick.AddListener(() => Construct(0));
    }

    private void Construct(int id)
    {
        Debug.Log("clicked");
        PlacementSystem.instance.StartPlacement(id);
    }
}
