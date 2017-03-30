# ILject

> Provides a way which you can load a .NET dll/exe from disk, modify/inject IL, and then run the assembly all in memory without modifying the file.

## Projects in this repository

- ILject.Core: A .NET Standard 1.4 Library that is the main component of this repository
- ILject.SamplePatch: A sample application which changes the IL of a method in ILject.SamplePatchTarget and ILject.SampleFrameworkPatchTarget and then runs them
- ILject.SampleFrameworkPatchTarget: A .NET Framework application which gets targeted by ILject.SamplePatch
- ILject.SamplePatchTarget: A .NET Core application which gets targeted by ILject.SamplePatch

## How to use

I don't have any proper documentation at the moment. But you should be able to make out how to use this by looking at 
[this method](https://github.com/CameronAavik/ILject/blob/master/ILject.SamplePatch/Program.cs#L16-L19) and 
[a sample injector](https://github.com/CameronAavik/ILject/blob/master/ILject.SamplePatch/SampleInjectors.cs#L8-L22)

## Contribute

PRs accepted.