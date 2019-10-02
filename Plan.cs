using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GovtWalletApi
{
    public class Plan
    {
        public string PlanName { get; set; }
        public int BudgetAllocated { get; set; }
        public int RemainingBudget { get; set; }
    }
}
