using System;
using System.CodeDom.Compiler;

namespace Xenial.Framework.SourceGenerators
{
    internal class CurlyIndenter
    {
        private readonly IndentedTextWriter indentedTextWriter;

        public CurlyIndenter(IndentedTextWriter indentedTextWriter)
            => this.indentedTextWriter = indentedTextWriter;

        public void Write(string val) => indentedTextWriter.Write(val);
        public void WriteLine(string val) => indentedTextWriter.WriteLine(val);
        public void WriteLine() => indentedTextWriter.WriteLine();
        public void Indent() => indentedTextWriter.Indent++;
        public void UnIndent() => indentedTextWriter.Indent--;

        public void OpenBrace()
        {
            WriteLine("{");
            Indent();
        }

        public void CloseBrace()
        {
            UnIndent();
            WriteLine("}");
        }

        public override string ToString() => indentedTextWriter.InnerWriter.ToString();
    }
}
