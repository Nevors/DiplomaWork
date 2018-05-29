using Minifying.Abstract;
using Minifying.Concrete.Common;
using Minifying.External.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Minifying.Common
{
    class FactoryOutputMessage
    {
        IOutputMessage message;
        public FactoryOutputMessage CreateMessage(string text) {
            message = new BaseOutputMessage(text);
            return this;
        }

        public FactoryOutputMessage AddAction(AnswerType answer,Action action) {
            message.AddAction(answer, action);
            return this;
        }

        public IOutputMessage GetMessage() {
            return message;
        }
    }
}
