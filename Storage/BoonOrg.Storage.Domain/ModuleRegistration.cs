// (c) 2017, 2018, 2019, 2023 Roland Boon

using BoonOrg.Identification;
using BoonOrg.Registrations;
using BoonOrg.Functions;

using BoonOrg.Storage.Domain.Operations;
using BoonOrg.Storage.Domain.Creators;

namespace BoonOrg.Storage.Domain
{
    public static class ModuleRegistration
    {
        public static void Register(ITypeRegistrar registrar)
        {
            registrar.RegisterTypeAsSingleton<DocumentContainer, IDocumentContainer>();
            registrar.RegisterTypeAsSingleton<StorageRegistration, IStorageRegistration>();
            registrar.RegisterTypeAsSingleton<StorageIdentifier, IStorageIdentifier>();
            registrar.RegisterTypeAsSingleton<DocumentServer, IDocumentServer>();

            registrar.RegisterType<Document, IDocument>();
            registrar.RegisterType<DocumentStorage, IDocumentStorage>();
            registrar.RegisterType<IdentifiableStreamWriter, IIdentifiableStreamWriter>();

            registrar.RegisterType<IdentifiableContainerStorageProvider, IStorageProvider<IIdentifiableContainer>>();
            registrar.RegisterType<ParameterStorageProvider, IStorageProvider<IParameter>>();

            registrar.RegisterType<RootGroupCreator, IInitialDocumentContentCreator>();
            registrar.RegisterType<FilePathCleanup, IFilePathCleanup>();

            registrar.RegisterType<OperationReadDocument>();
            registrar.RegisterType<OperationWriteDocument>();
            registrar.RegisterType<OperationAddFile>();
            registrar.RegisterType<OperationCloseFile>();
            registrar.RegisterType<OperationWriteLine>();
        }

        public static void FunctionRegistration(IFunctionCallRegistrar registrar)
        {
            registrar.Add<OperationReadDocument>(@"ReadDocument");
            registrar.Add<OperationWriteDocument>(@"WriteDocument");
            registrar.Add<OperationAddFile>(@"AddFile");
            registrar.Add<OperationCloseFile>("CloseFile");
            registrar.Add<OperationWriteLine>("WriteLine");
        }

        public static void StorageRegistration(IStorageRegistration storageRegistration)
        {
            storageRegistration.Register<IIdentifiableContainer>();
            storageRegistration.Register<IParameter>();
        }
    }
}
