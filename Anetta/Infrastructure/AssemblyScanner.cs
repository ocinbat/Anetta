using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Anetta.Infrastructure
{
    public class AssemblyScanner
    {
        private static readonly Dictionary<string, Assembly> Assemblies;

        static AssemblyScanner()
        {
            Assemblies = new Dictionary<string, Assembly>();

            Assembly entryAssembly = Assembly.GetEntryAssembly();
            LoadAssembly(entryAssembly);
        }

        public static List<Assembly> GetAssemblies()
        {
            return Assemblies.Select(p => p.Value).ToList();
        }

        public static List<Type> GetTypes()
        {
            return GetAssemblies().SelectMany(a => a.GetTypes().ToList()).ToList();
        }

        public static List<Type> GetAllTypesOfInterface<T>()
        {
            return GetTypes().Where(t => Implements(t, typeof(T)) && t != typeof(T)).ToList();
        }

        private static void LoadAssembly(Assembly assembly)
        {
            Assemblies.Add(assembly.FullName, assembly);

            AssemblyName[] assemblyNames = assembly.GetReferencedAssemblies();

            if (assemblyNames.Any())
            {
                foreach (AssemblyName assemblyName in assemblyNames)
                {
                    try
                    {
                        Assembly referencedAssembly = Assembly.Load(assemblyName);

                        if (!Assemblies.ContainsKey(referencedAssembly.FullName))
                        {
                            LoadAssembly(referencedAssembly);
                        }
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }
            }
        }

        private static bool Implements<I>(Type type, I @interface) where I : class
        {
            if (((@interface as Type) == null) || !(@interface as Type).IsInterface)
            {
                throw new ArgumentException("Only interfaces can be 'implemented'.");
            }

            return (@interface as Type).IsAssignableFrom(type);
        }
    }
}