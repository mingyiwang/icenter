﻿using System;
using System.Reflection;
using Common.Collection;

namespace Core.Template
{

    public interface ITemplateContext : IDisposable
    {

        object GetObject(string key);
        object GetObjectIfNotExist(string key);
        ITemplateContext WithObject(string key, object obj);

        Type GetInstance(string key);
        ITemplateContext WithInstance(string key, Type obj);

        ITemplateContext WithParser(ITemplateParser parser);
        ITemplateParser GetParser();

    }

    public sealed class TemplateContext : ITemplateContext
    {
        private readonly HashMap<string, object> _objects = new HashMap<string, object>();
        private readonly HashMap<string, Type> _instances = new HashMap<string, Type>();
        private readonly HashMap<string, Assembly> _references = new HashMap<string, Assembly>();
        private ITemplateParser _parser;

        private TemplateContext(){}

        public ITemplateParser GetParser()
        {
            return this._parser;
        }

        public ITemplateContext WithParser(ITemplateParser parser)
        {
            this._parser = parser;
            return this;
        }

        public static TemplateContext NewInstance()
        {
            return new TemplateContext();
        }

        public object GetObject(string key)
        {
            return _objects.Get(key);
        }

        public object GetObjectIfNotExist(string key)
        {
            throw new NotImplementedException();
        }

        public ITemplateContext WithObject(string key, object obj)
        {
            _objects.Put(key, obj);
            return this;
        }

        public Type GetInstance(string key)
        {
            return _instances.Get(key);
        }

        public ITemplateContext WithInstance(string key, Type instance)
        {
            _instances.Put(key, instance);
            return this;
        }
       
        public void Dispose()
        {
           _objects.Clear();
           _instances.Clear();
        }

    }

}