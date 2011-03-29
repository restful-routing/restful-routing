using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RestfulRouting
{
    public class AdditionalAction
    {
        private IDictionary<string, HttpVerbs[]> _actions;

        public AdditionalAction(IDictionary<string, HttpVerbs[]> actions)
        {
            _actions = actions;
        }

        public void Get(string action)
        {
            Route(action, HttpVerbs.Get);
        }

        public void Post(string action)
        {
            Route(action, HttpVerbs.Post);
        }

        public void Put(string action)
        {
            Route(action, HttpVerbs.Put);
        }

        public void Delete(string action)
        {
            Route(action, HttpVerbs.Delete);
        }

        public void Head(string action)
        {
            Route(action, HttpVerbs.Head);
        }

        private void Route(string action, HttpVerbs verb)
        {
            var actionName = action.ToLowerInvariant();
            if (!_actions.ContainsKey(action))
                _actions[actionName] = new[] { verb };
            else
            {
                var verbs = _actions[actionName].ToList();
                verbs.Add(verb);
                _actions[actionName] = verbs.ToArray();
            }
        }
    }
}
