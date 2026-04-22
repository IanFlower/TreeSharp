using System;
using System.Collections.Generic;
using System.Linq;

enum SunLevel
{
    Full,
    Partial,
    Indirect
}

class Plant
{
    public string Name;
    public int IntValue;
    public string StringValue = "";
    public bool IsString;
    public SunLevel SunLevel;

    public Plant(string name, SunLevel sun)
    {
        Name = name;
        SunLevel = sun;
        IntValue = 0;
    }
}

class Interpreter
{
    private readonly Dictionary<string, Plant> memory = new();
    private readonly List<string> program;
    private int pc = 0;

    public Interpreter(List<string> lines)
    {
        program = lines;
    }

    public void Run()
    {
        while (pc >= 0 && pc < program.Count)
        {
            Execute(program[pc++]);
        }
    }

    private void Execute(string line)
    {
        if (string.IsNullOrWhiteSpace(line)) return;

        var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var cmd = parts[0];

        switch (cmd)
        {
            case "Plant":
                {
                    string name = parts[1];
                    SunLevel sun = Enum.Parse<SunLevel>(parts[2], true);
                    memory[name] = new Plant(name, sun);
                    break;
                }

            case "Water":
                {
                    var p = Get(parts[1]);
                    p.IntValue += p.SunLevel switch
                    {
                        SunLevel.Full => 3,
                        SunLevel.Partial => 2,
                        SunLevel.Indirect => 1,
                        _ => 0
                    };
                    break;
                }

            case "Prune":
                {
                    var p = Get(parts[1]);
                    p.IntValue -= p.SunLevel switch
                    {
                        SunLevel.Full => 1,
                        SunLevel.Partial => 2,
                        SunLevel.Indirect => 3,
                        _ => 0
                    };
                    break;
                }

            case "Chop":
                {
                    var p = Get(parts[1]);
                    p.IntValue = 0;
                    p.StringValue = "";
                    break;
                }

            case "Uproot":
                {
                    memory.Remove(parts[1]);
                    break;
                }

            case "Graft":
                {
                    var a = Get(parts[1]);
                    var b = Get(parts[2]);
                    if (b.IsString)
                    {
                        a.StringValue += b.StringValue;
                        a.IsString = true;
                    }
                    else
                    {
                        a.IntValue += b.IntValue;
                    }
                    break;
                }

            case "MakePaper":
                {
                    var p = Get(parts[1]);
                    Console.Write((char)p.IntValue);
                    break;
                }

            case "MakeMathPaper":
                {
                    var p = Get(parts[1]);
                    Console.Write(p.IntValue);
                    break;
                }

            case "Inscribe":
                {
                    var p = Get(parts[1]);
                    Console.Write("> ");
                    string input = Console.ReadLine() ?? "";
                    if (int.TryParse(input, out int result))
                    {
                        p.IntValue = result;
                        p.IsString = false;
                    }
                    else
                    {
                        p.StringValue = input;
                        p.IsString = true;
                    }
                    break;
                }

            case "Peel":
                {
                    var src = Get(parts[1]);
                    var dst = Get(parts[2]);

                    if (!src.IsString || src.StringValue.Length == 0)
                    {
                        dst.IntValue = -1;
                    }
                    else
                    {
                        dst.IntValue = src.StringValue[0];
                        src.StringValue = src.StringValue[1..];
                    }
                    break;
                }

            case "Measure":
                {
                    var src = Get(parts[1]);
                    var dst = Get(parts[2]);
                    dst.IntValue = src.StringValue?.Length ?? 0;
                    break;
                }

            case "Branch":
                {
                    var a = Get(parts[1]);
                    var b = Get(parts[2]);
                    var r = Get(parts[3]);

                    r.IntValue =
                        a.IntValue > b.IntValue ? 1 :
                        a.IntValue < b.IntValue ? -1 : 0;

                    break;
                }

            case "Jump":
                {
                    var cond = Get(parts[1]);

                    var jumpIf1 = Get(parts[2]).IntValue;
                    var jumpIf0 = parts.Length > 3 ? Get(parts[3]).IntValue : pc;
                    var jumpIfNeg1 = parts.Length > 4 ? Get(parts[4]).IntValue : pc;

                    int target = cond.IntValue switch
                    {
                        1 => jumpIf1,
                        0 => jumpIf0,
                        -1 => jumpIfNeg1,
                        _ => pc
                    };

                    pc = target - 1; // adjust for pc++
                    break;
                }

            default:
                throw new Exception($"Unknown instruction: {cmd}");
        }
    }

    private Plant Get(string name)
    {
        if (!memory.ContainsKey(name))
                throw new Exception($"Plant not found: {name}");

        return memory[name];
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("TreeSharp file path:");
        var path = Console.ReadLine();

        var lines = System.IO.File.ReadAllLines(path!)
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .ToList();

        var interpreter = new Interpreter(lines);
        interpreter.Run();
    }
}
