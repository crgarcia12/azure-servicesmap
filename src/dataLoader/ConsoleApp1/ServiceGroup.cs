using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class ServiceGroup
    {
        public string Name { get; set; }

        public List<Service> Services { get; private set; } = new List<Service>();
    }
}
