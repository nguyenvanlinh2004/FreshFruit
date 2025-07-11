﻿using System;
using System.Reflection;
using System.Runtime.Loader;

public class CustomAssemblyLoadContext : AssemblyLoadContext
{
	public CustomAssemblyLoadContext() : base(isCollectible: false) { }

	public IntPtr LoadUnmanagedLibrary(string absolutePath)
	{
		return LoadUnmanagedDll(absolutePath);
	}

	protected override IntPtr LoadUnmanagedDll(string unmanagedDllPath)
	{
		return LoadUnmanagedDllFromPath(unmanagedDllPath);
	}

	protected override Assembly? Load(AssemblyName assemblyName)
	{
		return null;
	}
}
