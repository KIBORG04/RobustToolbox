﻿using Robust.Shared.Serialization.Manager;

namespace Robust.Shared.Serialization.TypeSerializers.Interfaces;

public interface ITypeCopier<TType> : BaseSerializerInterfaces.ITypeInterface<TType>
{
    void CopyTo(ISerializationManager serializationManager, TType source, ref TType target,
        bool skipHook,
        ISerializationContext? context = null);

}
