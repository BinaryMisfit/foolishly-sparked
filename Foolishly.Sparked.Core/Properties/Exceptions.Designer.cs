﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Foolishly.Sparked.Core.Properties {
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Exceptions {

        private static global::System.Resources.ResourceManager resourceMan;

        private static global::System.Globalization.CultureInfo resourceCulture;

        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Exceptions() {
        }

        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Foolishly.Sparked.Core.Properties.Exceptions", typeof(Exceptions).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }

        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to Base game not found..
        /// </summary>
        public static string GameBaseNotFound {
            get {
                return ResourceManager.GetString("GameBaseNotFound", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to No game found..
        /// </summary>
        public static string GameDirectoryNotFound {
            get {
                return ResourceManager.GetString("GameDirectoryNotFound", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to No game files found..
        /// </summary>
        public static string GameFilesNotFound {
            get {
                return ResourceManager.GetString("GameFilesNotFound", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to Not a valid Sims Toolkit plugin..
        /// </summary>
        public static string PluginInvalid {
            get {
                return ResourceManager.GetString("PluginInvalid", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to Valid platform plugin not found..
        /// </summary>
        public static string PluginMissingPlatform {
            get {
                return ResourceManager.GetString("PluginMissingPlatform", resourceCulture);
            }
        }
    }
}
