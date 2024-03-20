# MARC Validator
Tool for validating MARC records.

## How to use?
MARC Validator is a Windows console tool, meaning it can be used with command line in Windows with parameters.

+ -p, –-path=[pathToFile]: Path to file with MARC record (or collection of MARC records) you want to validate. __This parameter is mandatory__.
+ -o, –-output=[pathToFile]: Path to text file you want to output results to (if file does not exist, it will be created).
+ -v, –-verbose: Writes also failed conditions with MA, RA, or O obligation.
+ -h, -?, -–help: Writes help.

__Example Full Command:__ 
```
MarcValidator.exe -p=C:\myMARC.xml --output D:\output.txt -v
```

## Supported formats
Only supported format is currently __MARXML__ and output is currently just in text files.
