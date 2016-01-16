using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("ManagedDism")]
[assembly: AssemblyDescription("Managed API for Deployment Image Servicing and Management (DISM)")]
#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif
[assembly: AssemblyCompany("ManagedDism")]
[assembly: AssemblyProduct("DISM")]
[assembly: AssemblyCopyright("Copyright ©  2016")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: CLSCompliant(false)]
[assembly: ComVisible(false)]
[assembly: AssemblyVersion("1.5.*")]
[assembly: AssemblyInformationalVersion("1.5.1")]
[assembly: InternalsVisibleTo("ManagedDism.Tests")]