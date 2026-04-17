using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FunFactScript : MonoBehaviour
{
    [TextArea(2, 5)]
    public List<string> funFacts = new List<string>();

    public TextMeshProUGUI factText;

    private void OnEnable()
    {
        ShowRandomFact();
    }

    void ShowRandomFact()
    {
        if (funFacts.Count == 0) return;

        int index = Random.Range(0, funFacts.Count);
        factText.text = funFacts[index];
    }
}