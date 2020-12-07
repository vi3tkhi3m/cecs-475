using System;
using System.Collections.Generic;
using System.Text;

namespace HealthCareFactory
{
    class PreferredProviderOrganization : HealthCarePlan
    {
        public decimal AnnualCharge { get; set; }
        public decimal DeductionAmount { get; set; }
        public string Type { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public PreferredProviderOrganization()
        {
            this.AnnualCharge = 20000.00M;
            this.DeductionAmount = 300.00M;
            this.Type = "Preferred Provider Organization";
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
