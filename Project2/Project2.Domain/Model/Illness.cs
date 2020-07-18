using System;
using System.Collections.Generic;
using System.Text;

namespace Project2.Domain.Model
{
    public class Illness
    {
        public int IllnessId { get; set; }
        public string Name { get; set; }

        public Illness(int id, string name)
        {
            IllnessId = id;
            Name = name;

        }

        public Illness() { }

    }
}
