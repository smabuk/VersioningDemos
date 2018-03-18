# Versioning Demos
Examples of ways to version .NET Core projects both without and with CI/CD

There are 3 versions generally referenced within a .NET Core assembly
* File Version - this is used to see which DLL is later.
    * It has the pattern **x.y.b.r** usually considered to be Major.Minor.Build.Revision
* Product Version - also known as the InformationalVersion within the assembly. This is a freeform text version that is used for descriptive purposes.
    * Currently the format of this tends to follow the Semantic Versioning pattern (put forward by GitHub)
    * **x.y.p-buildinfo** Major.Minor.Patch[-metadata]
    * You can read more at [semver.org](https://semver.org/)
* Assembly Version - the version of the assembly

## Projects
All of these projects use the .csproj files rather than the previous techniques
that used AssemblyInfo.cs or project.json

1. Default
2. Manual (Version)
  - Using Visual Studio UI
  - Editing `<project>.csproj` 
3. Manual.AllDifferent
  - Using Visual Studio UI
  - Editing `<project>.csproj` 
4. VersionPrefix
5. VersionSuffix
6. MSBuild
7. VSTS pipeline
