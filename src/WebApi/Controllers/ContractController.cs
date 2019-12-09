using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using SolcNet;
using SolcNet.DataDescription.Input;

namespace SmarTreatyCompilation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContractController : ControllerBase
    {
        [HttpGet]
        public JsonResult Compile(string source)
        {
            var compiler = new SolcLib();

            var input = new InputDescription
            {
                Language = Language.Solidity,
                Settings = new Settings
                {
                    OutputSelection = new Dictionary<string, Dictionary<string, OutputType[]>>
                    {
                        { "*", new Dictionary<string, OutputType[]> {
                            {
                                "*",
                                new[]
                                {
                                    OutputType.Abi,
                                    OutputType.EvmBytecodeObject
                                }
                            }
                        }
                        }
                    },
                    EvmVersion = EvmVersion.Byzantium,
                    Optimizer = new Optimizer
                    {
                        Enabled = false,
                        Runs = 200
                    }
                },
                Sources = new Dictionary<string, Source>
                {
                    { "comiled.sol", new Source { Content = source } }
                }
            };

            var output = compiler.Compile(input);
            var regex = new Regex("contract[ ]*.*_xtemplate");
            var name = regex.Match(source).Value.Split(" ")[1];

            var result = new object[]
            {
                output.Contracts["comiled.sol"][name].Abi,
                output.Contracts["comiled.sol"][name].Evm.Bytecode.Object
            };

            return new JsonResult(result);
        }
    }
}

