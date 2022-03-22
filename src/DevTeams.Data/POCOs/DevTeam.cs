using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

    public class DevTeam
    {
        public DevTeam(){}

        public DevTeam(string name)
        {
            Name = name;
        }

        public DevTeam(string name, List<Developer> developers)
        {
            Name = name;
            Developers = developers;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public List<Developer> Developers { get; set; } = new List<Developer>();
    }