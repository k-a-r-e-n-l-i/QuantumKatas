// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

//////////////////////////////////////////////////////////////////////
// This file contains profiling code.
// The tasks themselves can be found in Tasks.qs file.
//////////////////////////////////////////////////////////////////////

using System;
using System.Diagnostics;


using Microsoft.Quantum.Katas;
using Microsoft.Quantum.Simulation.XUnit;
using Microsoft.Quantum.Simulation.Simulators;

using Xunit.Abstractions;


namespace Quantum.Kata.DeutschJozsaAlgorithm
{
    public class ProfilingRunner
    {
        static void Main()
        {
            Stopwatch s = new Stopwatch();
            using (var sim = new QuantumSimulator()) {
                s.Start();
                T31_DJ_Algorithm_Test.Run(sim);
                s.Stop();
                TimeSpan ts = s.Elapsed;
                System.Console.WriteLine(String.Format("Time for T31: {0}", ts));
            }
        }
    }
}
