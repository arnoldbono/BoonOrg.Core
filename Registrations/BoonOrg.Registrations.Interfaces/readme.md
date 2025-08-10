The BoonOrg.Registrations.Interfaces package contains a Autofac wrapper to register interfaces.

To resolve an instance of a given interface, IInterface, say use Resolver.Resolve<IInterface>().

TODO
The Resolver class is part of the BoonOrg.Registrations.Domain package.
I would like a way to get the Resolver instance without a need to expose Resolver.
To do that, I need a factory class following method.

	static IResolver Create();
