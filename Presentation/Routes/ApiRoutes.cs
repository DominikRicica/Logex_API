using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Routes
{
    public static class ApiRoutes
    {
        public const string Root = "api";

        public const string Version = "v1";

        public const string Base = Root + "/" + Version;

        public static class Establishment
        {
            public const string GetAll = Base + "/establishments";

            public const string Get = Base + "/establishments/{Id}";
        }

        public static class Event
        {
            public const string GetAll = Base + "/events";

            public const string Get = Base + "/events/{Id}";
        }
    }
}
