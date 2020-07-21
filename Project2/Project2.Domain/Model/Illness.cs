using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Project2.Domain.Model
{
    public class Illness
    {
        private string _name;
        public int IllnessId { get; set; }
        public string Name {
            get
            {
                return _name;
            } 

            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentNullException("Illness name must have a value", nameof(value));
                }
                else if (value.GetType() != typeof(string))
                {
                    throw new ArgumentException("Illness name must be a string", nameof(value));
                }
                _name = value;
            }
        }

        public Illness(int id, string name)
        {
            IllnessId = id;
            _name = name;

        }

        public Illness() { }

    }
}
