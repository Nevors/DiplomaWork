using System;
using System.Collections.Generic;
using System.Text;
using Minifying.Abstract;
using Minifying.External.Models;

namespace Minifying.Concrete.Common {
    class BaseOutputMessage : IOutputMessage {
        Dictionary<AnswerType, Action> actions = new Dictionary<AnswerType, Action>();

        public BaseOutputMessage(string message) {
            Message = message;
        }
        public string Message { get; }

        public IEnumerable<AnswerType> Actions => actions.Keys;

        public void AddAction(AnswerType answer, Action action) {
            actions.Add(answer, action);
        }

        public void ExecuteAction(AnswerType answer) {
            actions[answer]?.Invoke();
        }
    }
}
