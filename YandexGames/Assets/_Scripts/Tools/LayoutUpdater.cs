using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Tools
{
    public class LayoutUpdater : MonoBehaviour
    {
        [SerializeField] private GameObject _layoutRoot;

        public void UpdateAllLayouts()
        {
            RefreshLayoutGroupsImmediateAndRecursive(_layoutRoot);
        }

        private void RefreshLayoutGroupsImmediateAndRecursive(GameObject root)
        {
            var componentsInChildren = root.GetComponentsInChildren<LayoutGroup>(true);
            foreach (var layoutGroup in componentsInChildren)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(layoutGroup.GetComponent<RectTransform>());
            }

            var parent = root.GetComponent<LayoutGroup>();
            LayoutRebuilder.ForceRebuildLayoutImmediate(parent.GetComponent<RectTransform>());
        }
    }
}