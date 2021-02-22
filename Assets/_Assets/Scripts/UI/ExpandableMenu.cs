using UnityEngine;


namespace PokeUI
{
    /// <summary>
    /// Allow active or deactive a Gameobject, use to open and close a UI
    /// </summary>
    public class ExpandableMenu : MonoBehaviour
    {
        
        [SerializeField] private bool state;

        /// <summary>
        /// Array with GO to open and close
        /// </summary>
        [SerializeField] private GameObject[] canvas;

        /// <summary>
        /// Alternate its state
        /// </summary>
        public void AlternateMenu()
        {
            state = !state;
            foreach (var canva in canvas)
            {
                canva.SetActive(state);
            }
        }
    }
}