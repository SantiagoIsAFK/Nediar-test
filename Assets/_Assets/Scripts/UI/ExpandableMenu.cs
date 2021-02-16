using UnityEngine;


public class ExpandableMenu : MonoBehaviour
{
    [SerializeField]
    private bool state;
    
    [SerializeField]
    private GameObject[] canvas;

    public void AlternateMenu()
    {
        state = !state;
        foreach (var canva in canvas)
        {
            canva.SetActive(state);
        }
    }
}