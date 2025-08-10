// (c) 2017, 2023 Roland Boon

using BoonOrg.Identification;
using BoonOrg.Functions;
using BoonOrg.Functions.Domain;

namespace BoonOrg.Storage.Domain.Operations
{
    internal sealed class OperationCloseFile : Operation
    {
        private readonly IIdentifiableStreamWriterService m_streamWriterService;

        public OperationCloseFile(IParameterCollection parameters,
            IIdentifiableStreamWriterService streamWriterService) : base(parameters)
        {
            m_streamWriterService = streamWriterService;
        }

        public override bool Execute(IIdentifiableContainer container)
        {
            if (!Parameters.GetString(@"file", out var name))
            {
                return false;
            }

            m_streamWriterService.Close(name, container);

            return true;
        }
    }
}
