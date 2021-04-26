using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

using Shouldly;

using Xenial.Framework.SourceGenerators;

using static Xenial.Tasty;

namespace Xenial.Framework.Tests.Generators
{
    public static class SourceGeneratorFacts
    {
        public static void SourceGeneratorTests() => FDescribe("SourceGenerators", () =>
        {
            static Compilation CreateCompilation(string source)
                => CSharpCompilation.Create("compilation",
                   new[] { CSharpSyntaxTree.ParseText(source) },
                   new[] { MetadataReference.CreateFromFile(typeof(Binder).GetTypeInfo().Assembly.Location) },
                   new CSharpCompilationOptions(OutputKind.ConsoleApplication));

            It("Should load and execute source generator without errors", () =>
            {
                // Create the 'input' compilation that the generator will act on
                var inputCompilation = CreateCompilation(@"
namespace MyCode
{
    public class Program
    {
        public static void Main(string[] args)
        {
        }
    }
}
");

                // directly create an instance of the generator
                // (Note: in the compiler this is loaded from an assembly, and created via reflection at runtime)
                var generator = new ViewIdGenerator();

                // Create the driver that will control the generation, passing in our generator
                GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

                // Run the generation pass
                // (Note: the generator driver itself is immutable, and all calls return an updated version of the driver that you should use for subsequent calls)
                driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out var diagnostics);

                // We can now assert things about the resulting compilation:
                diagnostics.IsEmpty.ShouldBeTrue("there were no diagnostics created by the generators");
                outputCompilation.SyntaxTrees.Count().ShouldBe(2, "we have two syntax trees, the original 'user' provided one, and the one added by the generator");
                outputCompilation.GetDiagnostics().IsEmpty.ShouldBeTrue("verify the compilation with the added source has no diagnostics");

                // Or we can look at the results directly:
                var runResult = driver.GetRunResult();

                // The runResult contains the combined results of all generators passed to the driver
                runResult.GeneratedTrees.Length.ShouldBe(1, "The runResult contains the combined results of all generators passed to the driver");
                runResult.Diagnostics.IsEmpty.ShouldBeTrue("The runResult contains the combined results of all generators passed to the driver");

                // Or you can var the individual results on a by-generator basis
                var generatorResult = runResult.Results[0];
                generatorResult.Generator.ShouldBe(generator);

                generatorResult.Diagnostics.IsEmpty.ShouldBeTrue();
                generatorResult.GeneratedSources.Length.ShouldBe(1);
                generatorResult.Exception.ShouldBeNull();
            });
        });
    }
}
