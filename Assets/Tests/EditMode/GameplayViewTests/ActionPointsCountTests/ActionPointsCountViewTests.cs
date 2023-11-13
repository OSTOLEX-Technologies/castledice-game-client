using NUnit.Framework;
using Src.GameplayView.ActionPointsCount;
using TMPro;
using UnityEngine;

namespace Tests.EditMode.GameplayViewTests.ActionPointsCountTests
{
    public class ActionPointsCountViewTests
    {
        [Test]
        public void ShowActionPointsCount_ShouldSetBothTextMeshesActive()
        {
            var labelTextMesh = new GameObject().AddComponent<TextMeshProUGUI>();
            var countTextMesh = new GameObject().AddComponent<TextMeshProUGUI>();
            labelTextMesh.gameObject.SetActive(false);
            countTextMesh.gameObject.SetActive(false);
            var view = new ActionPointsCountView(countTextMesh, labelTextMesh);
            
            view.ShowActionPointsCount(1);
            
            Assert.IsTrue(labelTextMesh.gameObject.activeSelf);
            Assert.IsTrue(countTextMesh.gameObject.activeSelf);
        }

        [Test]
        public void ShowActionPointsCount_ShouldSetCountText_ToGivenNumber([Values(1, 2, 3, 4, 5, 6)]int count)
        {
            var labelTextMesh = new GameObject().AddComponent<TextMeshProUGUI>();
            var countTextMesh = new GameObject().AddComponent<TextMeshProUGUI>();
            var view = new ActionPointsCountView(countTextMesh, labelTextMesh);
            
            view.ShowActionPointsCount(count);
            
            Assert.AreEqual(count.ToString(), countTextMesh.text);
        }

        [Test]
        public void HideActionPoints_ShouldDeactivateBothTextMeshes()
        {
            var labelTextMesh = new GameObject().AddComponent<TextMeshProUGUI>();
            var countTextMesh = new GameObject().AddComponent<TextMeshProUGUI>();
            labelTextMesh.gameObject.SetActive(true);
            countTextMesh.gameObject.SetActive(true);
            var view = new ActionPointsCountView(countTextMesh, labelTextMesh);
            
            view.HideActionPointsCount();
            
            Assert.IsFalse(labelTextMesh.gameObject.activeSelf);
        }
    }
}