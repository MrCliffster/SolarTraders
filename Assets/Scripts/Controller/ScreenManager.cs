using UnityEngine;
using UnityEngine.UI;

namespace SolarTraders
{
    public class ScreenManager : MonoBehaviour 
    { 
        [SerializeField]
        private GameObject SelectionDialogue;

        [SerializeField]
        private Text title;
        [SerializeField]
        private Text description;

        [SerializeField]
        private GameObject ControlPanel;
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
                if (ControlPanel.activeInHierarchy)
                {
                    ControlPanel.SetActive(false);
                    BuildPanel.SetActive(true);
                }

                title.text = body.Name;

                if (body.Explored)
                {
                    description.text = body.ToString();
                }
                else
                {
                    description.text = "This planetary body is currently unexplored. Send a probe to explore it!";
                }
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
                if (BuildPanel.activeInHierarchy)
                {
                    ControlPanel.SetActive(true);
                    BuildPanel.SetActive(false);
                }

                title.text = ship.Name;

                description.text = "No details available for Ships yet.";
            }
        }

        public void CloseSelectionDialogue()
        {
            if (!BuildPanel.activeInHierarchy && !ControlPanel.activeInHierarchy)
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