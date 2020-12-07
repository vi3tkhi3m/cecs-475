using System;
using System.Collections.Generic;
using System.Text;

namespace HealthCareFactory
{
    abstract class HealthCarePlanFactory
    {
        public static HealthCarePlan GetHealthCarePlan(HealthCarePlans healthCarePlan)
        {
            HealthCarePlan plan;

            switch (healthCarePlan)
            {
                case HealthCarePlans.hmo:
                    plan = new HealthMaintenanceOrganization();
                    break;
                case HealthCarePlans.ppo:
                    plan = new PreferredProviderOrganization();
                    break;
                case HealthCarePlans.obamacare:
                    plan = new ObamaCare();
                    break;
                default:
                    plan = new HealthMaintenanceOrganization();
                    break;
            }

            return plan;
        }
    }
}
