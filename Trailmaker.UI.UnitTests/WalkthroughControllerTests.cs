using NUnit.Framework;
using System;
using System.Collections.Generic;
using Moq;
using System.Threading.Tasks;

namespace Trailmaker.UI.UnitTests
{
    [TestFixture()]
    public class WalkthroughControllerTests
    {
        Mock<WalkthroughController.IWalkthroughView> mockView;
        List<string> onePageList = new List<string> { "Page 1" };
        List<string> twoPageList = new List<string> { "Page 1", "Page 2" };
        List<string> threePageList = new List<string> { "Page 1", "Page 2", "Page 3" };

        [SetUp]
        public void Setup()
        {
            mockView = new Mock<WalkthroughController.IWalkthroughView>();;
        }

        [Test()]
        public void FirstPageValuesCorrect()
        {
            var controller = new WalkthroughController(mockView.Object, threePageList);

            Assert.AreEqual(0, controller.CurrentPage);
            Assert.AreEqual(3, controller.PageCount);
            Assert.IsTrue(controller.OnFirstPage);
            Assert.IsFalse(controller.OnLastPage);
            Assert.IsFalse(controller.BackIsVisible);
            Assert.IsTrue(controller.NextIsVisible);
        }

        [Test()]
        public async Task MiddlePageValuesCorrect()
        {
			var controller = new WalkthroughController(mockView.Object, threePageList);

            await controller.ChangePageForwards();

			Assert.AreEqual(1, controller.CurrentPage);
			Assert.AreEqual(3, controller.PageCount);
			Assert.IsFalse(controller.OnFirstPage);
			Assert.IsFalse(controller.OnLastPage);
			Assert.IsTrue(controller.BackIsVisible);
			Assert.IsTrue(controller.NextIsVisible);
        }

        [Test()]
        public async Task LastPageValuesCorrect()
        {
			var controller = new WalkthroughController(mockView.Object, twoPageList);

			await controller.ChangePageForwards();

			Assert.AreEqual(1, controller.CurrentPage);
			Assert.AreEqual(2, controller.PageCount);
			Assert.IsFalse(controller.OnFirstPage);
			Assert.IsTrue(controller.OnLastPage);
			Assert.IsTrue(controller.BackIsVisible);
			Assert.IsFalse(controller.NextIsVisible);
        }

        [Test()]
        public async Task BackToMiddlePageValuesCorrect()
        {
			var controller = new WalkthroughController(mockView.Object, threePageList);

			await controller.ChangePageForwards();
            await controller.ChangePageForwards();
            await controller.ChangePageBackwards();

			Assert.AreEqual(1, controller.CurrentPage);
			Assert.AreEqual(3, controller.PageCount);
			Assert.IsFalse(controller.OnFirstPage);
			Assert.IsFalse(controller.OnLastPage);
			Assert.IsTrue(controller.BackIsVisible);
			Assert.IsTrue(controller.NextIsVisible);
        }

        [Test()]
        public async Task BackToFirstPageValuesCorrect()
        {
			var controller = new WalkthroughController(mockView.Object, twoPageList);

            await controller.ChangePageForwards();
            await controller.ChangePageBackwards();

			Assert.AreEqual(0, controller.CurrentPage);
			Assert.AreEqual(2, controller.PageCount);
			Assert.IsTrue(controller.OnFirstPage);
			Assert.IsFalse(controller.OnLastPage);
			Assert.IsFalse(controller.BackIsVisible);
			Assert.IsTrue(controller.NextIsVisible);
        }

        [Test()]
        public void SinglePageValuesCorrect()
        {
			var controller = new WalkthroughController(mockView.Object, onePageList);

			Assert.AreEqual(0, controller.CurrentPage);
			Assert.AreEqual(1, controller.PageCount);
			Assert.IsTrue(controller.OnFirstPage);
			Assert.IsTrue(controller.OnLastPage);
			Assert.IsFalse(controller.BackIsVisible);
			Assert.IsFalse(controller.NextIsVisible);
        }

        [Test()]
        public async Task FastPageChangeIgnored()
        {
            mockView.Setup(m => m.ChangePage(true)).Returns(Task.Delay(10)); 
            var controller = new WalkthroughController(mockView.Object, threePageList);

            await Task.WhenAll(
                controller.ChangePageForwards(),
                controller.ChangePageForwards()
            );

            Assert.AreEqual(1, controller.CurrentPage);
            mockView.Verify(m => m.ChangePage(true), Times.Exactly(1)); 
        }

        [Test()]
        public async Task CantGoPastStart()
        {
            var controller = new WalkthroughController(mockView.Object, onePageList);

            await controller.ChangePageBackwards();

            Assert.AreEqual(0, controller.CurrentPage);
        }

        public async Task CantGoPastEnd()
        {
            var controller = new WalkthroughController(mockView.Object, onePageList);

            await controller.ChangePageForwards();

            Assert.AreEqual(0, controller.CurrentPage);
        }
    }
}
