using Minifying.External.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Minifying.External.Models
{
    public class Answer<T>
    {
        public int Id { get; set; }
        public T Value { get; set; }
    }
}
