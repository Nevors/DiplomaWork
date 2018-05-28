using Minifying.External.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Minifying.External.Abstract {
    public interface IOutputMessage {
        string Message { get; }
        IEnumerable<AnswerType> Actions { get; }
        void ExecuteAction(AnswerType answer);
    }
}
