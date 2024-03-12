using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Src.PVE.MoveConditions;
using Src.Tutorial;

namespace Tests.EditMode.TutorialTests
{
    public class ListMoveConditionsSequenceTests
    {
        [Test]
        public void GetCurrentCondition_ShouldThrowInvalidOperationException_IfConditionsListIsEmpty()
        {
            var conditions = new List<IMoveCondition>();
            var sequence = new ListMoveConditionsSequence(conditions);

            Assert.Throws<InvalidOperationException>(() => sequence.GetCurrentCondition());
        }

        [Test]
        public void GetCurrentCondition_ShouldReturnFirstConditionInTheList_IfConditionsListIsNotEmpty()
        {
            var expectedCondition = new Mock<IMoveCondition>().Object;
            var conditions = new List<IMoveCondition> { expectedCondition };
            var sequence = new ListMoveConditionsSequence(conditions);

            var actualCondition = sequence.GetCurrentCondition();

            Assert.AreSame(expectedCondition, actualCondition);
        }

        [Test]
        public void GetCurrentCondition_ShouldReturnSecondConditionInTheList_IfMoveToNextConditionWasCalled()
        {
            var firstCondition = new Mock<IMoveCondition>().Object;
            var secondCondition = new Mock<IMoveCondition>().Object;
            var conditions = new List<IMoveCondition> { firstCondition, secondCondition };
            var sequence = new ListMoveConditionsSequence(conditions);

            sequence.MoveToNextCondition();
            var actualCondition = sequence.GetCurrentCondition();

            Assert.AreSame(secondCondition, actualCondition);
        }

        [Test]
        public void MoveToNextCondition_ShouldThrowInvalidOperationException_IfConditionsListIsEmpty()
        {
            var conditions = new List<IMoveCondition>();
            var sequence = new ListMoveConditionsSequence(conditions);

            Assert.Throws<InvalidOperationException>(() => sequence.MoveToNextCondition());
        }

        [Test]
        public void MoveToNextCondition_ShouldThrowInvalidOperationException_IfNoMoreConditionsInTheList()
        {
            var conditions = new List<IMoveCondition> { new Mock<IMoveCondition>().Object, new Mock<IMoveCondition>().Object };
            var sequence = new ListMoveConditionsSequence(conditions);

            sequence.MoveToNextCondition();

            Assert.Throws<InvalidOperationException>(() => sequence.MoveToNextCondition());
        }

        [Test]
        public void MoveToNextCondition_ShouldMakeGetCurrentCondition_ReturnNextConditionInTheList()
        {
            var rnd = new Random();
            var conditionsList = new List<IMoveCondition>();
            var conditionsCount = 10;
            for (int i = 0; i < conditionsCount; i++)
            {
                conditionsList.Add(new Mock<IMoveCondition>().Object);
            }
            var expectedConditionIndex = rnd.Next(0, conditionsCount);
            var expectedCondition = conditionsList[expectedConditionIndex];
            var sequence = new ListMoveConditionsSequence(conditionsList);
            
            for (int i = 0; i < expectedConditionIndex; i++)
            {
                sequence.MoveToNextCondition();
            }
            var actualCondition = sequence.GetCurrentCondition();
            
            Assert.AreSame(expectedCondition, actualCondition);
        }
    }
}