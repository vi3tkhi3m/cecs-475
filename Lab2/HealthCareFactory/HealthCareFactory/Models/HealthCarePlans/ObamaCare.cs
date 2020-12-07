using System;
using System.Collections.Generic;
using System.Text;

namespace HealthCareFactory
{
    class ObamaCare : HealthCarePlan
    {
        public decimal AnnualCharge { get; set; }
        public decimal DeductionAmount { get; set; }
        public string Type { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public ObamaCare()
        {
            this.AnnualCharge = 5000.00M;
            this.DeductionAmount = 550.00M;
            this.Type = "Obama Care";
        }

        /// <summary>
        /// Prints the details of this object.
        /// </summary>
        /// <returns>Returns the details of this object in a string</returns>
        public string GetInfo()
        {
            return $"Annual Charge: {AnnualCharge} \nDeduction Amount: {DeductionAmount} \nType: {Type}";
        }
    }
}
