using System.IO;
using UnrealBuildTool;

public class FastNoise2 : ModuleRules
{
	public FastNoise2(ReadOnlyTargetRules Target) : base(Target)
	{
		Type = ModuleType.External;
		PublicIncludePaths.Add(Path.Combine(ModuleDirectory, "include"));

		PublicAdditionalLibraries.Add(StaticLibraryPath);
	}

	private string ConfigName
	{
		get
		{
			return Target.Configuration == UnrealTargetConfiguration.Debug ? "Debug" : "Release";
		}
	}

	private string PlatformString
	{
		get
		{
			return Target.Platform.ToString();
		}
	}

	private string StaticLibraryPath
	{
		get
		{
			if (Target.Platform == UnrealTargetPlatform.IOS || Target.Platform == UnrealTargetPlatform.Android || Target.Platform.IsInGroup(UnrealPlatformGroup.Microsoft))
			{
				return Path.Combine(ModuleDirectory, "lib", PlatformString, ConfigName, LibraryName + StaticLibraryExtension);
			}

			if (Target.Platform == UnrealTargetPlatform.Mac)
			{
				return Path.Combine(ModuleDirectory, "lib", PlatformString, "AppleSilicon", ConfigName, LibraryName + StaticLibraryExtension);
			}

			if (Target.Platform == UnrealTargetPlatform.LinuxArm64)
			{
				return Path.Combine(ModuleDirectory, "lib", PlatformString, "Arm", ConfigName, LibraryName + StaticLibraryExtension);
			}

			if (Target.Platform == UnrealTargetPlatform.Linux)
			{
				return Path.Combine(ModuleDirectory, "lib", PlatformString, "x64", ConfigName, LibraryName + StaticLibraryExtension);
			}
			
			throw new BuildException("Unsupported platform");
		}
	}

	private string LibraryName
	{
		get
		{
			if (Target.Configuration == UnrealTargetConfiguration.Debug)
			{
				if (Target.Platform == UnrealTargetPlatform.Mac ||
				    Target.Platform == UnrealTargetPlatform.IOS ||
				    Target.Platform == UnrealTargetPlatform.Android ||
				    Target.Platform == UnrealTargetPlatform.Linux ||
				    Target.Platform == UnrealTargetPlatform.LinuxArm64)
				{
					return "libFastNoiseD";
				}
				if (Target.Platform.IsInGroup(UnrealPlatformGroup.Microsoft))
				{
					return "FastNoiseD";
				}
			}
			else
			{

				if (Target.Platform == UnrealTargetPlatform.Mac ||
				    Target.Platform == UnrealTargetPlatform.IOS ||
				    Target.Platform == UnrealTargetPlatform.Android ||
				    Target.Platform == UnrealTargetPlatform.Linux ||
				    Target.Platform == UnrealTargetPlatform.LinuxArm64)
				{
					return "libFastNoise";
				}
				if (Target.Platform.IsInGroup(UnrealPlatformGroup.Microsoft))
				{
					return "FastNoise";
				}
			}

			throw new BuildException("Unsupported platform");
		}
	}

	private string StaticLibraryExtension
	{
		get
		{
			if (Target.Platform == UnrealTargetPlatform.Mac)
			{
				return ".a";
			}

			return ".lib";
		}
	}
}
