# PEPatcher

> Patches .NET Portable Executables on assembly load without modifying on disk 

## Projects in this repository

- PEPatcher.Core: A .NET Standard 1.4 Library that is the main component of this repository
- PEPatcher.SamplePatch: A sample application which changes the IL of a method in PEPatcher.SamplePatchTarget and PEPatcher.SampleFrameworkPatchTarget and then runs them
- PEPatcher.SampleFrameworkPatchTarget: A .NET Framework application which gets targeted by PEPatcher.SamplePatch
- PEPatcher.SamplePatchTarget: A .NET Core application which gets targeted by PEPatcher.SamplePatch

## How to use

I don't have any proper documentation at the moment. But you should be able to make out how to use this by looking at 
[this method](https://github.com/CameronAavik/PEPatcher/blob/master/PEPatcher.SamplePatch/Program.cs#L16-L19) and 
[a sample injector](https://github.com/CameronAavik/PEPatcher/blob/master/PEPatcher.SamplePatch/SampleInjectors.cs#L8-L22)

## Contribute

PRs accepted.