// (c) 2017, 2023 Roland Boon

using System;

using BoonOrg.Identification;
using BoonOrg.Functions;
using BoonOrg.Functions.Domain;

namespace BoonOrg.Storage.Domain.Operations
{
    internal sealed class OperationAddFile : Operation
    {
        private readonly IIdentifiableStreamWriterService m_streamWriterService;
        public OperationAddFile(IParameterCollection parameters,
            IIdentifiableStreamWriterService streamWriterService) : base(parameters)
        {
            m_streamWriterService = streamWriterService;
        }

        public override bool Execute(IIdentifiableContainer container)
        {
            if (!Parameters.GetString(@"file", out string name) ||
                !Parameters.GetString(@"path", out string path))
            {
                return false;
            }

            m_streamWriterService.Add(name, path, container);

            return true;
        }
    }
}
