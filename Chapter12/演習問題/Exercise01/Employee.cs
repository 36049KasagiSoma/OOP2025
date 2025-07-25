﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise01 {
    public record Employee {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime HireDate {  get; set; }


        public override string ToString() {
            return $"{Id},{Name},{HireDate.ToString()}";
        }
    }
}
