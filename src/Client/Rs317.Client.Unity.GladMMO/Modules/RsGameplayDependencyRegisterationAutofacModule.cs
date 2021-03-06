﻿using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Rs317.GladMMO;
using Rs317.Sharp;
using UnityEngine;

namespace GladMMO
{
	public sealed class RsGameplayDependencyRegisterationAutofacModule : Module
	{
		public RsGameplayDependencyRegisterationAutofacModule()
		{

		}

		/// <inheritdoc />
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterModule<ZoneServerServiceDependencyAutofacModule>();

			builder.RegisterType<UtcNowNetworkTimeService>()
				.As<INetworkTimeService>()
				.As<IReadonlyNetworkTimeService>()
				.SingleInstance();

			//This service is required by the entity data change system/tickable
			builder.RegisterType<EntityDataChangeCallbackManager>()
				.AsImplementedInterfaces()
				.SingleInstance();

			builder.RegisterType<DefaultThreadUnSafeKnownEntitySet>()
				.AsSelf()
				.AsImplementedInterfaces()
				.SingleInstance();

			builder.RegisterType<NetworkVisibilityCreationBlockToVisibilityEventFactory>()
				.As<IFactoryCreatable<NetworkEntityNowVisibleEventArgs, EntityCreationData>>()
				.SingleInstance();

			//DefaultEntityVisibilityEventPublisher : INetworkEntityVisibilityEventPublisher, INetworkEntityVisibleEventSubscribable
			builder.RegisterType<DefaultEntityVisibilityEventPublisher>()
				.AsSelf()
				.AsImplementedInterfaces()
				.SingleInstance();

			//DefaultMovementGeneratorFactory: IFactoryCreatable<IMovementGenerator<IWorldObject>, EntityAssociatedData<IMovementData>>
			builder.RegisterType<DefaultMovementGeneratorFactory>()
				.As<IFactoryCreatable<IMovementGenerator<IWorldObject>, EntityAssociatedData<IMovementData>>>();

			//DefaultMovementGeneratorFactory : IFactoryCreatable<IMovementGenerator<GameObject>, EntityAssociatedData<IMovementData>>
			/*builder.RegisterType<ClientMovementGeneratorFactory>()
				.As<IFactoryCreatable<IMovementGenerator<GameObject>, EntityAssociatedData<IMovementData>>>()
				.SingleInstance();*/

			//Ok, now we actually register update block types manually
			//because it's not worth it to do an assembly-wide search for them.
			/*builder.RegisterType<DefaultObjectUpdateBlockDispatcher>()
				.AsSelf()
				.AsImplementedInterfaces()
				.SingleInstance();

			RegisterUpdateBlockHandler<ObjectUpdateCreateObject1BlockHandler>(builder);
			RegisterUpdateBlockHandler<ObjectUpdateValuesObjectBlockHandler>(builder);

			//Stub out all unused ones
			builder.RegisterInstance(new StubbedObjectUpdateBlockHandler(ObjectUpdateType.UPDATETYPE_CREATE_OBJECT2)).As<IObjectUpdateBlockHandler>();
			builder.RegisterInstance(new StubbedObjectUpdateBlockHandler(ObjectUpdateType.UPDATETYPE_MOVEMENT)).As<IObjectUpdateBlockHandler>();
			builder.RegisterInstance(new StubbedObjectUpdateBlockHandler(ObjectUpdateType.UPDATETYPE_NEAR_OBJECTS)).As<IObjectUpdateBlockHandler>();
			builder.RegisterInstance(new StubbedObjectUpdateBlockHandler(ObjectUpdateType.UPDATETYPE_OUT_OF_RANGE_OBJECTS)).As<IObjectUpdateBlockHandler>();*/
		}

		/*private static void RegisterUpdateBlockHandler<THandlerType>([NotNull] ContainerBuilder builder)
			where THandlerType : IObjectUpdateBlockHandler
		{
			if(builder == null) throw new ArgumentNullException(nameof(builder));

			builder.RegisterType<THandlerType>()
				.AsImplementedInterfaces()
				.SingleInstance();
		}*/
	}
}