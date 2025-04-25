using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExampleUI : PopupUI
{
    TextMeshProUGUI text;
    // Start is called before the first frame update
    public void Init(string input)
    {
        text.text = input;
    }

    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    protected override void Start()
    {
        base.Start();
        Invoke(nameof(DestroySelf), 2.0f);
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
