using System;
using System.IO;
using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Xenial.Framework.SourceGenerators
{
    [Generator]
    public sealed class ViewIdGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            AddGenerateViewIdsAttribute(context);
        }

        public void Initialize(GeneratorInitializationContext context) { }

        private static void AddGenerateViewIdsAttribute(GeneratorExecutionContext context)
        {
            var syntaxWriter = new CurlyIndenter(new System.CodeDom.Compiler.IndentedTextWriter(new StringWriter()));
            syntaxWriter.WriteLine("using System;");
            syntaxWriter.WriteLine("using System.Runtime.CompilerServices;");
            syntaxWriter.WriteLine();
            syntaxWriter.WriteLine("namespace Xenial");
            syntaxWriter.OpenBrace();
            syntaxWriter.WriteLine("[AttributeUsage(AttributeTargets.Class)]");
            syntaxWriter.WriteLine("[CompilerGenerated]");
            syntaxWriter.WriteLine("internal class GenerateViewIdsAttribute : Attribute");
            syntaxWriter.OpenBrace();
            syntaxWriter.WriteLine("public GenerateViewIdsAttribute() { }");

            syntaxWriter.CloseBrace();
            syntaxWriter.CloseBrace();

            var syntax = syntaxWriter.ToString();
            var source = SourceText.From(syntax, Encoding.UTF8);
            context.AddSource("XenialGenerateViewIdsAttribute.g.cs", source);
        }
    }
}
