using Moq;
using Xunit;

namespace FootballManager.Tests
{
    public class TransferApprovalShould
    {
        [Fact]
        public void ApproveYoungCheapPlayerTransfer()
        {
            // var approval = new TransferApproval(null);
            Mock<IPhysicalExamination> mockExamination = new Mock<IPhysicalExamination>();
            //设定默认返回true
            // mockExamination.Setup(x => x.IsHealthy(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            //符合设定条件的才返回true
            mockExamination.Setup(x =>
            x.IsHealthy(It.Is<int>(age => age < 30), It.IsIn(80, 85, 90), It.IsInRange<int>(75, 99, Moq.Range.Inclusive)))
            .Returns(true);
            var approval = new TransferApproval(mockExamination.Object);
            var emreTransfer = new TransferApplication
            {
                PlayerName = "Emre Can",
                PlayerAge = 24,
                TransferFee = 0,
                AnnualSalary = 4.52m,
                ContractYears = 4,
                IsSuperStar = false,
                PlayerStrength = 80,
                PlayerSpeed = 75
            };

            var result = approval.Evaluate(emreTransfer);
            Assert.Equal(TransferResult.Approved, result);
        }

        [Fact]
        public void ReferredToBossWhenTransferringSuperStar()
        {
            //var approval = new TransferApproval(null);
            //Strict模式下，必须预先定义好方法结果，否则会抛异常
            Mock<IPhysicalExamination> mockExamination = new Mock<IPhysicalExamination>(MockBehavior.Strict);
            //设定默认返回true
            //mockExamination.Setup(x => x.IsHealthy(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            var approval = new TransferApproval(mockExamination.Object);
            var emreTransfer = new TransferApplication
            {
                PlayerName = "Cristiano Ronaldo",
                PlayerAge = 33,
                TransferFee = 112m,
                AnnualSalary = 30m,
                ContractYears = 4,
                IsSuperStar = true,
                PlayerStrength = 80,
                PlayerSpeed = 75
            };

            var result = approval.Evaluate(emreTransfer);
            Assert.Equal(TransferResult.ReferredToBoss, result);
        }

        [Fact]
        public void RejectedWhenNonSuperstarOldPlayer()
        {
            Mock<IPhysicalExamination> mockExamination = new Mock<IPhysicalExamination>();
            bool isHealthy = true;
            mockExamination.Setup(x => x.IsHealthy(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), out isHealthy));
            var approval = new TransferApproval(mockExamination.Object);
            var carlosBaccaTransfer = new TransferApplication
            {
                PlayerName = "Carlos Bacca",
                PlayerAge = 32,
                TransferFee = 15m,
                AnnualSalary = 3.5m,
                ContractYears = 4,
                IsSuperStar = false,
                PlayerStrength = 80,
                PlayerSpeed = 70
            };

            var result = approval.Evaluate(carlosBaccaTransfer);
            Assert.Equal(TransferResult.Rejected, result);
        }
    }
}
