using Minifying.External.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class Answer
    {
        public string Id { get; set; }
        public bool IsProcessing { get; set; }
        public List<IOutputMessage> Messages { get; set; }
    }
}
