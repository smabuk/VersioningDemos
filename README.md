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

The normal method you see for retrieving version information only works for
some use cases and falls apart when placed into a library.

## Projects
All of these projects use the .csproj files rather than the previous techniques
that used AssemblyInfo.cs or project.json

1. Nothing set - [Default](Default\Default.vbproj)
2. Setting a value - [Manual](Manual\Manual.vbproj)
  - Using Visual Studio UI
  - Editing `<project>.csproj` 
3. Setting all values - [Manual.AllDifferent](Manual.AllDifferent\Manual.AllDifferent.vbproj)
  - Using Visual Studio UI
  - Editing `<project>.csproj` 
4. Just use VersionPrefix - [VersionPrefix](VersionPrefix\VersionPrefix.vbproj)
5. VersionSuffix
6. BuildRevision
  -  replicate build and revision wildcards by implementing [roslyn's version pattern logic](https://github.com/dotnet/roslyn/blob/614299ff83da9959fa07131c6d0ffbc58873b6ae/src/Compilers/Core/Portable/VersionHelper.cs#L187-L202) 
  -  Uses calculations in the csproj file
7. BuildDateTime
  - used x.YYYY.MDD.HHMM to create automated values
8. VSTS
  - Add configured BUILDNUMBER from VSTS to VersionSuffix
9. VSTS.WithGit
  - Add Git branch to Version Suffix as well
