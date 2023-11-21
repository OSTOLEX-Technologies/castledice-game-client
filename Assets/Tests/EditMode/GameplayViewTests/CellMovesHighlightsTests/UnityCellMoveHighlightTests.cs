using castledice_game_logic.MovesLogic;
using NUnit.Framework;
using Src.GameplayView.CellMovesHighlights;
using UnityEngine;
using static Tests.ObjectCreationUtility;

namespace Tests.EditMode.GameplayViewTests.CellMovesHighlightsTests
{
    public class UnityCellMoveHighlightTests
    {
         [Test]
         [TestCase(MoveType.Place)]
         [TestCase(MoveType.Upgrade)]
         public void ShowHighlight_ShouldSetGreenHighlightActive_ForGivenMoveTypes(MoveType moveType)
         {
             var greenHighlight = new GameObject();
             greenHighlight.SetActive(false);
             var orangeHighlight = new GameObject();
             orangeHighlight.SetActive(false);
             var highlight = new GameObject().AddComponent<UnityCellMoveHighlight>();
             AddObjectReferenceValueToSerializedProperty(highlight, "greenHighlight", greenHighlight);
             AddObjectReferenceValueToSerializedProperty(highlight, "orangeHighlight", orangeHighlight);
             
             highlight.ShowHighlight(moveType);
             
             Assert.IsTrue(greenHighlight.activeSelf);
         }
         
         [Test]
         [TestCase(MoveType.Remove)]
         [TestCase(MoveType.Replace)]
         [TestCase(MoveType.Capture)]
         public void ShowHighlight_ShouldSetOrangeHighlightActive_ForGivenMoveTypes(MoveType moveType)
         {
             var greenHighlight = new GameObject();
             greenHighlight.SetActive(false);
             var orangeHighlight = new GameObject();
             orangeHighlight.SetActive(false);
             var highlight = new GameObject().AddComponent<UnityCellMoveHighlight>();
             AddObjectReferenceValueToSerializedProperty(highlight, "greenHighlight", greenHighlight);
             AddObjectReferenceValueToSerializedProperty(highlight, "orangeHighlight", orangeHighlight);
             
             highlight.ShowHighlight(moveType);
             
             Assert.IsTrue(orangeHighlight.activeSelf);
         }
         
         [Test]
         [TestCase(MoveType.Place)]
         [TestCase(MoveType.Upgrade)]
         public void HideHighlight_ShouldSetGreenHighlightInactive_ForGivenMoveTypes(MoveType moveType)
         {
             var greenHighlight = new GameObject();
             greenHighlight.SetActive(true);
             var orangeHighlight = new GameObject();
             orangeHighlight.SetActive(true);
             var highlight = new GameObject().AddComponent<UnityCellMoveHighlight>();
             AddObjectReferenceValueToSerializedProperty(highlight, "greenHighlight", greenHighlight);
             AddObjectReferenceValueToSerializedProperty(highlight, "orangeHighlight", orangeHighlight);
             
             highlight.HideHighlight(moveType);
             
             Assert.IsFalse(greenHighlight.activeSelf);
         }
         
         [Test]
         [TestCase(MoveType.Remove)]
         [TestCase(MoveType.Replace)]
         [TestCase(MoveType.Capture)]
         public void HideHighlight_ShouldSetOrangeHighlightInactive_ForGivenMoveTypes(MoveType moveType)
         {
             var greenHighlight = new GameObject();
             greenHighlight.SetActive(true);
             var orangeHighlight = new GameObject();
             orangeHighlight.SetActive(true);
             var highlight = new GameObject().AddComponent<UnityCellMoveHighlight>();
             AddObjectReferenceValueToSerializedProperty(highlight, "greenHighlight", greenHighlight);
             AddObjectReferenceValueToSerializedProperty(highlight, "orangeHighlight", orangeHighlight);
             
             highlight.HideHighlight(moveType);
             
             Assert.IsFalse(orangeHighlight.activeSelf);
         }

         [Test]
         public void HideAllHighlights_ShouldSetAllHighlightGameObjectsInactive()
         {
             var greenHighlight = new GameObject();
             greenHighlight.SetActive(true);
             var orangeHighlight = new GameObject();
             orangeHighlight.SetActive(true);
             var highlight = new GameObject().AddComponent<UnityCellMoveHighlight>();
             AddObjectReferenceValueToSerializedProperty(highlight, "greenHighlight", greenHighlight);
             AddObjectReferenceValueToSerializedProperty(highlight, "orangeHighlight", orangeHighlight);
             
             highlight.HideAllHighlights();
             
             Assert.IsFalse(orangeHighlight.activeSelf);
             Assert.IsFalse(greenHighlight.activeSelf);
         }
    }
}