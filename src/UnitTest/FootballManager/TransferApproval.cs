using System;
namespace FootballManager
{
    public class TransferApproval
    {
        /// <summary>
        /// 剩余预算（百万）
        /// </summary>
        private const int RemainingTotalBudget = 300;

        private readonly IPhysicalExamination _physicalExamination;

        public TransferApproval(IPhysicalExamination physicalExamination)
        {
            _physicalExamination = physicalExamination ?? throw new ArgumentNullException(nameof(physicalExamination));
        }
        public TransferResult Evaluate(TransferApplication transfer)
        {
            // var isHealthy = _physicalExamination.IsHealthy(transfer.PlayerAge, transfer.PlayerStrength, transfer.PlayerSpeed);
            _physicalExamination.IsHealthy(transfer.PlayerAge, transfer.PlayerStrength, transfer.PlayerSpeed, out var isHealthy);
            
            if (!isHealthy) return TransferResult.Rejected;

            var totalTransferFee = transfer.TransferFee + transfer.ContractYears * transfer.AnnualSalary;

            if (RemainingTotalBudget < totalTransferFee) return TransferResult.Rejected;
            if (transfer.PlayerAge < 30) return TransferResult.Approved;
            if (transfer.IsSuperStar) return TransferResult.ReferredToBoss;
            return TransferResult.Rejected;
        }
    }
}