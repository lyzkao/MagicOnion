﻿using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MagicOnion.Server
{
    public class ServiceContext
    {
        Dictionary<string, object> items;

        /// <summary>Object storage per invoke.</summary>
        public IDictionary<string, object> Items
        {
            get
            {
                if (items == null) items = new Dictionary<string, object>();
                return items;
            }
        }

        public Type ServiceType { get; private set; }

        public MethodInfo MethodInfo { get; private set; }

        /// <summary>Cached Attributes both service and method.</summary>
        public ILookup<Type, Attribute> AttributeLookup { get; private set; }

        public MethodType MethodType { get; private set; }

        /// <summary>Raw gRPC Context.</summary>
        public ServerCallContext CallContext { get; private set; }

        // internal, used from there methods.
        internal object RequestMarshaller { get; set; }
        internal object ResponseMarshaller { get; set; }
        internal IAsyncStreamReader<byte[]> RequestStream { get; set; }
        internal IAsyncStreamWriter<byte[]> ResponseStream { get; set; }
        internal byte[] Result { get; set; }

        public ServiceContext(Type serviceType, MethodInfo methodInfo, ILookup<Type, Attribute> attributeLookup, MethodType methodType, ServerCallContext context)
        {
            this.ServiceType = serviceType;
            this.MethodInfo = methodInfo;
            this.AttributeLookup = attributeLookup;
            this.MethodType = methodType;
            this.CallContext = context;
        }
    }
}