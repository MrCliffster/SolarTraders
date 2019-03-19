using UnityEngine;
using UnityEngine.UI;

namespace SolarTraders
{
    public class ScreenManager : MonoBehaviour 
    { 
        [SerializeField]
        private GameObject SelectionDialogue;
        [SerializeField]
        private GameObject BuildPanel;
        [SerializeField]
        private GameObject DroneBuildDialogue;
    
        public void ShowSelectionDialogue(PlanetaryBody body)
        {
            if (SelectionDialogue.activeInHierarchy)
            {
                Debug.LogWarning("Selection Dialogue already shown!");
            }
            else
            {
                SelectionDialogue.SetActive(true);
            }
        }

        public void ShowSelectionDialogue(Ship ship)
        {
            if (SelectionDialogue.activeInHierarchy)
            {
                Debug.LogWarning("Selection Dialogue already shown!");
            }
            else
            {
                SelectionDialogue.SetActive(true);
            }
        }

        public void CloseSelectionDialogue()
        {
            if (!BuildPanel.activeInHierarchy)
            {
                Debug.LogWarning("Must close interior dialogues first!");
            }
            else if (!SelectionDialogue.activeInHierarchy)
            {
                Debug.LogWarning("Selection Dialogue already hidden!");
            }
            else
            {
                SelectionDialogue.SetActive(false);
            }
        }

        public void ShowDroneBuildDialogue()
        {
            if (DroneBuildDialogue.activeInHierarchy)
            {
                Debug.LogWarning("Selection Dialogue already shown!");
            }
            else
            {
                DroneBuildDialogue.SetActive(true);
                BuildPanel.SetActive(false);
            }
        }

        public void CloseDroneBuildMenu()
        {
            if (!DroneBuildDialogue.activeInHierarchy)
            {
                Debug.LogWarning("Selection Dialogue already hidden!");
            }
            else
            {
                DroneBuildDialogue.SetActive(false);
                BuildPanel.SetActive(true);
            }
        }
    }
}