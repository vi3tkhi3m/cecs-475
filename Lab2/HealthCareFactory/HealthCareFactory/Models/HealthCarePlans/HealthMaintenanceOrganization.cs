using System;
using System.Collections.Generic;
using System.Text;

namespace HealthCareFactory
{
    class HealthMaintenanceOrganization : HealthCarePlan
    {
        public decimal AnnualCharge { get; set; }
        public decimal DeductionAmount { get; set; }
        public string Type { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public HealthMaintenanceOrganization()
        {
            this.AnnualCharge = 12000.00M;
            this.DeductionAmount = 200.55M;
            this.Type = "Health Maintenance Organization";
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
