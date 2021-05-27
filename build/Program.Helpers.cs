using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

using static SimpleExec.Command;

namespace Xenial.Build
{
    internal static partial class Program
    {
        private static (string fullFramework, string netcore, string net5, string winVersion) FindTfms()
        {
            var dirProps = XElement.Load("Directory.Build.props");
            var props = dirProps.Descendants("PropertyGroup");
            var fullFramework = props.Descendants("FullFrameworkVersion").First().Value;
            var netcore = props.Descendants("NetCoreVersion").First().Value;
            var net5 = props.Descendants("Net5Version").First().Value;
            var winVersion = props.Descendants("WindowsFrameworkVersion").First().Value;
            return (fullFramework, netcore, net5, winVersion);
        }

        private static async Task EnsureTools()
        {
            try
            {
                await RunAsync("dotnet", "format --version");
            }
            catch (SimpleExec.NonZeroExitCodeException)
            {
                //Can't find dotnet format, assuming tools are not installed
                await RunAsync("dotnet", "tool restore");
            }
        }

        private static async Task<string> ReadToolAsync(Func<Task<string>> action)
        {
            try
            {
                return await action();
            }
            catch (SimpleExec.NonZeroExitCodeException)
            {
                Console.WriteLine("Tool seams missing. Try to restore");
                await RunAsync("dotnet", "tool restore");
                return await action();
            }
        }

        private static string Tabify(string s)
            => string.IsNullOrEmpty(s)
                ? string.Empty
                : string.Join(
                    Environment.NewLine,
                    s.Split("\n")
                        .Select(s => $"\t{s}")
                );
    }
}
