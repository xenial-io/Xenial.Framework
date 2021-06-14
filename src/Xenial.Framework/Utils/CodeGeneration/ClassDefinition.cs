using System;
using System.Collections.Generic;
using System.Text;

namespace Xenial.Framework.Utils.CodeGeneration
{
    internal interface ICSharpSyntax
    {
        string ToCSharp();
    }

    internal record AttributeDefinition(string Name) : ICSharpSyntax
    {
        public string ToCSharp() => $"[{Name}]";
    }

    internal record ClassDefinition(string Name)
    {

    }

    internal record SyntaxDefinition(SyntaxCollection SyntaxDefinitions) : ICSharpSyntax
    {
        public string ToCSharp() => SyntaxDefinitions.ToCSharp();
    }

    internal enum CodeVisibility
    {
        Private,
        Protected,
        Internal,
        Public
    }

    internal record MethodDefinition(
        CodeVisibility Visibility,
        string Name,
        TypeDefinition ReturnType
    ) : ICSharpSyntax
    {
        public bool IsStatic { get; init; }
        public bool IsAsync { get; init; }

        public static MethodDefinition Void(CodeVisibility visiblity, string name)
            => new MethodDefinition(visiblity, name, TypeDefinition.Void);

        public string ToCSharp()
            => $"{Visibility} {(IsStatic ? "static" : "")} {(IsAsync ? "async" : "")} {ReturnType} {Name}";
    }

    internal record TypeDefinition(string Name) : ICSharpSyntax
    {
        public static TypeDefinition Void { get; } = new TypeDefinition("void");
        public static TypeDefinition Task { get; } = new TypeDefinition("System.Threading.Task");

        public string ToCSharp() => Name;
    }

    internal class SyntaxCollection : List<ICSharpSyntax>, ICSharpSyntax
    {
        public string ToCSharp()
        {
            var sb = new StringBuilder();
            foreach (var node in this)
            {
                sb.AppendLine(node.ToCSharp());
            }
            return sb.ToString();
        }
    }

    internal record UsingDefinition(string Using) : ICSharpSyntax
    {
        public bool IsStatic { get; init; }
        public string ToCSharp() => $"using {(IsStatic ? "static " : "")}{Using};";
    }

    internal record NamespaceDefinition(
        string Name,
        SyntaxCollection SyntaxCollection
    ) : ICSharpSyntax
    {
        public string ToCSharp() => $@"namespace {Name}
{{
    {SyntaxCollection.ToCSharp()}
}}";
    }

    internal record CodeObjectCreateDefinition(
        TypeDefinition TypeDefinition
    ) : ICSharpSyntax
    {
        public string ToCSharp()
            => $"new {TypeDefinition}";
    }

    internal class LambdaExpressionDefinition
    {

    }
}
