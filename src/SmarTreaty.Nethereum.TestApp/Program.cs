using System;
using System.Collections.Generic;
using SolcNet;
using SolcNet.DataDescription.Input;

namespace SmarTreaty.Nethereum.TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = @"D:\7term\TOFI\SmarTreaty\src\SmarTreaty.Nethereum.TestApp\bin\Debug\netcoreapp3.0\native\win-x86\solc.dll";
            //var path = LibPath.GetLibPath(SolcVersion.v0_4_25);
            var compiler = new SolcLib(path);

            var input = new InputDescription
            {
                Language = Language.Solidity,
                Sources = new System.Collections.Generic.Dictionary<string, Source>
                {
                    { "test.sol", new Source(){ Content = "pragma solidity ^0.4.25; contract C {function f() public {}}" } }
                },
                Settings = new Settings
                {
                    EvmVersion = EvmVersion.Byzantium,
                    OutputSelection = new Dictionary<string, Dictionary<string, OutputType[]>>
                    {
                        { "*", new Dictionary<string, OutputType[]>{ { "*", new[] { OutputType.Abi, OutputType.EvmBytecodeObject, OutputType.EvmBytecodeSourceMap, OutputType.EvmDeployedBytecodeObject, OutputType.EvmDeployedBytecodeSourceMap } } } }
                    }
                }
                
            };


            var output = compiler.Compile(input);


            Console.WriteLine(output.RawJsonOutput);
        }
    }
}
