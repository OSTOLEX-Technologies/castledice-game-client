using System;
using evm.net.MetaMask.Plugins.Libraries.evm.net.Runtime.Models.ABI;

namespace evm.net.MetaMask.Plugins.Libraries.evm.net.Runtime.Generator
{
    public class TupleTypeGenerator : CodeGenerator
    {
        private ABIParameter _parameter;
        private string _internalType;

        public TupleTypeGenerator(ABIParameter parameter, GeneratorContext context, string internalType)
        {
            _context = context;
            _parameter = parameter;
            _internalType = internalType;
        }
        
        public void WriteAutoGeneratedComment()
        {
            WriteLine("//------------------------------------------------------------------------------");
            WriteLine("// This code was generated by a tool.");
            WriteLine("//");
            WriteLine("//   Tool : MetaMask Unity SDK ABI Code Generator");
            WriteLine($"//   Input filename:  {_context.ContractName}.sol");
            WriteLine($"//   Output filename: {Filename}.cs");
            WriteLine("//");
            WriteLine("// Changes to this file may cause incorrect behavior and will be lost when");
            WriteLine("// the code is regenerated.");
            WriteLine("// <auto-generated />");
            WriteLine("//------------------------------------------------------------------------------");
            WriteLine();
        }

        public void WriteUsings()
        {
            WriteLine("using System;");
            WriteLine("using System.Numerics;");
            WriteLine("using System.Threading.Tasks;");
            WriteLine("using evm.net;");
            WriteLine("using evm.net.Models;");
            WriteLine();
        }

        public void WriteNamespace()
        {
            WriteLine($"namespace {_context.RootNamespace}");
            StartCodeBlock();
        }

        public void WriteClassName()
        {
            var typedName = _internalType;
            WriteLine($"public class {_internalType}");
            StartCodeBlock();
        }

        public void WriteComponents()
        {
            foreach (var component in _parameter.Components)
            {
                WriteComponent(component);
                WriteLine();
            }
        }

        public void WriteComponent(ABIParameter component)
        {
            var literalName = component.Name;
            var parmName = literalName;
            if (!IsValidIdentifier(parmName))
                parmName = $"@{parmName}";

            string typeName;
            if (ParameterConverter.StrictEvmToType.ContainsKey(component.TypeName))
            {
                var t = ParameterConverter.StrictEvmToType[component.TypeName];
                typeName = t.Name;
            }
            else if (ParameterConverter.DynamicEvmToType.ContainsKey(component.TypeName))
            {
                var t = ParameterConverter.DynamicEvmToType[component.TypeName];
                typeName = t.Name;
            }
            else if (!string.IsNullOrWhiteSpace(component.InternalType))
            {
                typeName = component.InternalType;
                _context.GenerateTuple(typeName, component);
            }
            else
            {
                throw new Exception("Not a valid type " + component.TypeName);
            }
            
            WriteLine($"[EvmParameterInfo(Name = \"{literalName}\", Type = \"{component.TypeName}\")]");
            WriteLine($"public {typeName} {parmName};");
        }

        protected override void DoWrite()
        {
            WriteAutoGeneratedComment();
            WriteUsings();
            WriteNamespace();
            WriteClassName();
            WriteComponents();
            CompleteCodeBlocks();
        }

        public override string Filename
        {
            get
            {
                return _internalType;
            }
        }
    }
}