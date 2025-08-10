// (c) 2018 Roland Boon

using BoonOrg.Identification;
using BoonOrg.Functions;
using BoonOrg.Functions.Domain;
using BoonOrg.Commands;

namespace BoonOrg.Storage.Domain.Operations
{
    internal sealed class OperationReadDocument : Operation, IExecuteWithCommandExecuter
    {
        private readonly IDocumentStorage m_documentStorage;
        private readonly IDocumentServer m_documentServer;

        public OperationReadDocument(IParameterCollection parameters,
            IDocumentStorage documentStorage,
            IDocumentServer documentServer) : base(parameters)
        {
            m_documentStorage = documentStorage;
            m_documentServer = documentServer;
        }

        public bool Execute(ICommandExecuter commandExecuter)
        {
            if (!Parameters.GetString(@"path", out string path) ||
                !m_documentStorage.DocumentExists(path))
            {
                return false;
            }

            var document2 = m_documentStorage.Read(path);
            var document = m_documentServer.Document;
            foreach (var typeIdentifier in document2.AllTypeIdentifiers)
            {
                var source = document2.Get(typeIdentifier);
                var target = document.Get(typeIdentifier);
                if (target == null)
                {
                    document.Add(source);
                    continue;
                }

                if (target is IContainerHandover containerHandover)
                {
                    containerHandover.Handover(source);
                    continue;
                }

                document.Add(source); // replaces target
            }
            document2.Close();

            return true;
        }
    }
}
