using System;
using System.Collections;
using System.IO;
using System.Threading;
using Opc.Ua;
using Opc.Ua.Client;
using Org.BouncyCastle.Crypto.Agreement;

namespace OpenUAInterface
{
    public class ClientSample
    {

        const int MaxSearchDepth = 128;

        private TextWriter m_output;
        private Action<IList, IList> m_validateResponse;
        private ManualResetEvent m_quitEvent;
        private bool m_verbose;
        public ClientSample(TextWriter output, Action<IList,IList> validateResponse , ManualResetEvent  quitEvent = null , bool verbose = false)
        {
            m_output = output;
            m_validateResponse = validateResponse;
            m_verbose = verbose;
            m_quitEvent = quitEvent;
        }

        public void ReadNodes(ISession session)
        {
            if (session == null || session.Connected == false )
            {
                m_output.WriteLine("Session not connected!");
                return;
            }
            try
            {
                ReadValueIdCollection nodesToRead = new ReadValueIdCollection()
                {
                    new ReadValueId(){ NodeId  = Variables.Server_ServerStatus , AttributeId = Attributes.Value},

                    new ReadValueId(){NodeId  = Variables.Server_ServerStatus_StartTime , AttributeId = Attributes.BrowseName},

                    new ReadValueId(){NodeId  = Variables.Server_ServerStatus_StartTime , AttributeId = Attributes.Value}
                };
                m_output.WriteLine("Reading nodes...");

                session.Read(
                                null,
                                0,
                                TimestampsToReturn.Both,
                                nodesToRead,
                                out DataValueCollection resultValues,
                                out DiagnosticInfoCollection diagnosticInfos
                            );
                m_validateResponse(resultValues, nodesToRead);

                foreach (DataValue result in resultValues)
                {
                    m_output.WriteLine("Read Value = {0} , Status Code = {1}", result.Value, result.StatusCode);
                }

                m_output.WriteLine("Reading Value of NamespaceArray node...");
                DataValue namespaceArray = session.ReadValue(Variables.Server_NamespaceArray);

                m_output.WriteLine($"NamespaceArray Value = {namespaceArray}");
            }
            catch ( Exception e )
            {
                m_output.WriteLine($"Error reading nodes: {e.Message}");
            }


        }







        
    }
}
