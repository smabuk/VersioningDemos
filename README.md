# Versioning Demos
Examples of ways to version .NET Core projects both without and with CI/CD

There are 3 versions generally referenced within a .NET Core assembly
* File Version - this is used to see which DLL is later.
    It has the pattern x.x.x.x usually considered to be Major.Minor.Build.Revision
* Product Version - also known as the InformationalVersion within the assembly. This is a freeform text version that is used for descriptive purposes.
* Assembly Version - the version of the assembly

## Projects
All of these projects use the .csproj files rather than the previous techniques
that used AssemblyInfo.cs or project.json

1. Default
2. Manual (Version)
  - Using Visual Studio UI
  - Editing `<project>.csproj` 
3. VersionPrefix
4. VersionSuffix
5. MSBuild
6. VSTS pipeline
