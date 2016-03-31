﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactDrone
{
    public class Program
    {
        public static void Main()
        {
            var circlingGPS =
                GPS.Create(Observable.Generate(0, i => i < 360, i => i + 1, i => i, _ => TimeSpan.FromSeconds(1.0/5.0))
                    .Select(i => Math.PI/180.0*i)
                    .Select(angle => new Location(5*Math.Sin(angle), 5*Math.Cos(angle), 2))
                    .Repeat());

            circlingGPS.Location.Sample(TimeSpan.FromSeconds(1))
                .Subscribe(l => Console.WriteLine($"({l.Latitude:000.000},{l.Longitude:000.000},{l.Altitude:000.000})"));
            Console.ReadLine();
        }
    }
}