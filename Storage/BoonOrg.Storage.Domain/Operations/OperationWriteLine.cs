// (c) 2017, 2023 Roland Boon

using BoonOrg.Identification;
using BoonOrg.Functions;
using BoonOrg.Functions.Domain;

namespace BoonOrg.Storage.Domain.Operations
{
    internal sealed class OperationWriteLine : Operation
    {
        private readonly IIdentifiableStreamWriterService m_streamWriterService;

        public OperationWriteLine(IParameterCollection parameters,
            IIdentifiableStreamWriterService streamWriterService) : base(parameters)
        {
            m_streamWriterService = streamWriterService;
        }

        public override bool Execute(IIdentifiableContainer container)
        {
            if (!Parameters.GetString(@"file", out var name) ||
                !Parameters.GetString(@"text", out string text))
            {
                return false;
            }

            if (text.Length >= 2 && text.StartsWith(@"""") && text.EndsWith(@""""))
            {
                text = text.Substring(1, text.Length - 2).Trim();
            }

            for (int i = 0; i < 10; ++i)
            {
                string format = $@"{{{i}}}";
                if (!text.Contains(format))
                {
                    break;
                }
                string parameter = $@"parameter{i}";
                if (Parameters.Get(parameter, container, out int value))
                {
                    text = text.Replace(format, value.ToString());
                }
                else
                {
                    return false;
                }
            }

            m_streamWriterService.WriteLine(name, text, container);

            return true;
        }
    }
}
