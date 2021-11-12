using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace MenuSystem
{
    public class MenuMaster : MonoBehaviour
    {
        public List<MenuPanel> panels;

        private void Start()
        {
            OpenPanel(0);
        }

        public void OpenPanel(int index)
        {
            OpenPanel(index, true);
        }
        public void OpenPanel(int index, bool exclusive = true)
        {
            if (exclusive) CloseAllPanels();
            panels[index].gameObject.SetActive(true);
        }
        public void OpenPanel(MenuPanel panel, bool exclusive = true)
        {
            if (exclusive) CloseAllPanels();
            panel.gameObject.SetActive(true);
        }
        public void ClosePanel(int index)
        {
            transform.GetChild(index).gameObject.SetActive(false);
        }
        public void CloseAllPanels()
        {
            foreach (MenuPanel i in panels)
            {
                i.gameObject.SetActive(false);
            }
        }
        public bool PanelIsOpen(int index)
        {
            return panels[index].gameObject.activeSelf;
        }
    }
}