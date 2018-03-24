# Versioning Demos
Examples of ways to version .NET Core projects both with and without Continuous Deployment (CD)

If you clone this site and run it locally in Debug and Release modes you will see different results.
You can visit a live version at https://versioningdemos.azurewebsites.net/ this has passed through my Git and VSTS to
generate the version information.

You will notice that Microsoft has different versioning strategies within their own .Net libraries, presumaby not
from the same teams.

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

## Version Sample Projects
All of these projects use the .vbproj (could also be .csproj)files rather than the previous techniques
that used AssemblyInfo.cs or project.json

### Nothing set - [Default](Default/Default.vbproj)
    - Defaults to 1.0.0

### Setting a value - [Manual](Manual/Manual.vbproj)
 Using Visual Studio UI (Project Properties / Package)
    - Even if you don't use this to set the information it will display the calculated values
    - Be careful you don't accidentally override values calculated elsewhere
  - Editing `<project>.csproj` 

### Setting all values - [Manual.AllDifferent](Manual.AllDifferent/Manual.AllDifferent.vbproj)
    - Using Visual Studio UI
    - Editing `<project>.csproj` 

### Just use VersionPrefix - [VersionPrefix](VersionPrefix/VersionPrefix.vbproj)

### VersionSuffix - [VersionSuffix](VersionSuffix/VersionSuffix.vbproj)

### BuildRevision - [BuildRevision](BuildRevision/BuildRevision.vbproj)
    -  replicate build and revision wildcards by implementing [roslyn's version pattern logic](https://github.com/dotnet/roslyn/blob/614299ff83da9959fa07131c6d0ffbc58873b6ae/src/Compilers/Core/Portable/VersionHelper.cs#L187-L202) 
    -  Uses calculations in the csproj file

### BuildDateTime - [BuildDateTime](BuildDateTime/BuildDateTime.vbproj)
    - used x.YYYY.MDD.HHMM to create automated values

### Visual Studio Team Services build - [VSTS](VSTS/VSTS.vbproj)
    - Add configured BUILDNUMBER from VSTS to VersionSuffix

###  Visual Studio Team Services build with Git branch - [VSTS.WithGit](VSTS.WithGit/VSTS.WithGit.vbproj)
    - Add Git branch to Version Suffix as well

## TagHelpers
When using Taghelpers there are several Gotchas.
  - Pages end up being placed into different assemblies than you may expect
  - Running in Debug or Release may give different values and created assemblies
  - Putting a TagHelper in an external library changes the result of the current running assembly
