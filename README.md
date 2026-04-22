# TreeSharp Language Reference

| Instruction | Syntax | Description |
|------------|--------|-------------|
| Plant | `Plant plant_name sun_level` | Creates a new integer variable `plant_name` initialized to `0`. The `sun_level` must be one of: `Full`, `Partial`, `Indirect`. |
| Water | `Water plant_name` | Increases the value of `plant_name` based on its `sun_level`: `+3` (Full), `+2` (Partial), `+1` (Indirect). |
| Prune | `Prune plant_name` | Decreases the value of `plant_name` based on its `sun_level`: `-1` (Full), `-2` (Partial), `-3` (Indirect). |
| Chop | `Chop plant_name` | Resets the value of `plant_name` to `0`. |
| Uproot | `Uproot plant_name` | Removes `plant_name` from memory. |
| Graft | `Graft plant_name1, plant_name2` | Adds `plant_name2` to `plant_name1`. Performs integer addition or string concatenation depending on data type. Result is stored in `plant_name1`. |
| MakePaper | `MakePaper plant_name` | Converts the value of `plant_name` to ASCII and prints it. |
| MakeMathPaper | `MakeMathPaper plant_name` | Prints the integer value of `plant_name`. |
| Inscribe | `Inscribe plant_name string_input` | Takes user input as a string and stores it in `plant_name`. If the input is a single-digit integer, it stores it as an integer value instead. |
| Peel | `Peel string_plant, plant_for_calc` | Removes the first character from `string_plant`, converts it to its ASCII value, and stores it in `plant_for_calc`. If `string_plant` is empty, assigns `-1` to `plant_for_calc`. |
| Measure | `Measure string_plant, length_plant` | Calculates the length of the string stored in `string_plant` and stores it in `length_plant`. |
| Branch | `Branch plant1, plant2, plant_result` | Compares `plant1` and `plant2`. Stores result in `plant_result`: `1` if `plant1 > plant2`, `0` if equal, `-1` if `plant1 < plant2`. |
| Jump | `Jump condition_plant line_if_1 (line_if_0) (line_if_neg1)` | Jumps to a line number based on the value of `condition_plant`. If value is `1`, jump to `line_if_1` (required). If `0`, jump to `line_if_0` (optional). If `-1`, jump to `line_if_neg1` (optional). Each target line is read from the value stored in the corresponding plant variable. |

# How to run programs
Using .net 10 in Visual Studio, run the program and input the file location of the program you wish to run.
