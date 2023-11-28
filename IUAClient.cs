using System;
using System.Collections.Generic;
using System.Text;
using Opc.Ua;
using Opc.Ua.Client;

namespace OpenUAInterface
{
    public interface IUAClient
    {
        ISession Session { get; }
    }
}
