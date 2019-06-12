using System;

using Microsoft.Quantum.Simulation.Core;
using Microsoft.Quantum.Simulation.Simulators;

namespace Quantum.BU
{
    class Driver
    {
        static void Main(string[] args)
        {
            using (var qsim = new QuantumSimulator())
            {
                var res = BorrowingDriver.Run(qsim).Result;
                (Result before, Result after) = res;
                Console.WriteLine("Value Before: " + before + "\t Value After: " + after);

                res = UsingDriver.Run(qsim).Result;
                (before, after) = res;
                Console.WriteLine("Value Before: " + before + "\t Value After: " + after);
            }
        }
    }
}