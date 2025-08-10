// (c) 2018 Roland Boon

using BoonOrg.Functions;
using BoonOrg.Functions.Domain;
using BoonOrg.Commands;

namespace BoonOrg.Storage.Domain.Operations
{
    internal sealed class OperationWriteDocument : Operation, IExecuteWithCommandExecuter
    {
        private readonly IDocumentStorage m_documentStorage;
        private readonly IDocumentServer m_documentServer;

        public OperationWriteDocument(IParameterCollection parameters,
            IDocumentStorage documentStorage,
            IDocumentServer documentServer) : base(parameters)
        {
            m_documentStorage = documentStorage;
            m_documentServer = documentServer;
        }

        public bool Execute(ICommandExecuter commandExecuter)
        {
            var document = m_documentServer.Document;

            if (!Parameters.GetString(@"path", out string path))
            {
                return false;
            }

            m_documentStorage.Write(document, path);          
            return true;
        }
    }
}
