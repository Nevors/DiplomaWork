using System;
using System.Collections.Generic;
using System.Text;

namespace Minifying.Abstract {
    interface IOutputMessage : External.Abstract.IOutputMessage {
        void AddAction(External.Models.AnswerType answer, Action action);
    }
}
