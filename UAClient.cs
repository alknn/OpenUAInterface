using Opc.Ua.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenUAInterface
{
    public class UAClient : IUAClient , IDisposable
    {














        public ISession Session => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
