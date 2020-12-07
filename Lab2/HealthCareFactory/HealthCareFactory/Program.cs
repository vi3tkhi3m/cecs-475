// Khiem Pham #026909504
// CECS 475-01
// Lab 2
// Due date : 08/31/2020
using System;

namespace HealthCareFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter a health care plan. You can choose between ppo, obamacare or hmo.");
            bool done = false;

            while (done == false)
            {
                if (Enum.TryParse<HealthCarePlans>(Console.ReadLine(), ignoreCase: true, out var output))
                {
                    Console.WriteLine("Health Care Plan Details:");
                    HealthCarePlan plan = HealthCarePlanFactory.GetHealthCarePlan(output);
                    Console.WriteLine(plan.GetInfo());
                    done = true;
                }
                else
                {
                    Console.WriteLine("Unknown health care plan.");
                }
            }

        }
    }
}
